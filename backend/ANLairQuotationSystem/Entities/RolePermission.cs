namespace ANLAIRQuotationSystem.Entities;

public class RolePermission
{
    public uint RoleId { get; set; }
    public uint PermissionId { get; set; }

    public Role Role { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}
