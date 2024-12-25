using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using LyreaRPG.Characters;
using LyreaRPG.Items;
using LyreaRPG.Utils;
using Newtonsoft.Json.Linq;

namespace LyreaRPG.AI
{
    public static class NPCGenerator
    {
        private static readonly Random Random = new();

        // Paths to JSON files
        private static readonly string NamesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Characters", "npc_names.json");
        private static readonly string LikesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Characters", "npc_likes_dislikes.json");
        private static readonly string PersonalityFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Characters", "npc_personality.json");

        // Load JSON Data
        private static readonly dynamic NamesData = LoadJson(NamesFilePath);
        private static readonly dynamic LikesData = LoadJson(LikesFilePath);
        private static readonly dynamic PersonalityData = LoadJson(PersonalityFilePath);

        public static NPC GenerateRandomNPC()
        {
            var race = AssignRace(); // Assign race first
            var gender = AssignGender();
            var npc = new NPC(GenerateName(race, gender)); // Pass race to GenerateName

            npc.SetRace(race);
            npc.SetGender(gender);
            npc.SetAge(GenerateAge(race)); // Pass race to GenerateAge
            npc.SetBackground(AssignBackground());
            npc.SetLevel(Random.Next(1, 11));
            npc.SetLikes(GenerateLikes());
            npc.SetDislikes(GenerateDislikes(race));
            npc.SetPersonality();
            AssignDisposition(npc);
            npc.SetFaction(AssignFaction(race));
            npc.SetInventory(GenerateInventory());
            npc.SetLootTable(GenerateLootTable());

            AssignStats(npc);
            AssignChannelingPower(npc);

            return npc;
        }

        private static string AssignRace()
        {
            var roll = Random.Next(1, 101);
            return roll switch
            {
                <= 50 => "Human",
                <= 62 => "Carcharia",
                <= 75 => "Molluska",
                <= 87 => "Crabaxi",
                <= 94 => "Elf",
                _ => "Sinai"
            };
        }

        private static string AssignGender()
        {
            return Random.Next(0, 2) == 0 ? "Male" : "Female";
        }

        private static string GenerateName(string race = null, string gender = null)
        {

            // Check if race exists in NamesData
            if (!NamesData.ContainsKey(race))
                throw new KeyNotFoundException($"Race '{race}' not found in npc_names.json.");

            // Determine the appropriate keys for first names and surnames
            string firstNamesKey = gender == "Male" ? "MaleFirstNames" : "FemaleFirstNames";
            string surnamesKey = "Surnames";

            // Validate that race has required keys for first names and surnames
            if (!NamesData[race].ContainsKey(firstNamesKey))
                throw new KeyNotFoundException($"Key '{firstNamesKey}' not found for race '{race}' in npc_names.json.");

            if (!NamesData[race].ContainsKey(surnamesKey))
                throw new KeyNotFoundException($"Key '{surnamesKey}' not found for race '{race}' in npc_names.json.");

            // Retrieve the lists of first names and surnames
            var firstNameList = ((JArray)NamesData[race][firstNamesKey]).ToObject<List<string>>();
            var surnameList = ((JArray)NamesData[race][surnamesKey]).ToObject<List<string>>();

            // Validate the lists
            if (firstNameList == null || firstNameList.Count == 0)
                throw new InvalidOperationException($"No first names found for race '{race}' and gender '{gender}'.");

            if (surnameList == null || surnameList.Count == 0)
                throw new InvalidOperationException($"No surnames found for race '{race}'.");

            // Generate random first name and surname
            string firstName = firstNameList[Random.Next(firstNameList.Count)];
            string surname = surnameList[Random.Next(surnameList.Count)];

            return $"{firstName} {surname}";
        }


        private static int GenerateAge(string race)
        {
            return race switch
            {
                "Human" => Random.Next(16, 100),
                "Carcharia" => Random.Next(15, 100),
                "Molluska" => Random.Next(20, 150),
                "Crabaxi" => Random.Next(20, 120),
                "Elf" => Random.Next(100, 300),
                "Sinai" => Random.Next(30, 120),
                _ => 25
            };
        }

