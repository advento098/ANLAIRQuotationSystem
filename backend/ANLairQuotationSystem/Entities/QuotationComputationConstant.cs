using static ANLairQuotationSystem.Entities.ComputationConstant;

namespace ANLairQuotationSystem.Entities;

public class QuotationComputationConstant
{
    public uint Id { get; set; }
    public uint QuotationId { get; set; }
    public required string Name { get; set; }
    public ConstantOperator Operator { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public Quotation Quotation { get; set; } = null!;

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
