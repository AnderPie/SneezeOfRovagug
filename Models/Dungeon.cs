using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    public class Dungeon
    {
        public string DungeonTypeString { get; set; }
        public DungeonType DungeonType { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfLevels { get; set; }
        public bool UnderDarkEntrance { get; set; }

        public List<Room> Rooms { get; set; }

        public List<Tuple<int, Encounter>> RandomEncounters { get; set; }

        public Dungeon() // For deserialization
        {

        }

        public static Dungeon Create(DungeonType? dungeonType = null, int? numberRooms = null, int? numberLevels = null)
        {
            Random rand = new();

            Dungeon d = new() ;
            // Determine ruin type
            if (dungeonType is null)
            {
                
                d.DungeonType = (DungeonType)rand.Next(1, 15);
            }
            else
            {
                d.DungeonType = (DungeonType)dungeonType;
            }
            d.DungeonTypeString = d.DungeonType.ToString();
            // Determine number of rooms to generate
            if (numberRooms is null)
            {
                d.NumberOfRooms = rand.Next(4, 60);

            }
            else
            {
                d.NumberOfRooms = (int)numberRooms;
            }

            // Determine number of levels in the ruin (rooms will be distributed amongst them)
            if (numberLevels is null)
            {

                d.NumberOfLevels = rand.Next(1, 4); 

            }
            else
            {
                d.NumberOfLevels = (int)numberLevels;
            }

            // Generate the contents of rooms
            d.Rooms = d.GenerateRooms();

            // Generate the random encounter table
            d.RandomEncounters = d.GenerateEncounters();

            return d;
        }

        /// <summary>
        /// Uses the EncounterTable class to generate a table of random encounters.
        /// Random encounters are informed by the RuinType (clockwork beasts for a abandonded workshop, for instance.)
        /// </summary>
        /// <returns>A list of encounters and the roll on a d100 to trigger that encounter. </returns>
        private List<Tuple<int, Encounter>> GenerateEncounters()
        {
            return new();
        }

        private List<Room> GenerateRooms()
        {
            return new();
        }
    }
}

