namespace ANLAIRQuotationSystem.Entities;

public class Quotation
{
    public uint ProjectId { get; set; }
    public decimal ItemsFinalCost { get; set; }
    public decimal FinalCost { get; set; }

    public Project Project { get; set; } = null!;
    public ICollection<QuotationAdditional> QuotationAdditionals { get; set; } = [];
}
