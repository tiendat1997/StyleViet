using StyleViet.Service.Helper;
using System;

namespace StyleViet.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var testPassword = "tiendat";
            var hashed = HashingHelper.GenerateHash(testPassword);
            var hashed2 = HashingHelper.GenerateSHA256Hash(testPassword);
            Console.WriteLine($"Hashed {hashed}");
            Console.WriteLine($"Hashed {hashed2}");
            Console.ReadLine();
        }
    }
}
