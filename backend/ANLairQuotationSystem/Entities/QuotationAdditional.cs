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
}
