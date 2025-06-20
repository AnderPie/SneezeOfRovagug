using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    public class Ruin
    {
        public string RuinTypeString { get; set; }
        public RuinType RuinType { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfLevels { get; set; }

        public List<Room> Rooms { get; set; }

        public List<Tuple<int, Encounter>> RandomEncounters { get; set; }

        public Ruin() // For deserialization
        {

        }

        public static Ruin Create(RuinType? ruinType = null, int? numberRooms = null, int? numberLevels = null)
        {
            Random rand = new();

            Ruin r = new();

            // Determine ruin type
            if (ruinType is null)
            {
                int roll = rand.Next(1, 11);
                if (roll == 10) // A gate to another plane, or to another region of the game world
                {
                    roll = rand.Next(90, 99);
                }
                r.RuinType = (RuinType)roll;
            }
            else
            {
                r.RuinType = (RuinType)ruinType;
            }
            r.RuinTypeString = r.RuinType.ToString();
            // Determine number of rooms to generate
            if (numberRooms is null)
            {
                if (r.RuinType.ToString().Contains("Small"))
                {
                    r.NumberOfRooms = rand.Next(1, 11);
                }
                else
                {
                    r.NumberOfRooms = rand.Next(1, 30);
                }
            }
            else
            {
                r.NumberOfRooms = (int)numberRooms;
            }

            // Determine number of levels in the ruin (rooms will be distributed amongst them)
            if (numberLevels is null)
            {
                if (r.RuinType.ToString().Contains("Small"))
                {
                    r.NumberOfLevels = rand.Next(1, 3); // Small ruins have at most two levels
                }
                else
                {
                    r.NumberOfLevels = rand.Next(1, 6); // Whereas larger ruins may have up to 5
                }
            }
            else
            {
                r.NumberOfLevels = (int)numberLevels;
            }

            // Generate the contents of rooms
            r.Rooms = r.GenerateRooms();

            // Generate the random encounter table
            r.RandomEncounters = r.GenerateEncounters();

            return (r);
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
