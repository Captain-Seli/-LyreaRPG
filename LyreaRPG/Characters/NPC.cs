using System;
using System.Collections.Generic;
using LyreaRPG.Items;
using LyreaRPG.Utils;

namespace LyreaRPG.Characters
{
    public class NPC : Player
    {
        public string Faction { get; set; } = "Neutral"; // NPC's faction
        public int Likability { get; private set; } = 50; // Likability starts at neutral
        public string Personality { get; set; } = "Neutral"; // General personality description
        public List<string> Likes { get; set; } = new(); // Things the NPC likes
        public List<string> Dislikes { get; set; } = new(); // Things the NPC dislikes

        public List<Item> LootTable { get; set; } = new(); // Items dropped upon defeat

        public NPC(string name) : base(name)
        {
            // Default stats for NPC
            Strength = 8;
            Dexterity = 8;
            Constitution = 8;
            Intelligence = 8;
            Wisdom = 8;
            Charisma = 8;
        }

        /// <summary>
        /// Adjusts likability based on the player's action.
        /// </summary>
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

        /// <summary>
        /// React to a player's action based on personality and preferences.
        /// </summary>
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

        /// <summary>
        /// Gets the current relationship status based on likability.
        /// </summary>
        public string GetRelationshipStatus()
        {
            return Likability switch
            {
                <= 30 => "Dislike",
                >= 70 => "Like",
                _ => "Neutral"
            };
        }

        /// <summary>
        /// Drops loot when defeated.
        /// </summary>
        public List<Item> DropLoot()
        {
            Console.WriteLine($"{Name} has been defeated. Loot dropped:");
            foreach (var item in LootTable)
            {
                Console.WriteLine($"  - {item.Name}: {item.Description}");
            }
            return LootTable;
        }
    }
}
