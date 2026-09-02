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

    public enum ConstantOperator
    {
        Add = 1,
        Subtract = 2,
        Multiply = 3,
        Divide = 4,
        PercentageAdd = 5
    }
}
