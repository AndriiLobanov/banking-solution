using BankingSolution.Application.Dtos;
using BankingSolution.Application.Services.Abstractions;
using BankingSolution.Application.Services;
using BankingSolution.DataAccess;
using BankingSolution.DataAccess.Entities;
using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using BankingSolution.Application.Exceptions;

namespace BankingSolution.Tests.Services;

public class TransactionServiceTests
{
    private readonly BankingDbContext dbContext;
    private readonly Mock<IMapper> mapperMock;
    private readonly Mock<IValidator<TransferRequest>> validatorMock;
    private readonly Mock<IServiceProvider> serviceProviderMock;
    private readonly TransactionService transactionService;

    public TransactionServiceTests()
    {
        var options = new DbContextOptionsBuilder<BankingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new BankingDbContext(options);

        mapperMock = new Mock<IMapper>();
        validatorMock = new Mock<IValidator<TransferRequest>>();
        serviceProviderMock = new Mock<IServiceProvider>();

        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IValidator<TransferRequest>)))
            .Returns(validatorMock.Object);

        transactionService = new TransactionService(
            serviceProvider: serviceProviderMock.Object,
            dbContext: dbContext,
            mapper: mapperMock.Object
        );
    }

    [Fact]
    public async Task TransferAsync_ValidRequest_ShouldTransferFunds()
    {
        // Arrange
        var from = new Account { AccountNumber = "A1", Balance = 200 };
        var to = new Account { AccountNumber = "A2", Balance = 50 };

        dbContext.Accounts.AddRange(from, to);
        await dbContext.SaveChangesAsync();

        var request = new TransferRequest("A1", "A2", 100);

        validatorMock
            .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var transactionResponse = new TransactionResponse(1, "Transfer", 100, DateTime.UtcNow, "A1", "A2");
        mapperMock.Setup(m => m.Map<TransactionResponse>(It.IsAny<Transaction>()))
            .Returns(transactionResponse);

        // Act
        var result = await transactionService.TransferAsync(request);

        // Assert
        Assert.Equal("A1", result.FromAccountNumber);
        Assert.Equal("A2", result.ToAccountNumber);
        Assert.Equal(100, result.Amount);

        var updatedFrom = await dbContext.Accounts.FirstAsync(a => a.AccountNumber == "A1");
        var updatedTo = await dbContext.Accounts.FirstAsync(a => a.AccountNumber == "A2");

        Assert.Equal(100, updatedFrom.Balance);
        Assert.Equal(150, updatedTo.Balance);
    }

    [Fact]
    public async Task TransferAsync_FromAccountNotFound_ShouldThrow()
    {
        // Arrange
        var request = new TransferRequest("X1", "A2", 50);

        validatorMock
            .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        // Act + Assert
        await Assert.ThrowsAsync<AccountNotFoundException>(() =>
            transactionService.TransferAsync(request));
    }

    [Fact]
    public async Task TransferAsync_InsufficientFunds_ShouldThrow()
    {
        // Arrange
        var from = new Account { AccountNumber = "A1", Balance = 10 };
        var to = new Account { AccountNumber = "A2", Balance = 50 };
        dbContext.Accounts.AddRange(from, to);
        await dbContext.SaveChangesAsync();

        var request = new TransferRequest("A1", "A2", 100);

        validatorMock
            .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        // Act + Assert
        await Assert.ThrowsAsync<InsufficientFundsException>(() =>
            transactionService.TransferAsync(request));
    }
}