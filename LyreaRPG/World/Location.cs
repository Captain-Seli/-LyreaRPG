namespace LyreaRPG.World
{
    public class PointOfInterest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // e.g., "shop", "wilderness", "inn"

        public PointOfInterest(string name, string description, string type)
        {
            Name = name;
            Description = description;
            Type = type;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Point of Interest: {Name}");
            Console.WriteLine(Description);
        }
    }

    public class Location
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<PointOfInterest> PointsOfInterest { get; set; } = new();
        public List<Location> Neighbors { get; set; } = new();

        public Location(string name, string shortDescription, string longDescription)
        {
            Name = name;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
        }

        public void AddPointOfInterest(PointOfInterest poi)
        {
            PointsOfInterest.Add(poi);
        }

        public void AddNeighbor(Location neighbor)
        {
            Neighbors.Add(neighbor);
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Location: {Name}");
            Console.WriteLine(LongDescription);
            Console.WriteLine("Points of Interest:");
            foreach (var poi in PointsOfInterest)
            {
                Console.WriteLine($"  - {poi.Name}");
            }

            Console.WriteLine("Neighboring Locations:");
            foreach (var neighbor in Neighbors)
            {
                Console.WriteLine($"  - {neighbor.Name}");
            }
        }
    }
}
