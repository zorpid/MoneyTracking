// See https://aka.ms/new-console-template for more information

public enum TransactionType { Income, Expense }
public class Transaction
{
    

    public string Title { get; set; }
    public decimal Amount { get; set; }
    public string Month { get; set; }
    public TransactionType Type { get; set; }
    public override string ToString()
    {
        return $"{Title} - {Amount:Kr} ({Type}) in {Month}";
    }
}