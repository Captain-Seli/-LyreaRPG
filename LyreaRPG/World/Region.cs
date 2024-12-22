namespace LyreaRPG.World
{
    public class Region
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Location> Locations { get; set; } = new();

        public Region(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void AddLocation(Location location)
        {
            Locations.Add(location);
        }

        public void DisplayRegionDetails()
        {
            Console.WriteLine($"Region: {Name}");
            Console.WriteLine(Description);
            Console.WriteLine("Locations:");
            foreach (var location in Locations)
            {
                Console.WriteLine($"  - {location.Name}: {location.ShortDescription}");
            }
        }
    }
}
