using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using LyreaRPG.Characters;

namespace LyreaRPG.Utils
{
    public static class CharacterStorageHelper
    {
        private const string SaveDirectory = "./Saves/Characters";

        public static void Initialize()
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
        }

        public static void SaveCharacter(string username, Player character)
        {
            string characterFile = GetCharacterFilePath(username);

            // Load existing characters for the account
            List<Player> characters = LoadCharacters(username);

            // Add or update the character
            var existingCharacter = characters.Find(c => c.Name == character.Name);
            if (existingCharacter != null)
            {
                characters.Remove(existingCharacter); // Remove the existing character
            }
            characters.Add(character); // Add the new/updated character

            // Save the updated list back to the file
            string json = JsonSerializer.Serialize(characters, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(characterFile, json);

            Console.WriteLine("Character saved successfully!");
            Console.WriteLine("Press any key to return.");
            Console.ReadKey();
        }

        public static List<Player> LoadCharacters(string username)
        {
            string characterFile = GetCharacterFilePath(username);

            if (!File.Exists(characterFile))
            {
                return new List<Player>(); // No characters exist for this account
            }

            try
            {
                string json = File.ReadAllText(characterFile);
                return JsonSerializer.Deserialize<List<Player>>(json) ?? new List<Player>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading characters: {ex.Message}");
                return new List<Player>();
            }
        }

        public static Player LoadCharacter(string username, string characterName)
        {
            List<Player> characters = LoadCharacters(username);

            // Find the character by name
            return characters.Find(c => c.Name.Equals(characterName, StringComparison.OrdinalIgnoreCase));
        }

        private static string GetCharacterFilePath(string username)
        {
            return Path.Combine(SaveDirectory, $"{username}_characters.json");
        }
    }
}
