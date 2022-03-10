using System.Security.Cryptography;
using System.Text;

namespace chatAPI.Services;

/// <summary>
/// Handles everything password/encryption related in the app
/// </summary>
public class CryptoService
{
    private class HashInfo
    {
        public string Algorithm { get; set; }
        public int Itterations { get; set; }
        public string Base64Hash { get; set; }
        public byte[] Bytes { get; set; }
        public byte[] HashBytes { get; set; }
        public byte[] Salt { get; set; }
    }

    private const int SALT_SIZE = 16;
    private const int HASH_SIZE = 20;
    private const int ITTERATIONS = 10000;

    private const string HASH_ALGO = "SHA256";

    private readonly ILogger<CryptoService> _logger;

    public CryptoService(ILogger<CryptoService> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Hash a password
    /// </summary>
    /// <param name="password">the password to be hashed</param>
    /// <param name="iterations">itteration for KD operation</param>
    /// <param name="hashAlgorithmName">the hashing algorithm to be used</param>
    /// <returns>string</returns>
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

    /// <summary>
    /// Verifies if a hash and a password match or not
    /// </summary>
    /// <param name="base64Hash">the hashto be challenged</param>
    /// <param name="password">the challenging string</param>
    /// <returns></returns>
    public bool Verify(string base64Hash, string password)
    {
        // Get the original hash's informations
        var ogHashInfo = getHashInfo(base64Hash);
        var ogBytes = ogHashInfo.HashBytes;
        var ogSalt = ogHashInfo.Salt;

        // Hash the supplied password using the og's SALT
        var pbkdf2 = new Rfc2898DeriveBytes(password, ogSalt, ogHashInfo.Itterations, new HashAlgorithmName(ogHashInfo.Algorithm));
        var newBytes = pbkdf2.GetBytes(HASH_SIZE);

        _logger.LogInformation($"{bytesToString(ogBytes)} vs \n{bytesToString(newBytes)}");

        // Look for any unmatching byte
        for (int i = 0; i < HASH_SIZE; i++)
            if (ogBytes[i] != newBytes[i])
                return false;

        return true;
    }

    private string bytesToString(byte[] arr)
    {
        return string.Join('-', arr.Select(b => b.ToString()));
    }

    private HashInfo getHashInfo(string base64Hash)
    {
        // Retrieve hashing algorithm and itterations
        var parts = base64Hash.Split('$');
        var algo = parts[0];
        var itterations = int.Parse(parts[1]);
        var hash = parts[2];

        // Decode base64 to get the hash informations back
        var hashBytes = Convert.FromBase64String(hash);

        return new HashInfo
        {
            Algorithm = algo,
            Itterations = itterations,
            Base64Hash = hash,
            Bytes = hashBytes,
            HashBytes = hashBytes[SALT_SIZE..],
            Salt = hashBytes[..SALT_SIZE]
        };
    }
}