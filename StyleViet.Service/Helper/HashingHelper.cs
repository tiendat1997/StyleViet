using System;
using System.Collections.Generic;
using System.Text;

namespace StyleViet.Service.Helper
{
    public class HashingHelper
    {
        public static Tuple<string,string> GenerateHash(string input)
        {
            string salt = CreateSalt(7);
            string hashed = GenerateSHA256Hash(input, salt);
            return new Tuple<string, string>(hashed,salt); 
        }
        private static string CreateSalt(int size)
        {
            // random provider 
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buffer = new byte[size];
            rng.GetBytes(buffer);
            return Convert.ToBase64String(buffer);            
        }        
        public static string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256HashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);            
        }        
        public static bool ValidateHash(string pass,string input, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256HashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            return pass.Equals(Convert.ToBase64String(hash));                        
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
