using System;
using LyreaRPG.Characters;
using LyreaRPG.Utils;
using LyreaRPG.World;

namespace LyreaRPG.Utils
{
    public static class MenuHelper
    {
        public static void DisplayWelcomeMenu(out Account account, out bool exitProgram)
        {
            account = null;
            exitProgram = false;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Lyrea!");
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
                            return;
                        }
                        break;
                    case "2":
                        account = AccountsHelper.Login();
                        if (account != null)
                        {
                            return;
                        }
                        break;
                    case "3":
                        exitProgram = true;
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static Player StartGame(string username)
        {
            Console.Clear();
            Console.WriteLine("Choose your race:");

            var races = new string[] { "Human", "Sinai", "Carcharia", "Elf", "Molluska", "Saltatrix", "Crabaxi" };
            for (int i = 0; i < races.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {races[i]}");
            }

            bool validChoice = false;
            int raceChoice = 0;
            bool forceRoll = false;

            while (!validChoice)
            {
                Console.WriteLine("Enter the number of your chosen race, or type 'info' followed by the race number for a description (e.g., 'info 3'):");

                string input = Console.ReadLine();

                if (input.StartsWith("info"))
                {
                    if (int.TryParse(input.Split(' ')[1], out int infoChoice) && infoChoice > 0 && infoChoice <= races.Length)
                    {
                        ShowRaceInfo(infoChoice - 1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                }
                else if (input.EndsWith("*"))
                {
                    string trimmedInput = input.TrimEnd('*');
                    if (int.TryParse(trimmedInput, out raceChoice) && raceChoice > 0 && raceChoice <= races.Length)
                    {
                        validChoice = true;
                        forceRoll = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Try again.");
                    }
                }
                else if (int.TryParse(input, out raceChoice) && raceChoice > 0 && raceChoice <= races.Length)
                {
                    validChoice = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                }
            }

            Console.Clear();
            Console.Write("Enter your character's name: ");
            string playerName = Console.ReadLine();

            Player player = raceChoice switch
            {
                1 => new Human(playerName, forceRoll),
                2 => new Sinai(playerName),
                3 => new Carcharia(playerName, forceRoll),
                4 => new Elf(playerName),
                5 => new Molluska(playerName, forceRoll),
                6 => new Saltatrix(playerName, forceRoll),
                7 => new Crabaxi(playerName, forceRoll),
                _ => throw new ArgumentException("Invalid race selection."),
            };

            Console.WriteLine("Your character has been created!");
            player.SetLocation("The Sun Spur", "Port Waveward"); // Default starting location
            player.DisplayStats();

            // Display starting gold and inventory
            Console.WriteLine($"Starting Gold: {player.Gold}");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            // Save character immediately upon creation
            CharacterStorageHelper.SaveCharacter(username, player);

            Console.WriteLine("Character saved successfully!");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

            return player;
        }

        private static void ShowRaceInfo(int raceIndex)
        {
            string[] raceDescriptions = {
                "Human: Lyrea's most common race. Humans have 3 nations: The Kingdom of Azon, The Republic of Kyran, and the Empire of Venia. They are known for their adaptability and resourcefulness, balanced in all stats, and they excel at forming societies and mastering diverse skills.",
                "Sinai: A mysterious race of lizard-like beings originating from the Jungles of Venia. Sinai are known for their agility and wisdom, thriving in warm environments. They value secrecy and have a natural affinity for Kaida, a form of channeling.",
                "Carcharia: The formidable shark-people of the southern coastal regions and islands. Renowned for their strength and resilience, they dominate melee combat. Carcharia society is diverse, with some tribes focusing on trade and others on warrior traditions tied to the ocean's raw power.",
                "Elf: Reclusive descendants of the ancient faery tribes, deeply connected to Kaido, and energy of the natural world. Elves excel in lore and weaving, combining intelligence and dexterity to master the arcane and live harmoniously with nature.",
                "Molluska: A unique race resembling octopus or squid humanoids, native to the Emerald Empire, but often found in underwater cities or coastal areas around Lyrea. They are nimble and charismatic, thriving in social settings and combat. Molluska possess a keen ability to adapt and are often seen as natural diplomats and traders.",
                "Saltatrix: Small, agile fish-like beings native to the Emerald Empire. Saltatrix are masters of stealth and survival, using their environment to their advantage. Their quick reflexes and keen senses make them excellent hunters and scouts.",
                "Crabaxi: Hardy, crab-like warriors with natural armor and immense strength. They are often found in coastal and reef environments within the Emerald Empire, excelling as craftsmen and fighters. Crabaxi are known for their resilience and their ability to build complex structures with limited resources."
            };

            Console.WriteLine(raceDescriptions[raceIndex]);
            Console.WriteLine("Press any key to return to race selection.");
            Console.ReadKey();
        }

        public static void DisplayMainMenu(Account account, Player player)
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.Clear();
                Console.WriteLine("1. Explore The Sun Spur");
                Console.WriteLine("2. Inventory Menu");
                Console.WriteLine("3. View Stats and Skills");
                Console.WriteLine("4. Save Character");
                Console.WriteLine("5. Logout");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Choose an option:");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        var sunSpur = WorldSetup.InitializeSunSpur();
                        LocationsHelper.ExploreRegion(sunSpur, player);
                        break;
                    case "2":
                        DisplayInventoryMenu(player);
                        break;
                    case "3":
                        ActionsHelper.ShowStats(player);
                        break;
                    case "4":
                        CharacterStorageHelper.SaveCharacter(account.Username, player);
                        break;
                    case "5":
                        Console.WriteLine("Logging out...");
                        account = null;
                        player = null;
                        exitMenu = true;
                        break;
                    case "6":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void DisplayInventoryMenu(Player player)
        {
            bool exitInventory = false;

            while (!exitInventory)
            {
                Console.Clear();
                ActionsHelper.ShowInventory(player);

                Console.WriteLine("\nActions:");
                Console.WriteLine("1. Equip Item");
                Console.WriteLine("2. Back to Main Menu");
                Console.WriteLine("Choose an action:");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Select an item to equip:");
                        player.DisplayInventory();

                        Console.WriteLine("\nEnter the name of the item to equip:");
                        string itemName = Console.ReadLine();
                        EquipmentHelper.EquipItem(player, itemName);

                        Console.WriteLine("Press any key to return to the inventory menu.");
                        Console.ReadKey();
                        break;
                    case "2":
                        exitInventory = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static Player HandleCharacterMenu(Account account)
        {
            Console.Clear();
            var characters = CharacterStorageHelper.LoadCharacters(account.Username);

            if (characters.Count == 0)
            {
                Console.WriteLine("No saved characters found. Would you like to create a new character? (y/n)");
                string input = Console.ReadLine()?.ToLower();

                if (input == "y" || input == "yes")
                {
                    Player newPlayer = StartGame(account.Username);
                    CharacterStorageHelper.SaveCharacter(account.Username, newPlayer);
                    return newPlayer;
                }
                else
                {
                    Console.WriteLine("No character loaded. Returning to the main menu.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    return null;
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
                        Player selectedPlayer = characters[choice - 1];
                        Console.WriteLine($"Character '{selectedPlayer.Name}' loaded successfully!");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        return selectedPlayer;
                    }
                    else if (choice == characters.Count + 1)
                    {
                        Player newPlayer = StartGame(account.Username);
                        CharacterStorageHelper.SaveCharacter(account.Username, newPlayer);
                        return newPlayer;
                    }
                }

                Console.WriteLine("Invalid input. Returning to main menu.");
                Console.ReadKey();
                return null;
            }
        }
    }
}