        private static string AssignBackground()
        {
            var validBackgrounds = new List<string>
            {
                "Sailor", "Scholar", "Merchant", "Adventurer",
                "Farmer", "Craftsman", "Soldier", "Explorer",
                "Ars Notoria", "Harbingers of the Eternal Weave",
                "Faery-Pact-Bound", "Elder Blood"
            };

            return validBackgrounds[Random.Next(validBackgrounds.Count)];
        }

        private static List<string> GenerateLikes()
        {
            var likes = new List<string>();
            foreach (var category in LikesData.Likes)
            {
                var options = ((JArray)category.Value).ToObject<List<string>>();
                if (options != null && options.Count > 0)
                    likes.Add(options[Random.Next(options.Count)]);
            }
            return likes;
        }
        private static List<string> GenerateDislikes(string faction)
        {
            var dislikes = new List<string>();
            foreach (var category in LikesData.Dislikes)
            {
                var options = ((JArray)category.Value).ToObject<List<string>>();
                if (options != null && options.Count > 0)
                    dislikes.Add(options[Random.Next(options.Count)]);
            }
            return dislikes;
        }


        public static Dictionary<string, string> GeneratePersonality()
        {
            if (PersonalityData.Traits.Count == 0 ||
                PersonalityData.Ideals.Count == 0 ||
                PersonalityData.Bonds.Count == 0 ||
                PersonalityData.Flaws.Count == 0)
            {
                throw new InvalidOperationException("PersonalityData lists are not properly initialized or are empty.");
            }

            return new Dictionary<string, string>
    {
        { "Trait", PersonalityData.Traits[Random.Next(PersonalityData.Traits.Count)] },
        { "Ideal", PersonalityData.Ideals[Random.Next(PersonalityData.Ideals.Count)] },
        { "Bond", PersonalityData.Bonds[Random.Next(PersonalityData.Bonds.Count)] },
        { "Flaw", PersonalityData.Flaws[Random.Next(PersonalityData.Flaws.Count)] }
    };
        }

        private static void AssignDisposition(NPC npc)
        {
            // Assign Friendliness based on Background
            npc.Friendliness = npc.Background switch
            {
                "Pirate" => FriendlinessLevel.Hostile,
                "Merchant" => FriendlinessLevel.Neutral,
                "Scholar" => FriendlinessLevel.Friendly,
                _ => FriendlinessLevel.Neutral
            };

            // Assign Aggression based on Faction and Personality Traits
            npc.Aggression = npc.Friendliness switch
            {
                FriendlinessLevel.Hostile when npc.Background == "Pirate" => AggressionLevel.Aggressive,
                FriendlinessLevel.Neutral => AggressionLevel.Opportunistic,
                FriendlinessLevel.Friendly => AggressionLevel.Pacifist,
                _ => AggressionLevel.Defensive
            };
        }

