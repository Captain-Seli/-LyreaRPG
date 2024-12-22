using System;
using LyreaRPG.Characters;
using LyreaRPG.Items;

namespace LyreaRPG.Utils
{
    public static class EquipmentHelper
    {
        /// <summary>
        /// Initializes and returns a dictionary of equipment slots for a player.
        /// </summary>
        public static Dictionary<string, string> InitializeSlots()
        {
            // Define all possible equipment slots
            var slots = new Dictionary<string, string>
            {
                { "Head", null },
                { "Face", null },
                { "Neck", null },
                { "Chest", null },
                { "Hands", null },
                { "Waist", null },
                { "Pants", null },
                { "Left Arm", null },
                { "Right Arm", null },
                { "Left Leg", null },
                { "Right Leg", null },
                { "Feet", null },
                { "Main Hand", null },
                { "Offhand", null },
                { "Ring", null }
            };

            return slots;
        }
        public static void EquipItem(Player player, string itemName)
        {
            var item = player.Inventory.Find(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (item == null)
            {
                Console.WriteLine($"Item '{itemName}' not found in inventory.");
                return;
            }

            // Determine target slot(s) based on item type
            string targetSlot = item.Type switch
            {
                "1 Handed Weapon" => "Main Hand",
                "Shield" => "Offhand",
                "Necklace" => "Neck",
                "Ring" => "Ring",
                "Armor" => "Chest",
                "Helmet" => "Head",
                "Hat" => "Head",
                "Face" => "Face",
                "Pants" => "Pants",
                "Boots" => "Feet",
                "Gloves" => "Hands",
                "Belt" => "Waist",
                "BracerR" => "Right Arm",
                "BracerL" => "Left Arm",
                "LLeg" => "Left Leg",
                "RLeg" => "Right Leg",
                "2 Handed Weapon" => "2 Handed", // Special case for two-handed weapons
                _ => null
            };

            if (item.Type == "2 Handed Weapon")
            {
                if (player.EquipmentSlots["Main Hand"] != null || player.EquipmentSlots["Offhand"] != null)
                {
                    Console.WriteLine("You must unequip both Main Hand and Offhand to equip a two-handed weapon.");
                    return;
                }

                player.EquipmentSlots["Main Hand"] = item.Name;
                player.EquipmentSlots["Offhand"] = item.Name;
                player.Inventory.Remove(item);
                Console.WriteLine($"{item.Name} equipped to Main Hand and Offhand.");
                return;
            }

            if (targetSlot != null)
            {
                // Unequip existing item
                if (player.EquipmentSlots[targetSlot] != null)
                {
                    Console.WriteLine($"Unequipped {player.EquipmentSlots[targetSlot]} from {targetSlot}.");
                    player.AddItem(new Item(player.EquipmentSlots[targetSlot], "Unequipped item.", "Unequipped", 0));
                }

                // Equip new item
                player.EquipmentSlots[targetSlot] = item.Name;
                player.Inventory.Remove(item);
                Console.WriteLine($"{item.Name} equipped to {targetSlot}.");
            }
            else
            {
                Console.WriteLine("This item cannot be equipped.");
            }
        }

        public static void UnequipItem(Player player, string slot)
        {
            if (player.EquipmentSlots[slot] == null)
            {
                Console.WriteLine($"No item equipped in the {slot} slot.");
                return;
            }

            var itemName = player.EquipmentSlots[slot];
            player.AddItem(new Item(itemName, "Unequipped item.", "Unequipped", 0));
            player.EquipmentSlots[slot] = null;

            Console.WriteLine($"{itemName} unequipped from {slot}.");
        }

        public static void DisplayEquippedItems(Player player)
        {
            Console.WriteLine("Equipped Items:");
            foreach (var slot in player.EquipmentSlots)
            {
                Console.WriteLine($"{slot.Key}: {(slot.Value ?? "Empty")}");
            }
        }

        // New EquipItemSilently method
        public static void EquipItemSilently(Player player, string itemName)
        {
            var item = player.Inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (item == null)
            {
                return; // Silently fail if the item is not found
            }

            string targetSlot = item.Type switch
            {
                "1 Handed Weapon" => "Main Hand",
                "Shield" => "Offhand",
                "Necklace" => "Neck",
                "Ring" => "Ring",
                "Armor" => "Chest",
                "Helmet" => "Head",
                "Hat" => "Head",
                "Face" => "Face",
                "Pants" => "Pants",
                "Boots" => "Feet",
                "Gloves" => "Hands",
                "Belt" => "Waist",
                "BracerR" => "Left Arm",
                "BracerL" => "Right Arm",
                "LLeg" => "Left Leg",
                "RLeg" => "Right Leg",
                "2 Handed Weapon" => "2 Handed", // Special case for two-handed weapons
                _ => null
            };

            if (item.Type == "2 Handed Weapon")
            {
                if (player.EquipmentSlots["Main Hand"] != null || player.EquipmentSlots["Offhand"] != null)
                {
                    return; // Silently fail if both slots are occupied
                }

                player.EquipmentSlots["Main Hand"] = item.Name;
                player.EquipmentSlots["Offhand"] = item.Name;
                player.Inventory.Remove(item);
                return;
            }

            if (targetSlot != null)
            {
                if (player.EquipmentSlots[targetSlot] != null)
                {
                    player.AddItem(new Item(player.EquipmentSlots[targetSlot], "Unequipped item.", "Unequipped", 0));
                }

                player.EquipmentSlots[targetSlot] = item.Name;
                player.Inventory.Remove(item);
            }
        }
    }
}
