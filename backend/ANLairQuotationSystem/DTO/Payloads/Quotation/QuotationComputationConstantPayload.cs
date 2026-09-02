using static ANLairQuotationSystem.Entities.ComputationConstant;

namespace ANLairQuotationSystem.DTO.Payloads.Quotation;

public class QuotationComputationConstantPayload
{
    public uint? Id { get; set; } = null;
    public required string Name { get; set; }
    public ConstantOperator Operator { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
}
