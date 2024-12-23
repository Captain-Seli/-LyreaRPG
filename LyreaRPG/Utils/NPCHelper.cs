using System;
using System.Collections.Generic;
using LyreaRPG.Characters;
using LyreaRPG.AI;
using LyreaRPG.Items;

namespace LyreaRPG.Utils
{
    public static class NPCHelper
    {
        /// <summary>
        /// Updates NPC behavior in the world.
        /// </summary>
        public static void UpdateNPCBehavior(List<NPC> npcs, Player player)
        {
            foreach (var npc in npcs)
            {
                // NPCBehavior.HandleAI(npc, player, npcs);
                Console.WriteLine($"NPC '{npc.Name}' is interacting with {player.Name}.");
            }
        }

        /// <summary>
        /// Creates a basic NPC with specified attributes.
        /// </summary>
        public static NPC CreateBasicNPC(
            string name,
            string faction,
            Dictionary<string, string> personality,
            List<Item> lootTable,
            List<string> likes,
            List<string> dislikes)
        {
            var npc = new NPC(name)
            {
                Faction = faction,
                Personality = personality // Assign the Dictionary directly
            };

            npc.SetLikes(likes);
            npc.SetDislikes(dislikes);
            npc.SetLootTable(lootTable);

            return npc;
        }

        /// <summary>
        /// Generates a random NPC using the NPCGenerator.
        /// </summary>
        public static NPC GenerateRandomNPC()
        {
            return NPCGenerator.GenerateRandomNPC();
        }

        /// <summary>
        /// Debug NPC Details for Testing.
        /// </summary>
        public static void DebugNPCDetails(NPC npc)
        {
            Console.Clear();
            Console.WriteLine($"Name: {npc.Name}");
            Console.WriteLine($"Race: {npc.Race}");
            Console.WriteLine($"Gender: {npc.Gender}");
            Console.WriteLine($"Age: {npc.Age}");
            Console.WriteLine($"Background: {npc.Background}");
            Console.WriteLine($"Faction: {npc.Faction}");
            Console.WriteLine($"Personality: {npc.Personality}");
            Console.WriteLine($"Likes: {string.Join(", ", npc.Likes)}");
            Console.WriteLine($"Dislikes: {string.Join(", ", npc.Dislikes)}");
            Console.WriteLine($"Stats: Strength={npc.Strength}, Dexterity={npc.Dexterity}, Constitution={npc.Constitution}, Intelligence={npc.Intelligence}, Wisdom={npc.Wisdom}, Charisma={npc.Charisma}");
            Console.WriteLine($"Inventory: {string.Join(", ", npc.Inventory)}");
            Console.WriteLine($"Loot Table: {string.Join(", ", npc.LootTable)}");
            Console.WriteLine($"Likability: {npc.Likability}");
            Console.WriteLine("\nPress any key to return.");
            Console.ReadKey();
        }
    }
}
