using System.Collections.Generic;
using LyreaRPG.Items;

namespace LyreaRPG.Utils
{
    public static class ItemHelper
    {
        // Weapons
        public static readonly Item FlintlockPistol = new Item(
            "Flintlock Pistol", // Name
            "A reliable sidearm for close-range encounters.", // Description
            "1 Handed Weapon", // Type
            30,// Value
            1, // Quantity
            false, // isAmmo
            5 // Weight
        );

        public static readonly Item FlintlockMusket = new Item(
            "Flintlock Musket",
            "A long-barreled weapon for precision at range.",
            "2 Handed Weapon",
            50,
            1,
            false,
            10
        );

        public static readonly Item DoubleBarrelBlunderbuss = new Item(
            "Double-Barrel Blunderbuss",
            "A devastating scattergun capable of firing two shots at once.",
            "2 Handed Weapon",
            100,
            1,
            false,
            15
        );

        // Armor
        public static readonly Item SailorsShirt = new Item(
            "Sailor's Shirt",
            "A simple, durable shirt often worn by sailors.",
            "Armor",
            5,
            1,
            false,
            2
        );
        public static readonly Item SailorsPants = new Item(
            "Sailor Pants",
            "A simple, durable pair of pants, often worn by sailors.",
            "Pants",
            5,
            1,
            false,
            2
        );
        public static readonly Item TricornerHat = new Item(
            "Tricorner Hat",
            "A worn three pointed hat. Peak fashion for mariners.",
            "Hat",
            5,
            1,
            false,
            1
        );
        public static readonly Item LeatherGloves = new Item(
            "Leather Gloves",
            "A worn pair of mariner's gloves.",
            "Gloves",
            5,
            1,
            false,
            1
        );
        public static readonly Item LeatherBelt = new Item(
            "Leather Belt",
            "A simple leather belt.",
            "Belt",
            5,
            1,
            false,
            1
        );
        public static readonly Item LeatherBoots = new Item(
            "Leather Boots",
            "A pair of sturdy leather boots.",
            "Boots",
            10,
            1,
            false,
            3
        );

        // Shields
        public static readonly Item WoodenShield = new Item(
            "Wooden Shield",
            "A basic wooden shield for blocking attacks.",
            "Shield",
            15,
            1,
            false,
            8
        );

        // Rings
        public static readonly Item BrassRing = new Item(
            "Brass Ring",
            "A simple brass ring, common among sailors.",
            "Ring",
            2,
            1,
            false,
            0.1
        );

        // Tools
        public static readonly Item GunpowderHorn = new Item(
            "Gunpowder Horn",
            "Stores enough gunpowder for 5 reloads.",
            "Tool",
            5,
            1,
            false,
            2
        );

        // Ammo
        public static readonly Item ArmorPiercingShot = new Item(
            "Armor-Piercing Shot",
            "Hardened lead balls designed to punch through heavy armor.",
            "Ammo",
            15,
            10,
            true,
            1
        );

        public static readonly Item IncendiaryGrapeshot = new Item(
            "Incendiary Grapeshot",
            "Adds fire damage to scattergun blasts.",
            "Ammo",
            20,
            5,
            true,
            1
        );

        // Consumables
        public static readonly Item HealthPotion = new Item(
            "Health Potion",
            "Restores 20 HP.",
            "Potion",
            10,
            1,
            false,
            0.5
        );

        // Currency
        public static readonly Item GoldCoin = new Item(
            "Gold Coin",
            "Worth 100 silver coins, or 1000 copper coins in the Kingdom of Azon",
            "Currency",
            1,
            1,
            false,
            0.01
        );

        public static readonly Item SilverCoin = new Item(
            "Silver Coin",
            "Worth 100 copper coins in the Kingdom of Azon.",
            "Currency",
            1,
            1,
            false,
            0.01
        );
        public static readonly Item CopperCoin = new Item(
            "Copper Coin",
            "The basic unit of currency in the Kingdom of Azon.",
            "Currency",
            1,
            1,
            false,
            0.01
            );
        public static readonly Item SenkuCoin = new Item(
            "Senku",
            "A coin of gold silver mix from the Empire of Venia.",
            "Currency",
            1,
            1,
            false,
            0.01
            );

        // Materials
        public static readonly Item RawIronOre = new Item(
            "Iron Ore",
            "Unprocessed iron ore from the mines.",
            "Raw Material",
            3,
            1,
            false,
            10
        );

        public static readonly Item ProcessedSteelIngot = new Item(
            "Steel Ingot",
            "Refined steel, ready for forging.",
            "Processed Material",
            8,
            1,
            false,
            12
        );

        // Helper Methods for Item Groups
        public static List<Item> GetAllWeapons()
        {
            return new List<Item>
            {
                FlintlockPistol,
                FlintlockMusket,
                DoubleBarrelBlunderbuss
            };
        }

        public static List<Item> GetAllArmor()
        {
            return new List<Item>
            {
                SailorsShirt,
                LeatherBoots
            };
        }

        public static List<Item> GetAllShields()
        {
            return new List<Item>
            {
                WoodenShield
            };
        }

        public static List<Item> GetAllRings()
        {
            return new List<Item>
            {
                BrassRing
            };
        }

        public static List<Item> GetAllTools()
        {
            return new List<Item>
            {
                GunpowderHorn
            };
        }

        public static List<Item> GetAllAmmo()
        {
            return new List<Item>
            {
                ArmorPiercingShot,
                IncendiaryGrapeshot
            };
        }

        public static List<Item> GetAllConsumables()
        {
            return new List<Item>
            {
                HealthPotion
            };
        }

        public static List<Item> GetAllMaterials()
        {
            return new List<Item>
            {
                RawIronOre,
                ProcessedSteelIngot
            };
        }
    }
}
