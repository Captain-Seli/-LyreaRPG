using System;
using System.Collections.Generic;
using LyreaRPG.Items;
using LyreaRPG.Utils;

namespace LyreaRPG.Characters
{
    public class NPC : Player
    {
        public List<string> Likes { get; set; } = new();
        public List<string> Dislikes { get; set; } = new();
        //public string Personality { get; set; }
        public Dictionary<string, string> Personality { get; set; } = new();
        public string Faction { get; set; } = "None"; // Default faction
        public int Likability { get; private set; } = 50; // Likability starts at neutral

        public List<Item> LootTable { get; set; } = new();

        private Dictionary<string, string> personality = new();

        public void SetPersonality(Dictionary<string, string> personalityAttributes)
        {
            this.personality = personalityAttributes;
        }

        public Dictionary<string, string> GetPersonality()
        {
            return this.personality;
        }

        public NPC(string name) : base(name)
        {
            Strength = 8;
            Dexterity = 8;
            Constitution = 8;
            Intelligence = 8;
            Wisdom = 8;
            Charisma = 8;
        }

        // Setter Methods
        public void SetLikes(List<string> likes) => Likes = new List<string>(likes);
        public void SetDislikes(List<string> dislikes) => Dislikes = new List<string>(dislikes);
        // public void SetPersonality(Dictionary<string, string> personality) => Personality = personality;
        public void SetFaction(string faction) => Faction = faction;
        public void SetLootTable(List<Item> lootTable) => LootTable = new List<Item>(lootTable);

        public void AdjustLikability(int amount)
        {
            Likability = Math.Clamp(Likability + amount, 0, 100);

            if (amount > 0)
            {
                Console.WriteLine($"{Name}'s likability increased by {amount}. Current likability: {Likability}.");
            }
            else if (amount < 0)
            {
                Console.WriteLine($"{Name}'s likability decreased by {Math.Abs(amount)}. Current likability: {Likability}.");
            }
        }

        public void ReactToAction(string action)
        {
            if (Likes.Contains(action))
            {
                AdjustLikability(10); // Example: Increase likability for liked actions
                Console.WriteLine($"{Name}: \"I appreciate that!\"");
            }
            else if (Dislikes.Contains(action))
            {
                AdjustLikability(-10); // Decrease likability for disliked actions
                Console.WriteLine($"{Name}: \"I don't like that at all!\"");
            }
            else
            {
                Console.WriteLine($"{Name}: \"Hmm... I don't have strong feelings about that.\"");
            }
        }

        public string GetRelationshipStatus()
        {
            return Likability switch
            {
                <= 30 => "Dislike",
                >= 70 => "Like",
                _ => "Neutral"
            };
        }

        public List<Item> DropLoot()
        {
            Console.WriteLine($"{Name} has been defeated. Loot dropped:");
            foreach (var item in LootTable)
            {
                Console.WriteLine($"  - {item.Name}: {item.Description}");
            }
            return LootTable;
        }

        public void DisplayStats()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Race: {Race}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Background: {Background}");
            Console.WriteLine($"Faction: {Faction}");
            Console.WriteLine($"Level: {Level}");

            // Format Personality for Display
            Console.WriteLine("Personality:");
            if (Personality != null && Personality.Count > 0)
            {
                foreach (var entry in Personality)
                {
                    Console.WriteLine($"  {entry.Key}: {entry.Value}");
                }
            }
            else
            {
                Console.WriteLine("  None");
            }

            // Format Likes
            Console.WriteLine("Likes: " + (Likes != null && Likes.Count > 0 ? string.Join(", ", Likes) : "None"));

            // Format Dislikes
            Console.WriteLine("Dislikes: " + (Dislikes != null && Dislikes.Count > 0 ? string.Join(", ", Dislikes) : "None"));

            // Display Stats
            Console.WriteLine($"Stats: Strength={Strength}, Dexterity={Dexterity}, Constitution={Constitution}, Intelligence={Intelligence}, Wisdom={Wisdom}, Charisma={Charisma}");

            // Format Inventory
            Console.WriteLine("Inventory: " + (Inventory != null && Inventory.Count > 0 ? string.Join(", ", Inventory.Select(i => $"{i.Name} ({i.Type}) - {i.Description} (x{i.Quantity}, Weight: {i.Weight})")) : "None"));

            // Format Loot Table
            Console.WriteLine("Loot Table: " + (LootTable != null && LootTable.Count > 0 ? string.Join(", ", LootTable.Select(i => $"{i.Name} ({i.Type}) - {i.Description} (x{i.Quantity}, Weight: {i.Weight})")) : "None"));

            Console.WriteLine($"Likability: {Likability}");
        }



    }
}
