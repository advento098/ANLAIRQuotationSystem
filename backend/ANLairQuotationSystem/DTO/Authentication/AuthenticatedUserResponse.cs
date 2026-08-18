namespace ANLairQuotationSystem.DTO.Authentication;

public class AuthenticatedUserResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
