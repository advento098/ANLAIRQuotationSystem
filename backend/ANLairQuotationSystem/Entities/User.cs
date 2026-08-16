using ANLairQuotationSystem.Entities;

namespace ANLAIRQuotationSystem.Entities;

public class User
{
    public uint Id { get; set; }
    public required string PublicId { get; set; }
    public uint RoleId { get; set; }
    public required string Firstname { get; set; }
    public string? Middlename { get; set; }
    public required string Surname { get; set; }
    public string? ContactNumber { get; set; }
    public required string Email { get; set; }
    public UserStatus Status { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    public Role Role { get; set; } = null!;
    public ICollection<UserSession> UserSessions { get; set; } = [];
    public ICollection<Client> CreatedClients { get; set; } = [];
    public ICollection<Project> CreatedProjects { get; set; } = [];
    public ICollection<ProjectRequestProof> CreatedProjectRequestProofs { get; set; } = [];

    public enum UserStatus
    {
        ACTIVE,
        INACTIVE,
        SUSPENDED
    }
}
