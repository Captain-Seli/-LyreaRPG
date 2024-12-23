using System;
using System.Collections.Generic;
using LyreaRPG.Characters;
using LyreaRPG.Items;

namespace LyreaRPG.Utils
{
    public static class POIActionsHelper
    {
        public static void HandlePOIActions(string poiType, Player player)
        {
            switch (poiType.ToLower())
            {
                case "wilderness":
                    WildernessActions(player);
                    break;
                case "shop":
                    ShopActions(player);
                    break;
                case "inn":
                    InnActions(player);
                    break;
                case "house":
                case "park":
                    NPCInteractionActions(player);
                    break;
                case "dungeon":
                    DungeonActions(player);
                    break;
                case "forge":
                    ForgeActions(player);
                    break;
                case "travel":
                    TravelActions(player);
                    break;
                case "government":
                    GovernmentActions(player);
                    break;
                case "military":
                    MilitaryActions(player);
                    break;
                case "club":
                    ClubActions(player);
                    break;
                case "residential":
                    ResidentialActions(player);
                    break;
                case "marina":
                    MarinaActions(player);
                    break;
                case "trading_company":
                    TradingCompanyActions(player);
                    break;
                case "blacksmith":
                    BlacksmithActions(player);
                    break;
                case "chandlery":
                    ChandleryActions(player);
                    break;
                case "gunsmith":
                    GunsmithActions(player);
                    break;
                case "ruins":
                    RuinsActions(player);
                    break;
                case "flophouse":
                    FlophouseActions(player);
                    break;
                default:
                    Console.WriteLine("There are no specific actions for this Point of Interest.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    break;
            }
        }

        // Wilderness Actions
        private static void WildernessActions(Player player)
        {
            Console.WriteLine("1. Hunt for food");
            Console.WriteLine("2. Gather materials");
            Console.WriteLine("3. Explore the area");
            Console.WriteLine("Choose an action:");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.WriteLine("You successfully hunted some game. +5 Food added to inventory.");
                    player.AddItem(new Item("Food", "Freshly hunted game meat.", "Consumable", 5, 5));
                    break;
                case "2":
                    Console.WriteLine("You gathered rare herbs. +3 Herbs added to inventory.");
                    player.AddItem(new Item("Herbs", "Medicinal herbs for crafting or selling.", "Material", 10, 3));
                    break;
                case "3":
                    Console.WriteLine("You explored the wilderness and found a hidden stash. +10 Gold added.");
                    player.EarnGold(10);
                    break;
                default:
                    Console.WriteLine("Invalid action. Returning to the POI menu.");
                    break;
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        // Shop Actions
        private static void ShopActions(Player player)
        {
            var shopItems = new List<Item>
            {
                new Item("Sword", "A basic steel sword.", "Weapon", 50),
                new Item("Shield", "A sturdy wooden shield.", "Armor", 30),
                new Item("Health Potion", "Restores 20 HP.", "Consumable", 10)
            };

            Console.WriteLine("Welcome to the shop. Here are the items for sale:");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {shopItems[i]}");
            }
            Console.WriteLine("Enter the number of the item you wish to buy, or 0 to leave:");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= shopItems.Count)
            {
                var selectedItem = shopItems[choice - 1];
                if (player.Money >= selectedItem.Value)
                {
                    player.SpendGold(selectedItem.Value);
                    player.AddItem(new Item(selectedItem.Name, selectedItem.Description, selectedItem.Type, selectedItem.Value));
                    Console.WriteLine($"You purchased {selectedItem.Name} for {selectedItem.Value} Gold.");
                }
                else
                {
                    Console.WriteLine("You don't have enough gold to buy this item.");
                }
            }
            else if (choice == 0)
            {
                Console.WriteLine("You leave the shop.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        // Inn Actions
        private static void InnActions(Player player)
        {
            Console.WriteLine("1. Rent a room (5 Gold)");
            Console.WriteLine("2. Order food (3 Gold)");
            Console.WriteLine("3. Talk to patrons");
            Console.WriteLine("Choose an action:");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    if (player.SpendGold(5))
                    {
                        Console.WriteLine("You rented a room and rested. Fully healed!");
                        // player.RestoreHealth();
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough gold.");
                    }
                    break;
                case "2":
                    if (player.SpendGold(3))
                    {
                        Console.WriteLine("You ordered a hearty meal. Health restored by 20.");
                        // player.Heal(20);
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough gold.");
                    }
                    break;
                case "3":
                    Console.WriteLine("You talk to the patrons and learn a rumor about a hidden treasure.");
                    break;
                default:
                    Console.WriteLine("Invalid action.");
                    break;
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        // Other POI Types
        private static void NPCInteractionActions(Player player)
        {
            Console.WriteLine("You interact with the local NPCs.");
            Console.WriteLine("You might receive quests or learn rumors.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void TravelActions(Player player)
        {
            Console.WriteLine("You can book passage on a ship or hire transportation.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void GovernmentActions(Player player)
        {
            Console.WriteLine("You interact with government officials. Politics may play a role here.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void MilitaryActions(Player player)
        {
            Console.WriteLine("You visit the military base. Training and missions may be available.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void ClubActions(Player player)
        {
            Console.WriteLine("You mingle with the aristocracy. Networking opportunities abound.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void ResidentialActions(Player player)
        {
            Console.WriteLine("This is a quiet residential area. Not much happens here.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void MarinaActions(Player player)
        {
            Console.WriteLine("You admire the yachts and expensive watercraft. Perhaps a future goal?");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void TradingCompanyActions(Player player)
        {
            Console.WriteLine("You browse exotic goods from faraway lands.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void BlacksmithActions(Player player)
        {
            Console.WriteLine("You can forge weapons or repair your gear here.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void ChandleryActions(Player player)
        {
            Console.WriteLine("You stock up on nautical supplies for your next voyage.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }


        private static void RuinsActions(Player player)
        {
            Console.WriteLine("You explore ancient ruins. Danger and treasure await.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void FlophouseActions(Player player)
        {
            Console.WriteLine("You find a rough place to rest. Comfort is not guaranteed.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void DungeonActions(Player player)
        {
            Console.WriteLine("The dungeon is dark and damp. Better turn back for now.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void ForgeActions(Player player)
        {
            Console.WriteLine("You can use the forge to smelt metal and create metal items.");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        private static void GunsmithActions(Player player)
        {
            var shopItems = new List<Item>
            {
                new Item("Flintlock Pistol", "A basic flintlock pistol.", "Weapon", 50),
                new Item("Flintlock shot", "A bag of 10 flintlock bullets", "Ammo", 30),
                new Item("Gun Powder", "Required to fire a flintlock weapon", "Consumable", 10)
            };

            Console.WriteLine("Welcome to the shop. Here are the items for sale:");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {shopItems[i]}");
            }
            Console.WriteLine("Enter the number of the item you wish to buy, or 0 to leave:");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int choice) && choice > 0 && choice <= shopItems.Count)
            {
                var selectedItem = shopItems[choice - 1];
                if (player.Money >= selectedItem.Value)
                {
                    player.SpendGold(selectedItem.Value);
                    player.AddItem(new Item(selectedItem.Name, selectedItem.Description, selectedItem.Type, selectedItem.Value));
                    Console.WriteLine($"You purchased {selectedItem.Name} for {selectedItem.Value} Gold.");
                }
                else
                {
                    Console.WriteLine("You don't have enough gold to buy this item.");
                }
            }
            else if (choice == 0)
            {
                Console.WriteLine("You leave the shop.");
            }
            else
            {
                Console.WriteLine("Invalid choice.");
            }

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }
    }
}