        private static string AssignFaction(string race)
        {
            var factionsByRace = new Dictionary<string, List<string>>
    {
        { "Human", new List<string> { "West Azonian Trading Company", "Royal Azonian Army", "Royal Azonian Navy", "Order of Light", "Ars Notoria", "Slayers", "Black Squids", "Circle Knights", "Harbingers of the Eternal Weave", "Cobras", "Merchant's Guild", "Artisan's Guild", "Craftsman's Guild", "The Kingdom of Azon", "The Frayed" } },
        { "Elf", new List<string> { "West Azonian Trading Company", "Ars Notoria", "Slayers", "Black Squids", "Merchant's Guild", "Artisan's Guild", "Craftsman's Guild", "Alran Taesi" } },
        { "Sinai", new List<string> { "West Azonian Trading Company", "Ars Notoria", "Slayers", "Black Squids", "Cobras", "Merchant's Guild", "Artisan's Guild", "Craftsman's Guild", "The Empire of Venia", "Sinai" } },
        { "Carcharia", new List<string> { "West Azonian Trading Company", "Slayers", "Black Squids", "Merchant's Guild", "Artisan's Guild", "Craftsman's Guild", "The Kingdom of Azon", "The Republic of Kyran", "The Emerald Empire" } },
        { "Crabaxi", new List<string> { "West Azonian Trading Company", "Slayers", "Black Squids", "Merchant's Guild", "Artisan's Guild", "Craftsman's Guild", "The Kingdom of Azon", "The Republic of Kyran", "The Emerald Empire" } },
        { "Molluska", new List<string> { "West Azonian Trading Company", "Slayers", "Black Squids", "Merchant's Guild", "Artisan's Guild", "Craftsman's Guild", "The Kingdom of Azon", "The Republic of Kyran", "The Emerald Empire" } },
        { "Saltatrix", new List<string> { "West Azonian Trading Company", "Slayers", "Black Squids", "Merchant's Guild", "Artisan's Guild", "Craftsman's Guild", "The Kingdom of Azon", "The Republic of Kyran", "The Emerald Empire" } }
    };

            if (!factionsByRace.ContainsKey(race))
                return "Neutral";

            var eligibleFactions = factionsByRace[race].Where(f =>
                !(f == "Order of Light" && (race != "Human" || CanChannel(race))) &&
                !(f == "Harbingers of the Eternal Weave" && race != "Human")
            ).ToList();

            return eligibleFactions.Count > 0 ? eligibleFactions[Random.Next(eligibleFactions.Count)] : "Neutral";
        }

        private static bool CanChannel(string race)
        {
            return race switch
            {
                "Elf" => true,
                "Sinai" => true,
                _ => false
            };
        }



        private static void AssignStats(NPC npc)
        {
            npc.SetStats(
                Random.Next(8, 15) + npc.Level,
                Random.Next(8, 15) + npc.Level,
                Random.Next(8, 15) + npc.Level,
                Random.Next(8, 15) + npc.Level,
                Random.Next(8, 15) + npc.Level,
                Random.Next(8, 15) + npc.Level
            );
        }

        private static void AssignChannelingPower(NPC npc)
        {
            if (npc.Race == "Elf")
            {
                npc.SetChannelingPower("Kaido");
                if (!npc.Skills.ContainsKey("Channeling"))
                {
                    npc.Skills.Add("Channeling", new Skill("Channeling"));
                }
            }
            else if (npc.Race == "Sinai")
            {
                npc.SetChannelingPower("Kaida");
                if (!npc.Skills.ContainsKey("Channeling"))
                {
                    npc.Skills.Add("Channeling", new Skill("Channeling"));
                }
            }
            else if (npc.Background == "Ars Notoria" || npc.Background == "Harbingers of the Eternal Weave" || npc.Background == "Faery-Pact-Bound")
            {
                npc.SetChannelingPower(Random.Next(0, 2) == 0 ? "Kaida" : "Kaido");
                if (!npc.Skills.ContainsKey("Channeling"))
                {
                    npc.Skills.Add("Channeling", new Skill("Channeling"));
                }
            }
            else if (npc.Background == "Elder Blood")
            {
                npc.SetChannelingPower("Elder Blood");
                npc.SetSanity(80);
                if (!npc.Skills.ContainsKey("Channeling"))
                {
                    npc.Skills.Add("Channeling", new Skill("Channeling"));
                }
            }
        }


        private static List<Item> GenerateInventory()
        {
            return new List<Item>
            {
                new Item("Common Clothes", "Basic attire for everyday use.", "Clothing", 1),
                new Item("Bread", "Simple nourishment.", "Consumable", 2)
            };
        }

        private static List<Item> GenerateLootTable()
        {
            return new List<Item>
            {
                ItemHelper.CopperCoin,
                ItemHelper.WoodenTotem
            };
        }

        private static JObject LoadJson(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"JSON file not found at {path}");

            return JObject.Parse(File.ReadAllText(path));
        }
    }
}
