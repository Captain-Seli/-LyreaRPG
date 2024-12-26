using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using LyreaRPG.Characters;
using LyreaRPG.Items;

namespace LyreaRPG.Database
{
    public static class DatabaseHelper
    {
        // private const string ConnectionString = @"Data Source=Database\LyreaRPG.db;Version=3;";
        private const string DatabasePath = @"C:\Users\capta\Documents\-LyreaRPG\LyreaRPG\Database\LyreaRPG.db";
        private const string ConnectionString = $"Data Source={DatabasePath};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }

        public static void InitializeDatabase()
        {
            using var connection = GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Players (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Race TEXT NOT NULL,
                    Gender TEXT NOT NULL,
                    Level INTEGER NOT NULL,
                    Experience INTEGER NOT NULL,
                    Strength INTEGER NOT NULL,
                    Dexterity INTEGER NOT NULL,
                    Constitution INTEGER NOT NULL,
                    Intelligence INTEGER NOT NULL,
                    Wisdom INTEGER NOT NULL,
                    Charisma INTEGER NOT NULL,
                    Inventory TEXT NOT NULL,
                    Location TEXT NOT NULL,
                    Faction TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS NPCs (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Race TEXT NOT NULL,
                    Gender TEXT NOT NULL,
                    Background TEXT NOT NULL,
                    Faction TEXT NOT NULL,
                    Friendliness TEXT NOT NULL,
                    Aggression TEXT NOT NULL,
                    Personality TEXT NOT NULL,
                    Strength INTEGER NOT NULL,
                    Dexterity INTEGER NOT NULL,
                    Constitution INTEGER NOT NULL,
                    Intelligence INTEGER NOT NULL,
                    Wisdom INTEGER NOT NULL,
                    Charisma INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Animals (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Species TEXT NOT NULL,
                    Habitat TEXT NOT NULL,
                    Diet TEXT NOT NULL,
                    Disposition TEXT NOT NULL,
                    Size TEXT NOT NULL,
                    Strength INTEGER NOT NULL,
                    Dexterity INTEGER NOT NULL,
                    Constitution INTEGER NOT NULL,
                    Intelligence INTEGER NOT NULL,
                    Wisdom INTEGER NOT NULL,
                    Charisma INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Items (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Description TEXT,
                    Type TEXT NOT NULL,
                    Value INTEGER NOT NULL,
                    Quantity INTEGER NOT NULL,
                    Weight REAL NOT NULL
                );
            ";
            command.ExecuteNonQuery();

            Console.WriteLine("Database initialized successfully!");
        }

        public static void SaveAccount(string username, string passwordHash)
        {
            using var connection = GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
            INSERT INTO Accounts (Username, PasswordHash)
            VALUES (@Username, @PasswordHash)
            ON CONFLICT(Username) DO NOTHING;
        ";

            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@PasswordHash", passwordHash);

            command.ExecuteNonQuery();
            Console.WriteLine($"Account '{username}' saved to database.");
        }


        public static void SavePlayer(Player player)
        {
            using var connection = GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Players (Name, Race, Gender, Level, Experience, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma, Inventory, Location, Faction)
                VALUES (@Name, @Race, @Gender, @Level, @Experience, @Strength, @Dexterity, @Constitution, @Intelligence, @Wisdom, @Charisma, @Inventory, @Location, @Faction)
                ON CONFLICT(Name) DO UPDATE SET
                Race = excluded.Race,
                Gender = excluded.Gender,
                Level = excluded.Level,
                Experience = excluded.Experience,
                Strength = excluded.Strength,
                Dexterity = excluded.Dexterity,
                Constitution = excluded.Constitution,
                Intelligence = excluded.Intelligence,
                Wisdom = excluded.Wisdom,
                Charisma = excluded.Charisma,
                Inventory = excluded.Inventory,
                Location = excluded.Location,
                Faction = excluded.Faction;
            ";

            command.Parameters.AddWithValue("@Name", player.Name);
            command.Parameters.AddWithValue("@Race", player.Race);
            command.Parameters.AddWithValue("@Gender", player.Gender);
            command.Parameters.AddWithValue("@Level", player.Level);
            command.Parameters.AddWithValue("@Experience", player.Experience);
            command.Parameters.AddWithValue("@Strength", player.Strength);
            command.Parameters.AddWithValue("@Dexterity", player.Dexterity);
            command.Parameters.AddWithValue("@Constitution", player.Constitution);
            command.Parameters.AddWithValue("@Intelligence", player.Intelligence);
            command.Parameters.AddWithValue("@Wisdom", player.Wisdom);
            command.Parameters.AddWithValue("@Charisma", player.Charisma);
            command.Parameters.AddWithValue("@Inventory", SerializeInventory(player.Inventory));
            command.Parameters.AddWithValue("@Location", $"{player.CurrentRegion}|{player.CurrentLocation}");
            command.Parameters.AddWithValue("@Faction", player.Faction);

            command.ExecuteNonQuery();
            Console.WriteLine($"Player '{player.Name}' saved to database.");
        }

        public static void SaveNPC(NPC npc)
        {
            using var connection = GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO NPCs (Name, Race, Gender, Background, Faction, Friendliness, Aggression, Personality, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma)
                VALUES (@Name, @Race, @Gender, @Background, @Faction, @Friendliness, @Aggression, @Personality, @Strength, @Dexterity, @Constitution, @Intelligence, @Wisdom, @Charisma);
            ";

            command.Parameters.AddWithValue("@Name", npc.Name);
            command.Parameters.AddWithValue("@Race", npc.Race);
            command.Parameters.AddWithValue("@Gender", npc.Gender);
            command.Parameters.AddWithValue("@Background", npc.Background);
            command.Parameters.AddWithValue("@Faction", npc.Faction);
            command.Parameters.AddWithValue("@Friendliness", npc.Friendliness.ToString());
            command.Parameters.AddWithValue("@Aggression", npc.Aggression.ToString());
            command.Parameters.AddWithValue("@Personality", SerializePersonality(npc.Personality));
            command.Parameters.AddWithValue("@Strength", npc.Strength);
            command.Parameters.AddWithValue("@Dexterity", npc.Dexterity);
            command.Parameters.AddWithValue("@Constitution", npc.Constitution);
            command.Parameters.AddWithValue("@Intelligence", npc.Intelligence);
            command.Parameters.AddWithValue("@Wisdom", npc.Wisdom);
            command.Parameters.AddWithValue("@Charisma", npc.Charisma);

            command.ExecuteNonQuery();
            Console.WriteLine($"NPC '{npc.Name}' saved to database.");
        }

        public static void SaveAnimal(Animal animal)
        {
            using var connection = GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Animals (Name, Species, Habitat, Diet, Disposition, Size, Strength, Dexterity, Constitution, Intelligence, Wisdom, Charisma)
                VALUES (@Name, @Species, @Habitat, @Diet, @Disposition, @Size, @Strength, @Dexterity, @Constitution, @Intelligence, @Wisdom, @Charisma);
            ";

            command.Parameters.AddWithValue("@Name", animal.Name);
            command.Parameters.AddWithValue("@Species", animal.Species);
            command.Parameters.AddWithValue("@Habitat", animal.Habitat);
            command.Parameters.AddWithValue("@Diet", animal.Diet.ToString());
            command.Parameters.AddWithValue("@Disposition", animal.Disposition);
            command.Parameters.AddWithValue("@Size", animal.Size.ToString());
            command.Parameters.AddWithValue("@Strength", Convert.ToInt32(animal.Strength));
            command.Parameters.AddWithValue("@Dexterity", Convert.ToInt32(animal.Dexterity));
            command.Parameters.AddWithValue("@Constitution", Convert.ToInt32(animal.Constitution));
            command.Parameters.AddWithValue("@Intelligence", Convert.ToInt32(animal.Intelligence));
            command.Parameters.AddWithValue("@Wisdom", Convert.ToInt32(animal.Wisdom));
            command.Parameters.AddWithValue("@Charisma", Convert.ToInt32(animal.Charisma));

            // DEBUG
            Console.WriteLine($"Executing: {command.CommandText}");
            foreach (SQLiteParameter param in command.Parameters)
            {
                Console.WriteLine($"{param.ParameterName}: {param.Value}");
            }


            command.ExecuteNonQuery();
            Console.WriteLine($"Animal '{animal.Name}' saved to database.");
        }

        public static void SaveItem(Item item)
        {
            using var connection = GetConnection();
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Items (Name, Description, Type, Value, Quantity, Weight)
                VALUES (@Name, @Description, @Type, @Value, @Quantity, @Weight);
            ";

            command.Parameters.AddWithValue("@Name", item.Name);
            command.Parameters.AddWithValue("@Description", item.Description);
            command.Parameters.AddWithValue("@Type", item.Type);
            command.Parameters.AddWithValue("@Value", item.Value);
            command.Parameters.AddWithValue("@Quantity", item.Quantity);
            command.Parameters.AddWithValue("@Weight", item.Weight);

            command.ExecuteNonQuery();
            Console.WriteLine($"Item '{item.Name}' saved to database.");
        }

        private static string SerializeInventory(List<Item> inventory)
        {
            return string.Join(";", inventory.Select(i => $"{i.Name}:{i.Quantity}"));
        }

        private static string SerializePersonality(Dictionary<string, string> personality)
        {
            return string.Join(";", personality.Select(kvp => $"{kvp.Key}:{kvp.Value}"));
        }
    }
}
