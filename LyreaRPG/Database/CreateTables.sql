-- Create Players Table
CREATE TABLE Players (
    Id INTEGER PRIMARY KEY AUTOINCREMENT 
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
    Inventory TEXT,
    Location TEXT NOT NULL,
    Faction TEXT
);

-- Create NPCs Table
CREATE TABLE IF NOT EXISTS NPCs (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Race TEXT NOT NULL,
    Gender TEXT NOT NULL,
    Background TEXT NOT NULL,
    Faction TEXT,
    Friendliness TEXT,
    Aggression TEXT,
    Personality TEXT,
    Strength INTEGER,
    Dexterity INTEGER,
    Constitution INTEGER,
    Intelligence INTEGER,
    Wisdom INTEGER,
    Charisma INTEGER
);

-- Create Animals Table
CREATE TABLE IF NOT EXISTS Animals (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Species TEXT NOT NULL,
    Size TEXT NOT NULL,
    Habitat TEXT NOT NULL,
    Diet TEXT NOT NULL,
    Inventory TEXT,
    SecondaryInventory TEXT
);

-- Create Items Table
CREATE TABLE IF NOT EXISTS Items (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT,
    Weight REAL NOT NULL,
    Quantity INTEGER NOT NULL
);
