using Domain.Shared.Entities;
using Domain.Shared.Enums;

namespace Domain.Users;

public class User: TenantAuditableEntity<Guid>
{
    public string Username { get; private set; }
    public string NormalizedUsername { get; private set; } 
    public Guid RoleId { get; private set; } 
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; private set; }
    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }
    public Language Language { get; private set; }

    public User()
    {
    }

    public User(Guid id,
                Status status,
                string userName,
                Guid roleId,
                string name,
                string surname,
                string email,
                byte[] passwordHash,
                byte[] passwordSalt,
                Language language) : base(id, status)
    {
        Username = userName;
        NormalizedUsername = Username.ToUpperInvariant();
        RoleId = roleId;
        Name = name;
        Surname = surname;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Language = language;
    }

    public static User Create(string userName,
                              Guid roleId,
                              string name,
                              string surname,
                              string email,
                              byte[] passwordHash,
                              byte[] passwordSalt,
                              Language language)
    {
        var user = new User(Guid.NewGuid(), Status.Active, userName, roleId, name, surname, email, passwordHash, passwordSalt, language);
        return user;
    }

    public void UpdatePassword(byte[] passwordHash, byte[] passwordSalt)
    {
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }
}
