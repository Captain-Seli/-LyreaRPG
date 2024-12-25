using System;
using System.Collections.Generic;
using LyreaRPG.Items;
using LyreaRPG.Utils;

namespace LyreaRPG.Characters
{
    public class NPC : Player
    {
        public FriendlinessLevel Friendliness { get; set; } = FriendlinessLevel.Neutral;
        public AggressionLevel Aggression { get; set; } = AggressionLevel.Defensive;


        public List<string> Likes { get; set; } = new();
        public List<string> Dislikes { get; set; } = new();
        public Dictionary<string, string> Personality { get; set; } = new();
        public new string Faction { get; set; } = "None"; // Default faction
        public int Likability { get; private set; } = 50; // Likability starts at neutral
        public List<Item> LootTable { get; set; } = new();

        public NPC(string name) : base(name)
        {
            Strength = 8;
            Dexterity = 8;
            Constitution = 8;
            Intelligence = 8;
            Wisdom = 8;
            Charisma = 8;
        }

        // Getter Methods
        // Display Disposition
        public string GetDisposition()
        {
            return $"{Friendliness} | {Aggression}";
        }

        // Setter Methods
        public void SetLikes(List<string> likes) => Likes = new List<string>(likes);
        public void SetDislikes(List<string> dislikes) => Dislikes = new List<string>(dislikes);

        public void SetPersonality()
        {
            // Generate personality using the PersonalityData lists
            Personality = new Dictionary<string, string>
            {
                { "Trait", PersonalityData.Traits[new Random().Next(PersonalityData.Traits.Count)] },
                { "Ideal", PersonalityData.Ideals[new Random().Next(PersonalityData.Ideals.Count)] },
                { "Bond", PersonalityData.Bonds[new Random().Next(PersonalityData.Bonds.Count)] },
                { "Flaw", PersonalityData.Flaws[new Random().Next(PersonalityData.Flaws.Count)] }
            };
        }

        public new void SetFaction(string faction) => Faction = faction;
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

        public new void DisplayStats()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Race: {Race}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Background: {Background}");
            Console.WriteLine($"Faction: {Faction}");

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
            Console.WriteLine($"Disposition: {GetDisposition()}");
            Console.WriteLine("Likes: " + string.Join(", ", Likes));
            Console.WriteLine("Dislikes: " + string.Join(", ", Dislikes));

            Console.WriteLine("Stats:");
            Console.WriteLine($"  Strength: {Strength}");
            Console.WriteLine($"  Dexterity: {Dexterity}");
            Console.WriteLine($"  Constitution: {Constitution}");
            Console.WriteLine($"  Intelligence: {Intelligence}");
            Console.WriteLine($"  Wisdom: {Wisdom}");
            Console.WriteLine($"  Charisma: {Charisma}");
            Console.WriteLine($"  Sanity: {Sanity}");
            if (!string.IsNullOrEmpty(ChannelingPower))
            {
                Console.WriteLine($"  Power: {ChannelingPower}");
            }

            Console.WriteLine("Skills:");
            foreach (var skill in Skills)
            {
                Console.WriteLine($"  {skill.Key}: Level {skill.Value.Level}");
            }

            Console.WriteLine("Inventory:");
            foreach (var item in Inventory)
            {
                Console.WriteLine($"  {item.Name}: {item.Description} (x{item.Quantity})");
            }
        }
    }
}
