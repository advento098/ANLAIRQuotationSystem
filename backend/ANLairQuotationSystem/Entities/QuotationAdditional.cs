namespace ANLairQuotationSystem.Entities;

public class QuotationAdditional
{
    public uint Id { get; set; }
    public uint QuotationId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public QuotationOperator Operator { get; set; }
    public decimal Cost { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public Quotation Quotation { get; set; } = null!;

    public enum QuotationOperator
    {
        Add = 1,
        Subtract = 2,
        Multiply = 3,
        Divide = 4,
        PercentageAdd = 5
    }

    public decimal CalculateNewValueUsingOperator(decimal currentValue)
    {
        decimal resultingValue = Operator switch
        {
            QuotationOperator.Add => currentValue + Cost,
            QuotationOperator.Subtract => currentValue - Cost,
            QuotationOperator.Multiply => currentValue * Cost,
            QuotationOperator.PercentageAdd => currentValue + (currentValue * Cost),
            _ => throw new ArgumentException("Invalid operator: OPERATOR_NOT_FOUND"),
        };

        return resultingValue;
    }
}
