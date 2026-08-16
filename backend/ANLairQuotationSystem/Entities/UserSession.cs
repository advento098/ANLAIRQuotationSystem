namespace ANLAIRQuotationSystem.Entities;

public class UserSession
{
    public uint Id { get; set; }
    public uint UserId { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateExpiring { get; set; }

    public User User { get; set; } = null!;
}
