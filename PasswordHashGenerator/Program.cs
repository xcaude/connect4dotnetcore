using Microsoft.AspNetCore.Identity;

class Program
{
    static void Main(string[] args)
    {
        var passwordHasher = new PasswordHasher<object>();
        var passwordHash = passwordHasher.HashPassword(null, "password3");

        // Output the hash
        Console.WriteLine(passwordHash);
    }
}