using System;
using LyreaRPG.Characters;
using LyreaRPG.Utils;
using LyreaRPG.World;

namespace LyreaRPG
{
    class Program
    {
        static Account account = null; // Current logged-in account
        static Player player = null;   // Current loaded character

        static void Main(string[] args)
        {
            AccountsHelper.Initialize();
            CharacterStorageHelper.Initialize();

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Lyrea!");

                if (account == null)
                {
                    Console.WriteLine("1. Create Account");
                    Console.WriteLine("2. Login");
                    Console.WriteLine("3. Exit");
                    Console.WriteLine("Choose an option:");

                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            if (AccountsHelper.CreateAccount(out string newUsername))
                            {
                                account = AccountsHelper.Login(); // Automatically log into the new account
                                HandleCharacterCreationForAccount();
                            }
                            break;
                        case "2":
                            account = AccountsHelper.Login();
                            if (account != null)
                            {
                                HandleCharacterLoading();
                            }
                            break;
                        case "3":
                            isRunning = false;
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Press any key to try again.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("1. Explore The Sun Spur");
                    Console.WriteLine("2. View Inventory");
                    Console.WriteLine("8. Equip Item");
                    Console.WriteLine("3. View Stats and Skills");
                    Console.WriteLine("4. Where am I?");
                    Console.WriteLine("5. Save Character");
                    Console.WriteLine("6. Logout");
                    Console.WriteLine("7. Exit");
                    Console.WriteLine("Choose an option:");

                    string input = Console.ReadLine();
                    switch (input)
                    {
                        case "1":
                            if (player != null)
                            {
                                var sunSpur = WorldSetup.InitializeSunSpur();
                                LocationsHelper.ExploreRegion(sunSpur, player);
                            }
                            else
                            {
                                Console.WriteLine("No character loaded.");
                                Console.ReadKey();
                            }
                            break;
                        case "2":
                            ActionsHelper.ShowInventory(player);
                            break;
                        case "3":
                            ActionsHelper.ShowStats(player);
                            break;
                        case "4":
                            ActionsHelper.ShowCurrentLocation(player);
                            break;
                        case "5":
                            if (player != null && account != null)
                            {
                                CharacterStorageHelper.SaveCharacter(account.Username, player);
                            }
                            else
                            {
                                Console.WriteLine("No character or account found to save.");
                                Console.ReadKey();
                            }
                            break;
                        case "6":
                            LogoutPlayer();
                            break;
                        case "7":
                            isRunning = false;
                            break;
                        case "8":
                            if (player != null)
                            {
                                MenuHelper.DisplayEquipMenu(player);
                            }
                            else
                            {
                                Console.WriteLine("No character loaded.");
                                Console.ReadKey();
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Press any key to try again.");
                            Console.ReadKey();
                            break;
                    }
                }

                static void LogoutPlayer()
                {
                    if (player != null && account != null)
                    {
                        Console.WriteLine("Saving your progress before logging out...");
                        CharacterStorageHelper.SaveCharacter(account.Username, player);
                        Console.WriteLine("Progress saved successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No character or account found to save.");
                    }

                    account = null;
                    player = null;

                    Console.WriteLine("You have been logged out. Press any key to return to the main menu.");
                    Console.ReadKey();
                }
            }
        }

        private static void HandleCharacterCreationForAccount()
        {
            Console.WriteLine("Would you like to create a new character? (y/n)");
            string input = Console.ReadLine().ToLower();

            if (input == "y" || input == "yes")
            {
                player = MenuHelper.StartGame(account.Username);
                CharacterStorageHelper.SaveCharacter(account.Username, player);
            }
            else
            {
                Console.WriteLine("No character created. You can create one later.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }

        private static void HandleCharacterLoading()
        {
            Console.Clear();
            var characters = CharacterStorageHelper.LoadCharacters(account.Username);

            if (characters.Count == 0)
            {
                Console.WriteLine("No saved characters found. Would you like to create a new character? (y/n)");
                string input = Console.ReadLine()?.ToLower();

                if (input == "y" || input == "yes")
                {
                    player = MenuHelper.StartGame(account.Username);
                    CharacterStorageHelper.SaveCharacter(account.Username, player);
                }
                else
                {
                    Console.WriteLine("No character loaded. Exiting to the main menu.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Saved Characters:");
                for (int i = 0; i < characters.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {characters[i].Name}");
                }

                Console.WriteLine($"{characters.Count + 1}. Create a New Character");

                Console.WriteLine("Choose a character to load or create a new one:");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice > 0 && choice <= characters.Count)
                    {
                        player = characters[choice - 1];
                        Console.WriteLine($"Character '{player.Name}' loaded successfully!");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                    }
                    else if (choice == characters.Count + 1)
                    {
                        player = MenuHelper.StartGame(account.Username);
                        CharacterStorageHelper.SaveCharacter(account.Username, player);
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Returning to main menu.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Returning to main menu.");
                    Console.ReadKey();
                }
            }
        }

    }
}
