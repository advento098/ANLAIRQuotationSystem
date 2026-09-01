namespace ANLairQuotationSystem.Entities;

public class ComputationConstant
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    public ConstantOperator Operator { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public ICollection<QuotationComputationConstant> QuotationComputationConstants { get; set; } = [];

    public enum ConstantOperator
    {
        Add = 1,          // Flat fees, extra material costs
        Subtract = 2,     // Flat discounts, loyalty rebates
        Multiply = 3,     // Tax rates (e.g., 1.12), markup percentages (e.g., 1.10)
        PercentageAdd = 4 // Tax percentages stored as fractions (e.g., 0.12 means +12%)
    }

    public decimal CalculateNewValueUsingOperator(decimal currentValue)
    {
        decimal resultingValue = Operator switch
        {
            ConstantOperator.Add => currentValue + Value,
            ConstantOperator.Subtract => currentValue - Value,
            ConstantOperator.Multiply => currentValue * Value,
            ConstantOperator.PercentageAdd => currentValue + (currentValue * Value),
            _ => throw new ArgumentException("Invalid operator: OPERATOR_NOT_FOUND"),
        };

        return resultingValue;
    }
}
