﻿// Full Player Class with Races and Features
using System;
using System.Collections.Generic;
using System.Linq;
using LyreaRPG.Items;

namespace LyreaRPG.Characters
{
    public class Player
    {
        // Basic Info
        public string Name { get; set; }
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; } = 0;
        public int SkillPoints { get; private set; } = 0;

        // Gold property
        public int Gold { get; private set; } = 100; // Default starting gold

        // Primary Stats
        public int Strength { get; private set; }
        public int Dexterity { get; private set; }
        public int Constitution { get; private set; }
        public int Intelligence { get; private set; }
        public int Wisdom { get; private set; }
        public int Charisma { get; private set; }


        // Derived Stats
        public int BaseHealth { get; set; }
        public int Health => BaseHealth + (Constitution * Level);
        public int Speed => 30 + Dexterity;
        public double MaxWeight => Strength * 10; // Maximum carrying capacity in weight units
        public double CurrentWeight { get; private set; } = 0; // Tracks current total weight


        // Sanity
        public int Sanity { get; private set; } = 100;

        // Skills
        public Dictionary<string, Skill> Skills { get; private set; } = new();
        public bool CanChannel { get; private set; } = false;
        public string ChannelingPower { get; private set; } = "None";

        // Inventory and Wearable Slots
        public List<Item> Inventory { get; private set; } = new();
        public Dictionary<string, string> EquipmentSlots { get; private set; } = new();

        // Add tracking fields
        public string CurrentPOI { get; private set; } = "None";
        public string CurrentLocation { get; private set; } = "None";
        public string CurrentRegion { get; private set; } = "None";

        public void SetPOI(string region, string location, string poi)
        {
            CurrentRegion = region;
            CurrentLocation = location;
            CurrentPOI = poi;
        }

        public void SetLocation(string region, string location)
        {
            CurrentRegion = region;
            CurrentLocation = location;
            Console.WriteLine($"You are now in {CurrentLocation}, located in {CurrentRegion}.");
        }

        public Player(string name)
        {
            Name = name;
            InitializeStats();
            InitializeSkills();
            InitializeEquipmentSlots();

            // Default Starting Location
            CurrentRegion = "The Sun Spur";
            CurrentLocation = "Port Waveward";
            CurrentPOI = "The Harbor";

            // Starting Equipment
            Inventory.Add(new Item("Flintlock Pistol", "A reliable sidearm for close-range encounters.", "Weapon", 30, 1, false, 5));
            Inventory.Add(new Item("Flintlock Shot", "A bag of 10 flintlock bullets.", "Ammo", 5, 1, true, 1));
            Inventory.Add(new Item("Gunpowder Horn", "Stores enough gunpowder for 5 reloads.", "Tool", 5, 1, false, 3));

            // Equipped Items
            EquipmentSlots["Chest"] = "Sailor's Shirt";
            EquipmentSlots["pants"] = "Sailor's Pants";
            EquipmentSlots["Hands"] = "Sailor's Gloves";
            EquipmentSlots["Head"] = "Tricorner Hat";
            EquipmentSlots["Waist"] = "Leather Belt";
            EquipmentSlots["Feet"] = "Leather Boots";
            EquipmentSlots["Ring"] = "Brass Ring";
        }

        private void InitializeStats()
        {
            Strength = 10;
            Dexterity = 10;
            Constitution = 10;
            Intelligence = 10;
            Wisdom = 10;
            Charisma = 10;
        }

        private void InitializeSkills()
        {
            Skills.Add("Range", new Skill("Range"));
            Skills.Add("Melee", new Skill("Melee"));
            Skills.Add("Medicine", new Skill("Medicine"));
            Skills.Add("Speaking", new Skill("Speaking"));
            Skills.Add("Cooking", new Skill("Cooking"));
            Skills.Add("Alchemy", new Skill("Alchemy"));
            Skills.Add("Survival", new Skill("Survival"));
            Skills.Add("Crafting", new Skill("Crafting"));
            Skills.Add("Lore", new Skill("Lore"));
            Skills.Add("Sailing", new Skill("Sailing"));
            Skills.Add("Stealth", new Skill("Stealth"));
            Skills.Add("Hunting", new Skill("Hunting"));
            Skills.Add("Tinkering", new Skill("Tinkering"));
            Skills.Add("Diplomacy", new Skill("Diplomacy"));
        }

