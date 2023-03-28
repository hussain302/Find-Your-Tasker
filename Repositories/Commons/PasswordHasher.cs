using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        // Generate a random salt
        byte[] salt = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }

        // Compute the hash of the password with the salt
        byte[] hash = new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(20);

        // Combine the salt and hash into a single string
        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);
        string hashedPassword = Convert.ToBase64String(hashBytes);

        return hashedPassword;
    }

    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Convert the hashed password from a string to bytes
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);

        // Extract the salt and hash from the hashed password bytes
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);
        byte[] hash = new byte[20];
        Array.Copy(hashBytes, 16, hash, 0, 20);

        // Compute the hash of the password with the extracted salt
        byte[] computedHash = new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(20);

        // Compare the computed hash with the extracted hash
        for (int i = 0; i < 20; i++)
        {
            if (hash[i] != computedHash[i])
            {
                return false;
            }
        }
        return true;
    }
}
