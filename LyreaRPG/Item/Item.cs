namespace LyreaRPG.Items
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // E.g., "Weapon", "Armor", "Consumable", "Material"

        public int Value { get; set; } // Price in gold or relevant currency
        public int Quantity { get; set; } = 1;
        public bool IsAmmo { get; } = false; // Indicates if the item is ammunition
        public double Weight { get; } = 0; // Weight of the item

        public Item(string name, string description, string type, int value, int quantity = 1, bool isAmmo = false, double weight = 0)
        {
            Name = name;
            Description = description;
            Type = type;
            Value = value;
            Quantity = quantity;
            IsAmmo = isAmmo;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"{Name} ({Type}) - {Description} (x{Quantity}, Weight: {Weight})";
        }
    }
}
