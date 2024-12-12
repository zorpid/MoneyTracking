# Money Tracking Application

## Overview
The Money Tracking Application is a console-based program designed to help users manage their finances. It allows users to record incomes and expenses, view transaction history, calculate their current balance, and set personalized goals. The application features a secure PIN-based login system for access.

---

## Features

- **Secure PIN-Based Login**: Users must enter a PIN to access the application.
- **Transaction Management**:
  - Add incomes and expenses.
  - View all transactions sorted by date.
  - Edit or remove existing transactions.
- **Balance Calculation**: Automatically calculates the net balance based on incomes and expenses.
- **Transaction Sorting**:
  - View transactions sorted by oldest first or newest first.
- **Data Persistence**: All transactions are saved to a JSON file and loaded automatically when the application starts.
- **Customizable PIN**: Users can change their PIN from the settings menu.

---

## Default PIN
- The default PIN to access the application is: `1234`
- Users are encouraged to change their PIN upon first login for security purposes.

---

## How to Run
1. Clone or download the project.
2. Open the project in Visual Studio or any compatible C# IDE.
3. Build and run the project.
4. Log in using the default PIN (`1234`) or your custom PIN if you have already changed it.
5. Navigate through the menu to manage your finances.

---

## Usage Instructions

### Main Menu Options:
1. **Show Items**: View all transactions sorted by date.
2. **Add New Expense/Income**: Record a new transaction.
3. **Edit Item**: Modify or remove an existing transaction.
4. **Save and Quit**: Save your data and exit the application.

### Additional Features:
- **Change PIN**: Navigate to the settings menu to update your PIN.
- **Sorting Options**: Choose to view transactions sorted by newest first.

---

## Project Structure
- **Program.cs**: Handles the main flow of the application.
- **TransactionManager.cs**: Contains methods for managing transactions, login, and other core functionalities.
- **Transaction.cs**: Defines the `Transaction` class and its properties.
- **Data Files**:
  - `transactions.json`: Stores all transaction data persistently.
  - `pin.json`: Stores the user’s PIN securely.

---



