using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    public class Room
    {
        public string? NarrativeText { get; set; }
        public Encounter? Encounter { get; set; }
        public List<Treasure>? Treasure { get; set; }

        public List<Trap>? Traps { get; set; }
        public List<string>? Hazards { get; set; }
        public string? Features { get; set; }

        // One per room
        public List<Room> Rooms { get; set; }

    }
}
