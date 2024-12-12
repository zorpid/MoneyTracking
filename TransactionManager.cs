// See https://aka.ms/new-console-template for more information









using System.Text.Json;
using System.IO;

public static class TransactionManager
{
    private static readonly string PinFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "pin.json");
    private static readonly string FilePath = "transactions.json";




    public static void ShowKeypad()
    {
        Console.Clear();
        Console.WriteLine("*****************");
        Console.WriteLine("*   1   2   3   *");
        Console.WriteLine("*   4   5   6   *");
        Console.WriteLine("*   7   8   9   *");
        Console.WriteLine("*       0       *");
        Console.WriteLine("*****************");
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }


    public static string LoadPin()
    {
        if (File.Exists(PinFilePath))
        {
            return File.ReadAllText(PinFilePath); // Read the PIN from the file
        }

        // If the file doesn't exist, create a default PIN
        string defaultPin = "1234";
        File.WriteAllText(PinFilePath, defaultPin); // Save default PIN to the file
        return defaultPin;
    }

    public static void ShowItems(List<Transaction> transactions)
    {
        Console.Clear();

        if (transactions.Count == 0)
        {
            Console.WriteLine("No transactions to display.");
        }
        else
        {
            var sortedTransactions = transactions.OrderByDescending(t => t.Date).ToList(); // OrderByDescending makes newest first

            foreach (var transaction in sortedTransactions)
            {
                if (transaction.Type == TransactionType.Income)
                {
                    Console.ForegroundColor = ConsoleColor.Green; // Green for income
                }
                else if (transaction.Type == TransactionType.Expense)
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Red for expense
                }

                Console.WriteLine($"{transaction.Title} - {transaction.Amount:C} ({transaction.Type}) on {transaction.Date:yyyy-MM-dd}");
            }

            
            Console.ForegroundColor = ConsoleColor.Yellow;
            // Calculate and display the balance
            decimal balance = CalculateBalance(transactions);
            Console.WriteLine($"\nCurrent Balance: {balance:C}");
            Console.ResetColor(); // Reset text color to default
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    public static void AddTransaction(List<Transaction> transactions)
    {
        Console.Clear();
        Console.WriteLine("Add New Transaction");

        Console.Write("Enter Title: ");
        string title = Console.ReadLine();

        Console.Write("Enter Amount: ");
        decimal amount;
        while (!decimal.TryParse(Console.ReadLine(), out amount))
        {
            Console.Write("Invalid amount. Enter again: ");
        }

        Console.Write("Enter Date (yyyy-MM-dd): ");
        DateTime date;
        while (!DateTime.TryParse(Console.ReadLine(), out date))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid date. Enter again (yyyy-MM-dd): ");
        }

        Console.Write("Enter Type (Income/Expense): ");
        string typeInput = Console.ReadLine();
        TransactionType type = (TransactionType)Enum.Parse(typeof(TransactionType), typeInput, true);

        Transaction transaction = new Transaction
        {
            Title = title,
            Amount = amount,
            Date = date,
            Type = type
        };

        transactions.Add(transaction);
        Console.WriteLine("Transaction added! Press any key to return to the menu...");
        Console.ReadKey();
    }

    public static void EditOrRemoveTransaction(List<Transaction> transactions)
    {
        Console.Clear();
        if (transactions.Count == 0)
        {
            Console.WriteLine("No transactions to edit or remove.");
            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Select a transaction to edit or remove:");
        for (int i = 0; i < transactions.Count; i++)
        {
            Console.WriteLine($"{i + 1}) {transactions[i]}");
        }

        Console.Write("Enter the number of the transaction: ");
        int index;
        while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index > transactions.Count)
        {
            Console.Write("Invalid choice. Enter again: ");
        }

        Console.WriteLine("1) Edit");
        Console.WriteLine("2) Remove");
        Console.Write("Choose an action: ");
        string action = Console.ReadLine();

        if (action == "1")
        {
            Console.WriteLine("Editing not implemented yet.");
        }
        else if (action == "2")
        {
            transactions.RemoveAt(index - 1);
            Console.WriteLine("Transaction removed!");
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }

        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }

    public static decimal CalculateBalance(List<Transaction> transactions)
    {
        decimal balance = 0;

        foreach (var transaction in transactions)
        {
            if (transaction.Type == TransactionType.Income)
            {
                balance += transaction.Amount;
            }
            else if (transaction.Type == TransactionType.Expense)
            {
                balance -= transaction.Amount;
            }
        }

        return balance;
    }


    public static void SaveTransactions(List<Transaction> transactions)
    {
        try
        {
            // Serialize the transactions list to JSON
            string json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });

            // Ensure the file is created and the JSON is written
            File.WriteAllText(FilePath, json);
            Console.WriteLine($"Data saved to {FilePath}");
            Console.WriteLine("Data saved successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving data: {ex.Message}");
        }
    }
    public static List<Transaction> LoadTransactions()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                string json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
            }
            else
            {
                return new List<Transaction>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading transactions. Resetting file. Details: {ex.Message}");
            return new List<Transaction>();
        }
    }

    public static bool Login()
    {
        Console.ResetColor();
        string storedPin = LoadPin(); // Load the PIN from the file
        int attempts = 3;
        ShowKeypad();

        while (attempts > 0)
        {
            Console.Write("Enter your PIN: ");
            string enteredPin = ReadHiddenInput();

            if (enteredPin == storedPin)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Login successful!");
                Console.ResetColor();
                return true;  // Proceed to the main menu
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                attempts--;
                Console.WriteLine($"Incorrect PIN. You have {attempts} attempt(s) remaining.\n");
                Console.ResetColor();
            }
        }

        Console.WriteLine("Too many failed attempts. Exiting...");
        return false;
    }
    public static void ChangePin()
    {
        string currentPin = LoadPin(); // Load the current PIN

        Console.Write("Enter your current PIN: ");
        string enteredPin = Console.ReadLine();

        if (enteredPin != currentPin)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Incorrect current PIN. PIN not changed.");
            Console.ResetColor();
            return;
        }

        Console.Write("Enter your new PIN: ");
        string newPin = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(newPin) || newPin.Length != 4 || !IsDigitsOnly(newPin))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid PIN. PIN must be 4 digits. PIN not changed.");
            Console.ResetColor();
            return;
        }
        Console.ForegroundColor = ConsoleColor.Green;
        SavePin(newPin); // Save the new PIN to the file
        Console.WriteLine("PIN changed successfully!");
        Console.ResetColor();
    }
    private static bool IsDigitsOnly(string str)
    {
        foreach (char c in str)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }
        return true;

    }
    public static void SavePin(string newPin)
    {
        File.WriteAllText(PinFilePath, newPin); // Write the new PIN to the file
    }
    private static string ReadHiddenInput()
    {
        string input = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true); // Read key, but don't display it

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                input += key.KeyChar; // Add the character to the input
                Console.Write("*");  // Display asterisk
            }
            else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input = input[0..^1]; // Remove the last character
                Console.Write("\b \b"); // Remove the asterisk from the console
            }
        } while (key.Key != ConsoleKey.Enter); // Stop on Enter key

        return input;
    }



}
