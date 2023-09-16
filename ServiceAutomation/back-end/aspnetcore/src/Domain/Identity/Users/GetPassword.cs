namespace Domain.Identity.Users;

public class GetPassword
{
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
}