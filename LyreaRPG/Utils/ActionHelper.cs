using System;
using LyreaRPG.Characters;

namespace LyreaRPG.Utils
{
    public static class ActionsHelper
    {
        public static void ShowInventory(Player player)
        {
            Console.Clear();
            player?.DisplayInventory(); // Safeguard if player is null
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        public static void ShowStats(Player player)
        {
            Console.Clear();
            player?.DisplayStats(); // Safeguard if player is null
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        public static void ShowCurrentLocation(Player player)
        {
            Console.Clear();
            if (player != null)
            {
                Console.WriteLine($"{player.Name} is currently standing at {player.CurrentPOI} in {player.CurrentLocation}, within {player.CurrentRegion}.");
            }
            else
            {
                Console.WriteLine("Location tracking is unavailable without a character.");
            }
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        public static void EquipItem(Player player)
        {
            Console.Clear();
            Console.WriteLine("Select an item to equip:");
            player.DisplayInventory();

            Console.WriteLine("\nEnter the name of the item to equip:");
            string itemName = Console.ReadLine();
            EquipmentHelper.EquipItem(player, itemName);

            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }
    }
}
