namespace ANLairQuotationSystem.Entities;

public class QuotationAdditional
{
    public uint Id { get; set; }
    public uint QuotationId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Cost { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public Quotation Quotation { get; set; } = null!;
}
