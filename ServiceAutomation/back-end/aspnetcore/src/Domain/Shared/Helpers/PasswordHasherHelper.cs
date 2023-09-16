using System.Security.Cryptography;
using System.Text;

namespace Domain.Shared.Helpers;

public static class PasswordHasherHelper
{

    private static readonly int keySize = 64;
    private static readonly int iterations = 350000;
    private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

    public static void HashAndSaltPassword(string password, out byte[] hash, out byte[] salt)
    {

        salt = RandomNumberGenerator.GetBytes(keySize);

        hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),
                                         salt,
                                         iterations,
                                         hashAlgorithm,
                                         keySize);
    }

    public static bool VerifyPassword(string password, byte[] hash, byte[] salt)
    {

        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password,
                                                      salt,
                                                      iterations,
                                                      hashAlgorithm,
                                                      keySize);

        return CryptographicOperations.FixedTimeEquals(hashToCompare, hash);
    }
}
