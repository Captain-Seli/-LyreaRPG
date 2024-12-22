using System;
using System.IO;
using System.Text.Json;
using LyreaRPG.Characters;

namespace LyreaRPG.Utils
{
    public static class AccountsHelper
    {
        private const string SaveDirectory = "./Saves/Accounts";

        public static void Initialize()
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
        }

        public static bool CreateAccount(out string username)
        {
            Console.Clear();
            Console.WriteLine("Create an Account");

            Console.Write("Enter a username: ");
            username = Console.ReadLine();

            Console.Write("Enter a password: ");
            string password = Console.ReadLine();

            string filePath = GetAccountFilePath(username);

            if (File.Exists(filePath))
            {
                Console.WriteLine("An account with this username already exists.");
                Console.WriteLine("Press any key to return.");
                Console.ReadKey();
                return false;
            }

            var account = new Account { Username = username, Password = password };
            SaveAccountToFile(account);

            Console.WriteLine("Account created successfully!");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();

            return true;
        }

        public static Account Login()
        {
            Console.Clear();
            Console.WriteLine("Login to your Account");

            Console.Write("Enter your username: ");
            string username = Console.ReadLine();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            string filePath = GetAccountFilePath(username);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Account not found. Press any key to return.");
                Console.ReadKey();
                return null;
            }

            var account = LoadAccountFromFile(filePath);

            if (account.Password != password)
            {
                Console.WriteLine("Incorrect password. Press any key to return.");
                Console.ReadKey();
                return null;
            }

            Console.WriteLine("Login successful!");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            return account;
        }

        private static string GetAccountFilePath(string username)
        {
            return Path.Combine(SaveDirectory, $"{username}.json");
        }

        private static void SaveAccountToFile(Account account)
        {
            string filePath = GetAccountFilePath(account.Username);
            string json = JsonSerializer.Serialize(account);
            File.WriteAllText(filePath, json);
        }

        private static Account LoadAccountFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Account>(json);
        }
    }

    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CharacterFile { get; set; } // Path to the character save file
    }
}
