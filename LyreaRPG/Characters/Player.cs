// Full Player Class with Races and Features
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.CompilerServices;
using LyreaRPG.Items;
using LyreaRPG.Utils;

namespace LyreaRPG.Characters
{
    public class Player
    {
        // Basic Info
        public string Name { get; set; }
        public string Race { get; set; }
        public string Gender { get; set; }
        public int Level { get; protected set; } = 1;
        public int Experience { get; private set; } = 0;
        public int SkillPoints { get; private set; } = 0;
        public string Background { get; set; }
        public int Age { get; set; }
        

        // Money property
        public int Money { get; private set; } = 100; // Default starting money

        // Primary Stats
        public int Strength { get; protected set; }
        public int Dexterity { get; protected set; }
        public int Constitution { get; protected set; }
        public int Intelligence { get; protected set; }
        public int Wisdom { get; protected set; }
        public int Charisma { get; protected set; }


        // Derived Stats
        public int BaseHealth { get; set; }
        public int Health => BaseHealth + (Constitution * Level);
        public int Speed => 30 + Dexterity;
        public double MaxWeight => Strength * 10; // Maximum carrying capacity in weight units
        public double CurrentWeight { get; private set; } = 0; // Tracks current total weight


        // Sanity
        public int Sanity { get; protected set; } = 100;

        // Faction
        public string Faction { get; protected set; }

        // Skills
        public Dictionary<string, Skill> Skills { get; private set; } = new();
        public bool CanChannel { get; protected set; } = false;
        public string ChannelingPower { get; protected set; } = "None";

        // Inventory and Wearable Slots
        public List<Item> Inventory { get; protected set; } = new();
        public Dictionary<string, string> EquipmentSlots { get; protected set; } = new();

        // Add tracking fields
        public string CurrentPOI { get; protected set; } = "None";
        public string CurrentLocation { get; protected set; } = "None";
        public string CurrentRegion { get; protected set; } = "None";

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
            InitializeDefaultItems();

            // Default Starting Location
            CurrentRegion = "The Sun Spur";
            CurrentLocation = "Port Waveward";
            CurrentPOI = "The Harbor";

            void InitializeEquipmentSlots()
            {
                EquipmentSlots = EquipmentHelper.InitializeSlots();
            }

            void InitializeDefaultItems()
            {
                // Add default inventory items
                Inventory.AddRange(new[]
                {
            ItemHelper.SailorsShirt,
            ItemHelper.SailorsPants,
            ItemHelper.LeatherGloves,
            ItemHelper.TricornerHat,
            ItemHelper.LeatherBelt,
            ItemHelper.LeatherBoots,
            ItemHelper.BrassRing,
            ItemHelper.FlintlockPistol,
            ItemHelper.GunpowderHorn
            });

                // Equip default items silently using EquipmentHelper
                EquipmentHelper.EquipItemSilently(this, ItemHelper.SailorsShirt.Name);
                EquipmentHelper.EquipItemSilently(this, ItemHelper.SailorsPants.Name);
                EquipmentHelper.EquipItemSilently(this, ItemHelper.LeatherGloves.Name);
                EquipmentHelper.EquipItemSilently(this, ItemHelper.TricornerHat.Name);
                EquipmentHelper.EquipItemSilently(this, ItemHelper.LeatherBelt.Name);
                EquipmentHelper.EquipItemSilently(this, ItemHelper.LeatherBoots.Name);
                EquipmentHelper.EquipItemSilently(this, ItemHelper.BrassRing.Name);
            }
        }

        // Setter Methods
        public void SetRace(string race) => Race = race;
        public void SetGender(string gender) => Gender = gender;
        public void SetAge(int age) => Age = age;
        public void SetBackground(string background) => Background = background;
        public void SetLevel(int level) => Level = level;
        public void SetStats(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
        {
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Wisdom = wisdom;
            Charisma = charisma;
        }

        public void SetSanity(int sanity) => Sanity = Math.Clamp(sanity, 0, 100);
        public void SetChannelingPower(string power)
        {
            CanChannel = true;
            ChannelingPower = power;
            if (!Skills.ContainsKey("Channeling"))
                Skills.Add("Channeling", new Skill("Channeling"));
        }

        public void SetInventory(List<Item> inventory) => Inventory = new List<Item>(inventory);
        public void SetFaction(string faction) => Faction = faction;

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
            var slots = new[] { "Head", "Face", "Neck", "Chest", "Hands", "Waist", "Pants", "Left Arm", "Right Arm", "Left Leg", "Right Leg", "Feet", "Main Hand", "Offhand", "Ring" };
            foreach (var slot in slots)
            {
                EquipmentSlots[slot] = null; // No item equipped initially
            }
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
            Money += amount;
            Console.WriteLine($"{amount} money added. You now have {Money} money.");
        }

        public bool SpendGold(int amount)
        {
            if (Money >= amount)
            {
                Money -= amount;
                Console.WriteLine($"{amount} money spent. You now have {Money} money.");
                return true;
            }
            else
            {
                Console.WriteLine("Not enough money!");
                return false;
            }
        }

        // Method to earn gold
        public void EarnGold(int amount)
        {
            Money += amount;
            Console.WriteLine($"You earned {amount} money. Total money: {Money}.");
        }

        // Method to set gold (for debugging or admin purposes)
        public void SetGold(int amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Money cannot be set to a negative value.");
                return;
            }

            Money = amount;
            Console.WriteLine($"Gold has been set to {Money}.");
        }

        public void AddItem(Item item)
        {
            double newWeight = CurrentWeight + (item.Weight * item.Quantity);
            if (newWeight > MaxWeight)
            {
                Console.WriteLine($"Cannot add {item.Name}. Exceeds carrying capacity!");
                return;
            }

            Inventory.Add(item);
            CurrentWeight = newWeight;
            Console.WriteLine($"{item.Name} added to inventory. Current weight: {CurrentWeight}/{MaxWeight}");
        }

        public void RemoveItem(Item item, int quantity = 1)
        {
            var existingItem = Inventory.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem == null || existingItem.Quantity < quantity)
            {
                Console.WriteLine($"Not enough {item.Name} in inventory.");
                return;
            }

            existingItem.Quantity -= quantity;
            if (existingItem.Quantity <= 0)
            {
                Inventory.Remove(existingItem);
            }

            CurrentWeight -= item.Weight * quantity;
            Console.WriteLine($"{item.Name} removed. Current weight: {CurrentWeight}/{MaxWeight}");
        }

        public void DisplayInventory()
        {
            Console.WriteLine("Inventory:");

            // Display Gold as part of the inventory
            Console.WriteLine($"  - Gold: {Money}");

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
