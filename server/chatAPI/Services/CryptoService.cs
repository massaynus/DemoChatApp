using System.Security.Cryptography;
using System.Text;

namespace chatAPI.Services;

public class CryptoService
{
    private const int SALT_SIZE = 16;
    private const int HASH_SIZE = 20;
    private const int ITTERATIONS = 10000;

    private const string HASH_ALGO = "SHA256";

    public string Hash(string password, int iterations = ITTERATIONS, string hashAlgorithmName = HASH_ALGO)
    {
        // Create salt
        byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);

        // Create hash
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, new HashAlgorithmName(hashAlgorithmName));
        var hash = pbkdf2.GetBytes(HASH_SIZE);

        // Combine salt and hash
        var hashBytes = new byte[SALT_SIZE + HASH_SIZE];
        Array.Copy(salt, 0, hashBytes, 0, SALT_SIZE);
        Array.Copy(hash, 0, hashBytes, SALT_SIZE, HASH_SIZE);

        // Convert to base64
        var base64Hash = Convert.ToBase64String(hashBytes);

        // Format hash with extra information
        return $"{hashAlgorithmName}${iterations}${base64Hash}";
    }

    public bool Verify(string base64Hash, string password)
    {
        // Retrieve hashing algorithm and itterations
        var parts = base64Hash.Split('$');
        var algo = parts[0];
        var itterations = int.Parse(parts[1]);
        var hash = parts[2];

        // Decode base64 to get the hash informations back
        var hashString = Encoding.UTF8.GetString(Convert.FromBase64String(hash));

        // Hash the supplied password
        var base64PasswordHash = Hash(password, itterations, algo);

        // Check for equality
        return base64Hash == base64PasswordHash;
    }
}