        private void InitializeEquipmentSlots()
        {
            var slots = new[] { "Helmet/Hat", "Face", "Neck", "Chest", "Hands", "Waist", "pants", "Left Arm", "Right Arm", "Left Leg", "Right Leg", "Feet", "Main Hand", "Offhand", "Ring" };
            foreach (var slot in slots)
            {
                EquipmentSlots[slot] = null; // No item equipped initially
            }
        }

        public void EquipItem(string itemName)
        {
            var item = Inventory.FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (item == null)
            {
                Console.WriteLine("Item not found in inventory.");
                return;
            }

            string targetSlot = item.Type switch
            {
                "Weapon" => "Main hand",
                "Shield" => "Offhand",
                "Necklace" => "Neck",
                "Ring" => "Ring",
                "Armor" => "Chest",
                _ => null
            };

            if (targetSlot == null)
            {
                Console.WriteLine("This item cannot be equipped.");
                return;
            }

            // Unequip existing item in the slot
            if (EquipmentSlots[targetSlot] != null)
            {
                Console.WriteLine($"Unequipped {EquipmentSlots[targetSlot]} from {targetSlot}.");
                AddItem(new Item(EquipmentSlots[targetSlot], "Unequipped item.", "Unequipped", 0));
            }

            // Equip new item
            EquipmentSlots[targetSlot] = itemName;
            Inventory.Remove(item);
            Console.WriteLine($"{itemName} equipped to {targetSlot}.");
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            while (Experience >= GetExperienceForNextLevel() && Level < 100)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            SkillPoints++;
            Console.WriteLine($"{Name} leveled up to Level {Level}! You now have {SkillPoints} skill points to assign.");
        }

        private int GetExperienceForNextLevel()
        {
            return Level * Level * 10; // Example scaling formula
        }

        public void AssignSkillPoint(string stat)
        {
            if (SkillPoints <= 0)
            {
                Console.WriteLine("No skill points available.");
                return;
            }

            switch (stat.ToLower())
            {
                case "strength":
                    Strength++;
                    break;
                case "dexterity":
                    Dexterity++;
                    break;
                case "constitution":
                    Constitution++;
                    break;
                case "intelligence":
                    Intelligence++;
                    break;
                case "wisdom":
                    Wisdom++;
                    break;
                case "charisma":
                    Charisma++;
                    break;
                default:
                    Console.WriteLine("Invalid stat.");
                    return;
            }

            SkillPoints--;
            Console.WriteLine($"Assigned 1 point to {stat}. Remaining skill points: {SkillPoints}.");
        }

        public void DetermineChanneling(bool isElf, bool isSinai, bool forceRoll = false)
        {
            if (isElf)
            {
                CanChannel = true;
                ChannelingPower = "Kaido";
                Skills.Add("Channeling", new Skill("Channeling"));
                return;
            }

            if (isSinai)
            {
                CanChannel = true;
                ChannelingPower = "Kaida";
                Skills.Add("Channeling", new Skill("Channeling"));
                return;
            }

            Random rng = new Random();
            if (forceRoll || rng.Next(1, 101) == 1) // 1% chance unless forced
            {
                CanChannel = true;
                int powerRoll = rng.Next(1, 101);

                if (powerRoll <= 47) // 47% chance
                {
                    ChannelingPower = "Kaido";
                }
                else if (powerRoll <= 94) // 47% chance
                {
                    ChannelingPower = "Kaida";
                }
                else // 6% chance
                {
                    ChannelingPower = "Elder Blood";
                }
                Skills.Add("Channeling", new Skill("Channeling"));
            }
        }

