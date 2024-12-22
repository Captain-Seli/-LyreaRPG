// LocationsHelper.cs - Updated for Hierarchical Navigation
using System;
using System.Linq;
using LyreaRPG.Characters;
using LyreaRPG.World;
using LyreaRPG.Utils;

namespace LyreaRPG.Utils
{
    public static class LocationsHelper
    {
        public static void ExploreRegion(Region region, Player player)
        {
            bool exploring = true;

            while (exploring)
            {
                Console.Clear();
                Console.WriteLine($"Region: {region.Name}");
                Console.WriteLine(region.Description);
                Console.WriteLine("\nLocations:");

                foreach (var location in region.Locations)
                {
                    Console.WriteLine($"  - {location.Name}: {location.ShortDescription}");
                }

                Console.WriteLine("\nActions:");
                Console.WriteLine("  0. Go Back");
                Console.WriteLine("  4. Inventory");
                Console.WriteLine("  5. Stats and Skills");
                Console.WriteLine("  6. Where am I?");
                Console.WriteLine("  8. Equip Item");
                Console.WriteLine("Choose a location to explore:");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        exploring = false;
                        break;
                    case "4":
                        ActionsHelper.ShowInventory(player);
                        break;
                    case "5":
                        ActionsHelper.ShowStats(player);
                        break;
                    case "6":
                        ActionsHelper.ShowCurrentLocation(player);
                        break;
                    case "7": // Equip Item action
                        ActionsHelper.EquipItem(player);
                        break;
                    default:
                        var selectedLocation = region.Locations.Find(l => l.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
                        if (selectedLocation != null)
                        {
                            player.SetLocation(region.Name, selectedLocation.Name); // Move player to the selected location
                            ExploreLocation(region, selectedLocation, player); // Delegate to ExploreLocation
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Press any key to try again.");
                            Console.ReadKey();
                        }
                        break;
                }
            }
        }

        public static void ExploreLocation(Region region, Location location, Player player)
        {
            bool inLocation = true;

            while (inLocation)
            {
                Console.Clear();
                Console.WriteLine($"Location: {location.Name}");
                Console.WriteLine(location.LongDescription);

                Console.WriteLine("\nPoints of Interest:");
                foreach (var poi in location.PointsOfInterest)
                {
                    Console.WriteLine($"  - {poi.Name}");
                }

                Console.WriteLine("\nActions:");
                Console.WriteLine("  0. Go Back");
                Console.WriteLine("  4. Inventory");
                Console.WriteLine("  5. Stats and Skills");
                Console.WriteLine("  6. Where am I?");
                Console.WriteLine("  8. Equip Item");
                Console.WriteLine("Choose a Point of Interest to visit:");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        inLocation = false;
                        break;
                    case "4":
                        ActionsHelper.ShowInventory(player);
                        break;
                    case "5":
                        ActionsHelper.ShowStats(player);
                        break;
                    case "6":
                        ActionsHelper.ShowCurrentLocation(player);
                        break;
                    case "7": // Equip Item action
                        ActionsHelper.EquipItem(player);
                        break;
                    default:
                        var poi = location.PointsOfInterest.Find(p => p.Name.Equals(input, StringComparison.OrdinalIgnoreCase));
                        if (poi != null)
                        {
                            player.SetPOI(region.Name, location.Name, poi.Name); // Move player to the selected POI
                            ExplorePOI(poi, player); // Explore actions at the POI
                        }
                        else
                        {
                            Console.WriteLine("Invalid choice. Press any key to try again.");
                            Console.ReadKey();
                        }
                        break;
                }
            }
        }

        public static void ExplorePOI(PointOfInterest poi, Player player)
        {
            bool atPOI = true;

            while (atPOI)
            {
                Console.Clear();
                poi.DisplayDetails();

                Console.WriteLine("\nActions:");
                Console.WriteLine("  0. Go Back");
                Console.WriteLine("  4. Inventory");
                Console.WriteLine("  5. Stats and Skills");
                Console.WriteLine("  6. Where am I?");
                Console.WriteLine("  7. Interact with POI");
                Console.WriteLine("  8. Equip Item");
                Console.WriteLine("Choose an action:");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        atPOI = false;
                        break;
                    case "4":
                        ActionsHelper.ShowInventory(player);
                        break;
                    case "5":
                        ActionsHelper.ShowStats(player);
                        break;
                    case "6":
                        ActionsHelper.ShowCurrentLocation(player);
                        break;
                    case "7":
                        Console.Clear();
                        POIActionsHelper.HandlePOIActions(poi.Type, player); // Perform actions based on POI type
                        break;
                    case "8": // Equip Item action
                        ActionsHelper.EquipItem(player);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
