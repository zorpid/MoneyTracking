// See https://aka.ms/new-console-template for more information

public enum TransactionType { Income, Expense }
public class Transaction
{
    

    public string Title { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public TransactionType Type { get; set; }
    public override string ToString()
    {
        if (Type == TransactionType.Income)
        {
            Console.ForegroundColor = ConsoleColor.Green; // Green for income
        }
        else if (Type == TransactionType.Expense)
        {
            Console.ForegroundColor = ConsoleColor.Red; // Red for expense
        }

        // Display transaction details
        string result = $"{Title} - {Amount:C} ({Type}) on {Date:yyyy-MM-dd}";

        Console.ResetColor(); // Reset color to default
        return result;
    }
}