        public void AddGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"{amount} gold added. You now have {Gold} gold.");
        }

        public bool SpendGold(int amount)
        {
            if (Gold >= amount)
            {
                Gold -= amount;
                Console.WriteLine($"{amount} gold spent. You now have {Gold} gold.");
                return true;
            }
            else
            {
                Console.WriteLine("Not enough gold!");
                return false;
            }
        }

        // Method to earn gold
        public void EarnGold(int amount)
        {
            Gold += amount;
            Console.WriteLine($"You earned {amount} gold. Total gold: {Gold}.");
        }

        // Method to set gold (for debugging or admin purposes)
        public void SetGold(int amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Gold cannot be set to a negative value.");
                return;
            }

            Gold = amount;
            Console.WriteLine($"Gold has been set to {Gold}.");
        }

        public void AddItem(Item item)
        {
            if (item != null)
            {
                Inventory.Add(item);
                Console.WriteLine($"{item} has been added to your inventory.");
            }
            else
            {
                Console.WriteLine("Cannot add an empty or invalid item.");
            }
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Inventory:");

            // Display Gold as part of the inventory
            Console.WriteLine($"  - Gold: {Gold}");

            // Display other items in inventory
            if (Inventory.Count == 0)
            {
                Console.WriteLine("  Your inventory contains no other items.");
            }
            else
            {
                foreach (var item in Inventory)
                {
                    Console.WriteLine($"  - {item.Name}: {item.Description} (x{item.Quantity})");
                }
            }

            Console.WriteLine("\nEquipment:");
            foreach (var slot in EquipmentSlots)
            {
                Console.WriteLine($"  {slot.Key}: {(slot.Value ?? "Empty")}");
            }
        }

        public void DisplayStats()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Experience: {Experience}/{GetExperienceForNextLevel()} for next level");
            Console.WriteLine($"Skill Points: {SkillPoints}");
            Console.WriteLine($"Health: {Health}");
            Console.WriteLine($"Sanity: {Sanity}");
            Console.WriteLine($"Speed: {Speed}");
            Console.WriteLine($"Strength: {Strength}");
            Console.WriteLine($"Dexterity: {Dexterity}");
            Console.WriteLine($"Constitution: {Constitution}");
            Console.WriteLine($"Intelligence: {Intelligence}");
            Console.WriteLine($"Wisdom: {Wisdom}");
            Console.WriteLine($"Charisma: {Charisma}");

            Console.WriteLine("Skills:");

            if (CanChannel && Skills.ContainsKey("Channeling"))
            {
                var channelingSkill = Skills["Channeling"];
                Console.WriteLine($"  Channeling: Level {channelingSkill.Level} (Exp: {channelingSkill.Experience}/100 - {ChannelingPower})");
            }

            foreach (var skill in Skills)
            {
                if (skill.Key != "Channeling")
                {
                    Console.WriteLine($"  {skill.Key}: Level {skill.Value.Level} (Exp: {skill.Value.Experience}/100)");
                }
            }
        }
    }

    // Race-Specific Classes
    public class Human : Player
    {
        public Human(string name, bool forceRoll = false) : base(name)
        {
            BaseHealth = 8;
            DetermineChanneling(false, false, forceRoll);
        }
    }

    public class Sinai : Player
    {
        public Sinai(string name) : base(name)
        {
            BaseHealth = 6;
            DetermineChanneling(false, true);
        }
    }

    public class Carcharia : Player
    {
        public Carcharia(string name, bool forceRoll = false) : base(name)
        {
            BaseHealth = 10;
            DetermineChanneling(false, false, forceRoll);
        }
    }

    public class Elf : Player
    {
        public Elf(string name) : base(name)
        {
            BaseHealth = 8;
            DetermineChanneling(true, false);
        }
    }

    public class Molluska : Player
    {
        public Molluska(string name, bool forceRoll = false) : base(name)
        {
            BaseHealth = 8;
            DetermineChanneling(false, false, forceRoll);
        }
    }

    public class Saltatrix : Player
    {
        public Saltatrix(string name, bool forceRoll = false) : base(name)
        {
            BaseHealth = 6;
            DetermineChanneling(false, false, forceRoll);
        }
    }

    public class Crabaxi : Player
    {
        public Crabaxi(string name, bool forceRoll = false) : base(name)
        {
            BaseHealth = 10;
            DetermineChanneling(false, false, forceRoll);
        }
    }

    public class Skill
    {
        public string Name { get; }
        public int Level { get; private set; } = 1;
        public int Experience { get; private set; } = 0;

        public Skill(string name)
        {
            Name = name;
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            while (Experience >= 100 && Level < 100)
            {
                Level++;
                Experience -= 100;
                Console.WriteLine($"{Name} leveled up to Level {Level}!");
            }
        }
    }
}