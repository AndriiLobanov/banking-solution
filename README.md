Banking Solution API

A simple banking REST API built with ASP.NET Core, EF Core InMemory provider, and following SOLID principles.
The API supports account management and transactions (deposit, withdraw, transfer), with automated unit tests.

FEATURES

Account Management:
- Create a new account with initial balance.
- Get account details by account number.
- List all accounts.

Transactions:
- Deposit funds into an account.
- Withdraw funds from an account.
- Transfer funds between accounts.
- Retrieve transaction history per account.

TECH STACK
- ASP.NET Core Web API
- Entity Framework Core (in-memory DB)
- Mapster (object mapping)
- FluentValidation (input validation)
- xUnit + Moq + FluentAssertions (testing)

QUALITY
    Global error-handling middleware with meaningful HTTP responses

PREREQUISITES
- Installed .NET 8 SDK: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
- Installed Git

Setup & Run

1. Clone the repo:
    `git clone https://github.com/AndriiLobanov/banking-solution.git`
    `cd banking-solution`

2. Restore dependencies:
`dotnet restore`

3. if you want to work from CMD further, then run `dotnet run --project BankingSolution.WebApi` to start the project.
    If you have VS 2022, run through it.

4. You will see in the console the localhost URL with the port. Default ports are `https://localhost:7152` and `http://localhost:5229`. Add `/swagger/index.html` after `https://localhost:port`. If you run from VS, choose `https` profile and it will open Swagger page immediately.

API Endpoints

    Accounts:
    - `POST /api/accounts`: Create account
    - `GET /api/accounts/{accountNumber}`: Get account details
    - `GET /api/accounts`: List all accounts

    Transactions:
    - `POST /api/transactions/{accountNumber}/deposit`: Deposit funds
    - `POST /api/transactions/{accountNumber}/withdraw`: Withdraw funds
    - `POST /api/transactions/transfer`: Transfer funds
    - `GET /api/transactions/{accountNumber}`: List transactions for account

TESTS
    To run tests, use `dotnet test`.

DESIGN NOTES
- Entity Framework in-memory is used for persistence (no external DB needed).
- Mapster handles DTO to entity mappings.
- FluentValidation validates all requests before reaching services.
- Middleware centralizes error handling and returns consistent JSON error responses.
- Unit tests validate service logic in isolation.

Author

Andrii Lobanov. Developed a project to demonstrate clean architecture, best practices, and test-driven design in .NET.