using Domain.Shared.Entities;
using Domain.Shared.Enums;

namespace Domain.Users;

public class User: TenantAuditableEntity<Guid>
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Surname { get; private set; }
    public string Email { get; private set; }
    public byte[] PasswordHash { get; private set; }
    public byte[] PasswordSalt { get; private set; }
    public Language Language { get; private set; }

    public User()
    {
    }

    public User(Guid id, Status status, string code, string name, string surname, string email, byte[] passwordHash, byte[] passwordSalt, Language language) : base(id, status)
    {
        Code = code;
        Name = name;
        Surname = surname;
        Email = email;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        Language = language;
    }

    public static User Create(string code,
                              string name,
                              string surname,
                              string email,
                              byte[] passwordHash,
                              byte[] passwordSalt,
                              Language language)
    {
        var user = new User(Guid.NewGuid(), Status.Active, code, name, surname, email, passwordHash, passwordSalt, language)
        {
            TenantCode = "",
            CreatedBy = "",
            CreatedAt = default,
            LastUpdatedBy = "",
            LastUpdatedAt = default
        };

        return user;
    }

    public void UpdatePassword(byte[] passwordHash, byte[] passwordSalt)
    {
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
    }
}
