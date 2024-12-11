// See https://aka.ms/new-console-template for more information

List<Transaction> transactions = TransactionManager.LoadTransactions();
//List<Transaction> transactions = new List<Transaction>();
bool running = true;
if (!TransactionManager.Login())
{
    return; // Exit the program if login fails
}

while (running)
{
    Console.WriteLine("Welcome to TrackMoney");
    Console.WriteLine($"Current Balance: {TransactionManager.CalculateBalance(transactions):C}");
    Console.WriteLine("1) Show items (All/Expenses/Incomes)");
    Console.WriteLine("2) Add New Expense/Income");
    Console.WriteLine("3) Edit Item (edit, remove)");
    Console.WriteLine("4) Change pin");
    Console.WriteLine("5) Save and Quit");
    Console.Write("Choose an option: ");

    var choice = Console.ReadLine();


    switch (choice)
    {
        case "1":
            TransactionManager.ShowItems(transactions);
            
            break;
        case "2":
            TransactionManager.AddTransaction(transactions);
            break;
        case "3":
            TransactionManager.EditOrRemoveTransaction(transactions);
            break;
        case "4":

            TransactionManager.ChangePin();
            break;
        case "5":
            running = false;
            TransactionManager.SaveTransactions(transactions);
            break;

        default:
            Console.WriteLine("Invalid choice. Press any key to try again...");
            Console.ReadKey();
            break;
    }
}
   

