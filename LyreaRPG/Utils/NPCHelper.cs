using System;
using System.Collections.Generic;
using LyreaRPG.Characters;
using LyreaRPG.AI;
using LyreaRPG.Items;

namespace LyreaRPG.Utils
{
    public static class NPCHelper
    {
        public static void UpdateNPCBehavior(List<NPC> npcs, Player player)
        {
            foreach (var npc in npcs)
            {
                // NPCBehavior.HandleAI(npc, player, npcs);
            }
        }

        public static NPC CreateBasicNPC(string name, string faction, string personality, List<Item> lootTable, List<string> likes, List<string> dislikes)
        {
            var npc = new NPC(name)
            {
                Faction = faction,
                Personality = personality,
                Likes = likes,
                Dislikes = dislikes
            };

            npc.LootTable.AddRange(lootTable);
            // npc.State = AIState.Idle;

            return npc;
        }
    }
}
