namespace ANLairQuotationSystem.DTO.Payloads.Quotation;

public class AddEditQuotationPayload
{
    public required string ProjectUniqueId { get; set; }
    public List<string>? ConstantComputationNames { get; set; } = null;
    public List<QuotationComputationConstantPayload>? QuotationComputationConstantPayloads { get; set; } = null;
    public List<QuotationAdditionalPayload>? Additionals { get; set; } = null;
}
