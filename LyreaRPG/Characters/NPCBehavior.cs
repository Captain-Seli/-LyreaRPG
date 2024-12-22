using System;
//using System.Collections.Generic;
//using LyreaRPG.Characters;
//using LyreaRPG.Items;
//using LyreaRPG.Utils;

namespace LyreaRPG.AI
{
//    public enum AIState { Idle, Alert, Hostile, Fleeing, Dead }

//    public static class NPCBehavior
//    {
//        public static void HandleAI(NPC npc, Player player, List<NPC> nearbyNPCs)
//        {
//            switch (npc.State)
//            {
//                case AIState.Idle:
//                    HandleIdleState(npc, player, nearbyNPCs);
//                    break;

//                case AIState.Alert:
//                    HandleAlertState(npc, player, nearbyNPCs);
//                    break;

//                case AIState.Hostile:
//                    HandleHostileState(npc, player, nearbyNPCs);
//                    break;

//                case AIState.Fleeing:
//                    HandleFleeingState(npc);
//                    break;

//                case AIState.Dead:
//                    HandleDeadState(npc);
//                    break;

//                default:
//                    Console.WriteLine($"{npc.Name} is in an undefined state.");
//                    break;
//            }
//        }

//        private static void HandleIdleState(NPC npc, Player player, List<NPC> nearbyNPCs)
//        {
//            Console.WriteLine($"{npc.Name} is idle.");
//            // NPC reacts to player's presence
//            if (player != null && npc.Likability < 30)
//            {
//                Console.WriteLine($"{npc.Name} glares at you suspiciously.");
//                npc.State = AIState.Alert;
//            }
//        }

//        private static void HandleAlertState(NPC npc, Player player, List<NPC> nearbyNPCs)
//        {
//            Console.WriteLine($"{npc.Name} is alert.");
//            // Decide to engage or flee
//            if (npc.Likability < 10)
//            {
//                Console.WriteLine($"{npc.Name} draws their weapon!");
//                npc.State = AIState.Hostile;
//            }
//            else if (npc.Likability > 70)
//            {
//                Console.WriteLine($"{npc.Name} decides to calm down.");
//                npc.State = AIState.Idle;
//            }
//        }

//        private static void HandleHostileState(NPC npc, Player player, List<NPC> nearbyNPCs)
//        {
//            Console.WriteLine($"{npc.Name} is hostile and attacks!");
//            // Engage in combat
//            CombatHelper.InitiateCombat(npc, player);
//        }

//        private static void HandleFleeingState(NPC npc)
//        {
//            Console.WriteLine($"{npc.Name} is fleeing!");
//            // Move NPC to a safe location
//            MoveToSafeLocation(npc);
//        }

//        private static void HandleDeadState(NPC npc)
//        {
//            Console.WriteLine($"{npc.Name} has died.");
//            npc.DropLoot();
//        }

//        private static void MoveToSafeLocation(NPC npc)
//        {
//            Console.WriteLine($"{npc.Name} escapes to a safe location.");
//            npc.State = AIState.Idle;
//        }
//    }
}
