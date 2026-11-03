# Mini Bank

A small banking REST API built with **C# / ASP.NET Core (.NET 10)** while learning C# and .NET from scratch over one weekend.

## Run it

```bash
dotnet run --project src/MiniBank.Api
```

Then open http://localhost:5087/accounts, or use [`src/MiniBank.Api/MiniBank.Api.http`](src/MiniBank.Api/MiniBank.Api.http) in VS Code (REST Client extension) to send every request, including the error cases.

## Endpoints

| Method | Route | Result |
|---|---|---|
| `GET` | `/accounts` | `200` + all accounts |
| `POST` | `/accounts/{id}/withdraw` with body `{ "amount": 50.25 }` | `200` + the updated account |

Withdraw rules:

| Case | Response |
|---|---|
| Amount is zero or negative | `400 Bad Request` |
| Amount is more than the balance | `400 Bad Request` (no negative balances) |
| Account doesn't exist | `404 Not Found` |
| Body isn't valid JSON / amount isn't a number | `400 Bad Request` (automatic, from `[ApiController]` model binding) |

## Architecture

```
HTTP request
    │
    ▼
AccountsController        thin: routing, HTTP status codes
    │  depends on IAccountService (injected by ASP.NET Core DI)
    ▼
AccountService            business rules (validation, overdraft check)
    │
    ▼
In-memory List<Account>   seed data; resets when the app restarts
```

- **Controllers** stay thin. They translate service outcomes into HTTP responses.
- **Business logic lives in a service** behind an interface, so it can be unit tested and swapped without touching the controller.
- **Dependency injection:** `AddSingleton<IAccountService, AccountService>()` in `Program.cs`. It's a Singleton because the in-memory list has to survive between requests.
- **`decimal` for money**, never `float`/`double`, to avoid binary rounding errors.

## Project layout

```
src/MiniBank.Api/
  Controllers/   HelloController, AccountsController
  Models/        Account, WithdrawRequest
  Services/      IAccountService, AccountService
  Program.cs     DI registration + request pipeline
tests/MiniBank.Tests/   xUnit project (tests not written yet)
```

## What I'd do next

1. **Persistence:** swap the in-memory list for **EF Core + a SQL database** (SQLite locally, SQL Server in production) with migrations and seed data. Because the controller depends on `IAccountService`, this is a new implementation class plus one line in `Program.cs`. EF Core also uses **parameterized queries**, which prevent SQL injection.
2. **Tests:** xUnit unit tests for the service (overdraft, invalid amount, unknown account) and an integration test with `WebApplicationFactory`.
3. **CI:** a GitHub Actions workflow that runs `dotnet build` and `dotnet test` on every push.
4. **Concurrency:** two simultaneous withdrawals could both pass the balance check. A database transaction with optimistic concurrency (a row version) would prevent that.
5. **Security:** protect the endpoints with OAuth 2.0 / OpenID Connect (JWT bearer tokens) and check that the caller owns the account.
6. **Front end:** a small accessible page that lists accounts and submits withdrawals with `fetch`.
