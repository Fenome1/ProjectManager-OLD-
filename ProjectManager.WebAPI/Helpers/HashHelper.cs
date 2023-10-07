using System.Security.Cryptography;
using System.Text;

namespace ProjectManager.WebAPI.Helpers;

public class HashHelper
{
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        using var sha256 = SHA256.Create();

        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha256.ComputeHash(bytes);
        var hash = Convert.ToBase64String(hashBytes);

        return hash == hashedPassword;
    }
}