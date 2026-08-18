namespace ANLairQuotationSystem.Entities;

public class UserSession
{
    public uint Id { get; set; }
    public uint UserId { get; set; }
    public required string RefreshToken { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateExpiring { get; set; }

    public User User { get; set; } = null!;


    public bool IsNotValid => !IsActive || DateTime.Now > DateExpiring;
}
