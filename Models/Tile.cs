using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using DnDGenerator.StaticCollections;

namespace DnDGenerator.Models
{

    [JsonDerivedType(typeof(Settlement), nameof(Settlement))]
    [JsonDerivedType(typeof(Wilderness), nameof(Wilderness))]

    public class Tile
    {
        public Biome Biome { get; set; }
        public string Name { get; set; }
        public string TileType { get; set; }
        public TileSubType SubType { get; set; }

        [JsonIgnore]
        public List<Tile>? Neighbors { get; set; }

        public int Lat { get; set; } = 0;
        public int Lon { get; set; } = 0;

        public void SetNeighbors(List<Tile> tiles)
        {
            Neighbors = tiles.Where(x => (Math.Abs(x.Lon - Lon) < 2 && Math.Abs(x.Lat - Lat) < 2) && x != this).ToList();
        }
    }
}
