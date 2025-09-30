using BankingSolution.Application.Dtos;
using BankingSolution.Application.Services;
using BankingSolution.Application.Services.Abstractions;
using BankingSolution.DataAccess;
using BankingSolution.DataAccess.Entities;
using FluentValidation;
using FluentValidation.Results;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BankingSolution.Tests.Services;

public class AccountServiceTests
{
    private readonly BankingDbContext dbContext;
    private readonly Mock<IMapper> mapperMock;
    private readonly Mock<IAccountNumberGenerator> accountNumberGeneratorMock;
    private readonly Mock<IValidator<CreateAccountRequest>> validatorMock;
    private readonly Mock<IServiceProvider> serviceProviderMock;
    private readonly AccountService accountService;

    public AccountServiceTests()
    {
        var options = new DbContextOptionsBuilder<BankingDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        dbContext = new BankingDbContext(options);

        mapperMock = new Mock<IMapper>();
        accountNumberGeneratorMock = new Mock<IAccountNumberGenerator>();
        validatorMock = new Mock<IValidator<CreateAccountRequest>>();
        serviceProviderMock = new Mock<IServiceProvider>();

        serviceProviderMock
            .Setup(sp => sp.GetService(typeof(IValidator<CreateAccountRequest>)))
            .Returns(validatorMock.Object);

        accountService = new AccountService(
            serviceProvider: serviceProviderMock.Object,
            dbContext: dbContext,
            mapper: mapperMock.Object,
            accountNumberGenerator: accountNumberGeneratorMock.Object
        );
    }

    [Fact]
    public async Task CreateAccountAsync_ValidRequest_ShouldCreateAccount()
    {
        // Arrange
        var request = new CreateAccountRequest(InitialBalance: 100, Email: "john@gmail.com", Name: "alina");
        var account = new Account();
        var expectedAccountNumber = "010";
        var expectedAccountResponse = new AccountResponse(expectedAccountNumber, request.InitialBalance, request.Name, request.Email);

        validatorMock
            .Setup(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        mapperMock.Setup(m => m.Map<Account>(request)).Returns(account);
        accountNumberGeneratorMock.Setup(g => g.GenerateUniqueAccountNumberAsync()).ReturnsAsync(expectedAccountNumber);
        mapperMock.Setup(m => m.Map<AccountResponse>(account)).Returns(expectedAccountResponse);

        // Act
        var result = await accountService.CreateAccountAsync(request);

        // Assert
        Assert.Equal(expectedAccountResponse, result);
    }
}