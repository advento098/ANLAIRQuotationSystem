namespace ANLairQuotationSystem.Entities;

public class ComputationConstant
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    // TODO: Add a property that defines what to do with the value
    // maybe an enum to define it?
    /*
    public enum ConstantOperator
        {
            Add = 1,          // Flat fees, extra material costs
            Subtract = 2,     // Flat discounts, loyalty rebates
            Multiply = 3,     // Tax rates (e.g., 1.12), markup percentages (e.g., 1.10)
            PercentageAdd = 4 // Tax percentages stored as fractions (e.g., 0.12 means +12%)
        }
     */
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public ICollection<QuotationComputationConstant> QuotationComputationConstants { get; set; } = [];
}
