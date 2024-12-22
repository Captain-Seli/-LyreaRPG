using System.Collections.Generic;

namespace LyreaRPG.World
{
    public static class WorldSetup
    {
        public static Region InitializeSunSpur()
        {
            // Create the region
            var sunSpur = new Region(
                "The Sun Spur",
                "A vibrant, sun-drenched peninsula extending into the azure waters of the Cerulean Sea. Known for bustling trade routes, ancient ruins, and untamed wilderness."
            );

            // Define locations
            var portWaveward = new Location(
                "Port Waveward",
                "The bustling heart of the Sun Spur.",
                "The largest city in the Sun Spur, Port Waveward is a vibrant trade hub with a rich cultural mix, bustling markets, and strategic importance."
            );
            portWaveward.AddPointOfInterest(new PointOfInterest("The Harbor", "A bustling hub for ships arriving and departing. You can find passage on a ship here.", "travel"));
            portWaveward.AddPointOfInterest(new PointOfInterest("Captain's Council Hall", "The main government building for the city.", "government"));
            portWaveward.AddPointOfInterest(new PointOfInterest("The Salty Horizon (Inn)", "A popular inn for travelers and sailors.", "inn"));
            portWaveward.AddPointOfInterest(new PointOfInterest("Fort Seaward", "A defensive fort manned by Azonian soldiers and Sailors.", "military"));
            portWaveward.AddPointOfInterest(new PointOfInterest("The Guilded Trident", "An opulent club where the Aristocracy and Nobility hang out.", "inn"));
            portWaveward.AddPointOfInterest(new PointOfInterest("Manor Row", "A Large group of streets where the rich and famous have their manors.", "residential"));
            portWaveward.AddPointOfInterest(new PointOfInterest("The Yatch Yard", "This dock is where the nobility keep their personal watercraft.", "travel"));
            portWaveward.AddPointOfInterest(new PointOfInterest("The Driftwood Inn", "A modest, yet cozy Inn set within a greener, less urban part of the city.", "inn"));
            portWaveward.AddPointOfInterest(new PointOfInterest("The Windswept Greens", "A park with a lovely walk, and memorial for lost sailors.", "park"));
            portWaveward.AddPointOfInterest(new PointOfInterest("Market of the Tides", "A large open air market specializing in catch of the day, as well as exotic goods from all over Lyrea.", "shop"));
            portWaveward.AddPointOfInterest(new PointOfInterest("The Whale Gut Flop House", "A seedy poor house where the down on their luck can find some gruel, flea ridden mattresses, and a good time.", "inn"));
            portWaveward.AddPointOfInterest(new PointOfInterest("The Mariner's Anchor", "Headquarters of the West Azonian Trade Company. You can find an assortment of exotic and rare goods here.", "shop"));
            portWaveward.AddPointOfInterest(new PointOfInterest("The Seaforge", "The main Blacksmith found within Waveward. The Seaforge is famous for its chains and anchors.", "shop"));
            portWaveward.AddPointOfInterest(new PointOfInterest("The Wayward Sailor", "A Ship Chandelier, that supplies many outfits with supplies for various kinds of voyages", "shop"));
            portWaveward.AddPointOfInterest(new PointOfInterest("Powder and Steel", "The premier gunsmith and naval cannon dealer on the Sun Spur.", "shop"));
            // portWaveward.AddPointOfInterest(new PointOfInterest("", ""));
            // portWaveward.AddPointOfInterest(new PointOfInterest("", ""));
            // portWaveward.AddPointOfInterest(new PointOfInterest("", ""));

            var silverfinCove = new Location(
                "Silverfin Cove",
                "A quaint fishing town on the Sun Spur Bay.",
                "Famous for its fresh seafood and treacherous reefs, Silverfin Cove is a modest town with growing political tensions."
            );
            silverfinCove.AddPointOfInterest(new PointOfInterest("The Scaled Market", "A marketplace bustling with fishmongers and traders.", "shop"));
            silverfinCove.AddPointOfInterest(new PointOfInterest("Hullbreaker Salvage", "A salvage operation recovering goods from shipwrecks.", "shop"));
            silverfinCove.AddPointOfInterest(new PointOfInterest("Sun Spur Curios", "A small shop selling trinkets and handcrafted souvenirs made by local artisans.", "shop"));


            var barnacleFlats = new Location(
                "Barnacle Flats",
                "A barren, sandy stretch of wilderness.",
                "The Barnacle Flats are covered in cacti, aloe, and sagebrush. The area is dotted with ruins and home to various dangerous creatures."
            );
            barnacleFlats.AddPointOfInterest(new PointOfInterest("Ancient Ruins", "The remnants of an ancient civilization.", "ruins"));
            barnacleFlats.AddPointOfInterest(new PointOfInterest("Sandy Flats", "A stretch of sandy flats with sparse desert vegetation. There's nothing of note here.", "wilderness"));

            // Connect locations
            portWaveward.AddNeighbor(silverfinCove);
            silverfinCove.AddNeighbor(portWaveward);
            portWaveward.AddNeighbor(barnacleFlats);
            barnacleFlats.AddNeighbor(portWaveward);

            // Add locations to region
            sunSpur.AddLocation(portWaveward);
            sunSpur.AddLocation(silverfinCove);
            sunSpur.AddLocation(barnacleFlats);

            return sunSpur;
        }
    }
}
