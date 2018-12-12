using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Service.Helper
{
    public class HashingHelper
    {
        public static string GenerateHash(string input)
        {
            string salt = CreateSalt(7);
            string hashed = GenerateSHA256Hash(input, salt);
            return hashed; 
        }
        private static string CreateSalt(int size)
        {
            // random provider 
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buffer = new byte[size];
            rng.GetBytes(buffer);
            var result = Convert.ToBase64String(buffer);
            return result.Substring(0,size);
        }        
        public static string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256HashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            var hashed = Convert.ToBase64String(hash);
            return hashed.Insert(hashed.Length, salt);                        
        }        
        public static string ValidateHash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256HashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);                        
        }
        public static string GenerateSHA256Hash(string input)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);
            System.Security.Cryptography.SHA256Managed sha256HashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }
    }
}
