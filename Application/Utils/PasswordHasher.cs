using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace InmobiliariaAPI.Application.Utils;

public static class PasswordHasher
{
    public static string HashPassword(string password, string salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes(salt),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 256 / 8));
        
        return hashed;
    }
    
    public static bool VerifyPassword(string password, string storedHash, string salt)
    {
        string hashed = HashPassword(password, salt);
        return hashed == storedHash;
    }
}