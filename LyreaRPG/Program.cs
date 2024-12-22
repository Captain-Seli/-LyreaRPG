using System;
using LyreaRPG.Characters;
using LyreaRPG.Utils;
using LyreaRPG.World;

namespace LyreaRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize helpers
            AccountsHelper.Initialize();
            CharacterStorageHelper.Initialize();

            bool isRunning = true;

            while (isRunning)
            {
                // Display the welcome menu and handle account login or creation
                MenuHelper.DisplayWelcomeMenu(out Account account, out bool exitProgram);

                if (exitProgram)
                {
                    isRunning = false;
                    break; // Exit the program
                }

                if (account != null)
                {
                    // Load or create a character
                    Player player = MenuHelper.HandleCharacterMenu(account);

                    // Enter the main game menu if a character is loaded
                    if (player != null)
                    {
                        MenuHelper.DisplayMainMenu(account, player);
                    }
                }
            }

            Console.WriteLine("Thank you for playing Lyrea!");
        }
    }
}
