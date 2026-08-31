namespace ANLairQuotationSystem.Entities;

public class QuotationComputationConstant
{
    public uint QuotationId { get; set; }
    public uint ComputationConstantId { get; set; }
    // TODO: Add snapshot of the computation constant here
    public Quotation Quotation { get; set; } = null!;
    public ComputationConstant ComputationConstant { get; set; } = null!;
}
