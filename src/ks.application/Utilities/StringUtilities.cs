using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ks.application.Utilities;
public static class StringUtilities
{
    public static (string Password, byte[] Salt) HashPassword(this string password,
        byte[]? salt = null)
    {

        salt ??= RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
        return (hashed, salt);
    }
    
}