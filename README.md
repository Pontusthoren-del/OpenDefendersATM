
______________________________________  ____________
___  __ \__  ____/__    |__  __ \__   |/  /__  ____/
__  /_/ /_  __/  __  /| |_  / / /_  /|_/ /__  __/   
_  _, _/_  /___  _  ___ |  /_/ /_  /  / / _  /___   
/_/ |_| /_____/  /_/  |_/_____/ /_/  /_/  /_____/   

## OpenDefendersATM
This is an awesomely-simplified console-based banking simulation implemented in C# (.NET 8). It supports users: admin and customers, accounts, deposits/withdrawals, transfers which includes cross-currency conversion, loans, and a transaction log. 

The application follows a simple layered structure where the UI handles user interaction, domain classes handle business logic, and the BankSystem class manages shared system-wide data such as users, accounts, and exchange rates. 

## Key features
- User roles: Admin & Customer
- Multiple accounts per customer: regular and savings with option to change name of accounts
- Deposits, withdrawals and transfers between accounts
- Cross-currency transfers using stored exchange rates
- Transaction logging with timestamps
- Simple console UI for interaction

## Class overview
The code consists of fourteen classes:

User - Base class for all users, handling PIN authentication and account lockout.
Admin - Represents an administrative user with system-level access. Extends the User class.
Customer - Represents a bank customer with accounts and loans. Extends the User class.
Account - Handles balance, currency, transaction history, including deposits, withdrawals and transfers.
Backup - Contains helper methods used throughout the application.
BankSystem - Manages users (customers and admin), accounts, exchange rates, and core banking operations such as balance calculations and currency conversion.
Loan - Handles methods for loans
Program - Runs the program.
SavingsAccount - Represents a savings account with an interest rate and basic interest calculation, extending the Account class.
Transaction - Represents a bank transaction, including involved accounts, amount, currency, timestamp, unique ID and transaction status.
TransactionStatus - Struct that defines the possible transaction states: pending, declined and complete. 
UI - Handles the main user interface flow, including login/logout, menus, transfers, withdrawals/deposits etc.
UIAdmin - Functionality for administration to handle and register new customers, unlock their accounts, update rates etc.
UICustomer - Functionality for customers: apply for loans, renaming accounts, open new accounts, transfer between accounts etc. 


## Where to find things
- UI: `UI.cs`, `UICustomer.cs`, `UIAdmin.cs`
- Domain: `User.cs`, `Customer.cs`, `Admin.cs`
- Accounts & Transactions: `Account.cs`, `Transaction.cs`
- Exchange rate and system-level logic: `BankSystem.cs`
- Loans: `Loan.cs`


## Requirements
- .NET 8 SDK
- C# 
- Visual Studio 2022 (recommended) or any editor/IDE that supports .NET 8

## Quick start (Visual Studio)
1. Clone the repository:
2. Open solution in Visual Studio 2022. 
3. Build and run the project.


## Notes and important behavior
Exchange rates are stored and used by `BankSystem`. Cross-currency transfers convert the sender amount to the receiver currency before crediting the recipient.
Timestamps are assigned in `Transaction` (use `Transaction.GetTimeStamp()` or `DateTime.Now` as implemented).
Banking operations are executed from the `Account` methods; the UI calls these methods to keep business logic separate from presentation logic.


## Contact / License
 This is an educational project made by (GitHub):


Pontusthoren-del
RobinJohansson1992
Bellanthie
julialouisek




