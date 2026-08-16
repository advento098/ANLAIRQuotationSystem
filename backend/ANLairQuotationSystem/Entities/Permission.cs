namespace ANLAIRQuotationSystem.Entities;

public class Permission
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = [];
}
