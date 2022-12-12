using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public class LoginService
{
    // TODO: Gem 'users' i database, ikke her i koden 😊
    public record UserRecord(string username, string hashedPassword, string salt, string[] roles);

    private List<UserRecord> userDatabase = new List<UserRecord>();

    public LoginService() {;
        // I praksis laver vi ikke brugerne hver gang, men gemmer dem én gang for
        // alle nede i en database.
        CreateLogin("kristian", "banankage", new string[] {"admin"});
        CreateLogin("kell", "password123", new string[] {"user"});
        CreateLogin("klaus", "secret", new string[] {"user"});
    }

    public UserRecord CreateLogin(string username, string password, string[] roles)
    {
        // lav et 128-bit salt 
        byte[] saltBytes = new byte[128 / 8];
        RandomNumberGenerator rng = RandomNumberGenerator.Create();
        rng.GetNonZeroBytes(saltBytes);
        string salt = Convert.ToBase64String(saltBytes);

        // lav en 512-bit hash med HMACSHA512
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(salt),
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100000,
            numBytesRequested: 512 / 8));

        var record = new UserRecord(username, hashed, salt, roles);
        userDatabase.Add(record);
        return record;
    }

    public bool ValidateLogin(string username, string password)
    {
        Console.WriteLine("ValidateLogin");
        UserRecord? record = userDatabase.Find(user => user.username == username);
        Console.WriteLine(record);
        if (record == null) {
            Console.WriteLine($"Login failed for {username}/{password}");
            return false;
        }

        // lav en 512-bit hash med HMACSHA512
        string newHashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Convert.FromBase64String(record.salt),
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 100000,
            numBytesRequested: 512 / 8));

        if (newHashed != record.hashedPassword) {
            Console.WriteLine($"Login failed for {username}/{password}");
            return false;
        } else {
            Console.WriteLine($"Login succeded for {username}/{password}");
            return true;
        }
    }
}

