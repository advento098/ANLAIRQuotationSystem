namespace ANLAIRQuotationSystem.Entities;

public class ComputationConstant
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    public decimal Value { get; set; }
    public string? Description { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}
