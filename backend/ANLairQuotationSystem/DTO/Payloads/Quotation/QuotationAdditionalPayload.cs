using static ANLairQuotationSystem.Entities.QuotationAdditional;

namespace ANLairQuotationSystem.DTO.Payloads.Quotation;

public class QuotationAdditionalPayload
{
    public uint? Id { get; set; } = null;
    public required string Name { get; set; }
    public string? Description { get; set; }
    public QuotationOperator Operator { get; set; }
    public decimal Cost { get; set; }
}
