using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    // A collection of contiguous tiles
    // Currently 5 by 5
    public class Region
    {
        [JsonIgnore]
        public Tile[][] Tiles { get; set; }
        // Sloppy I know
        public List<Tile> TilesList { get; set; }
        public List<RegionalThreats> RegionalThreats { get; set; }
        public Biome Biome { get; set; }

        [JsonConstructor]
        public Region(List<Tile> TilesList, List<RegionalThreats> RegionalThreats, Biome Biome) // Used when deserializing
        {
            this.TilesList = TilesList;
            this.RegionalThreats = RegionalThreats;
            this.Biome = Biome;
        }

        public Region(Biome? biome = null, List<RegionalThreats>? threats = null) // The UseMe bool is used to separate this from the parameterless constructor, and is actually insane
        {
            Random rand = new();
            if(biome is null)
            {
                Biome = (Biome)rand.Next(1, 5);
            }
            PopulateRegion();
            if(threats is null)
            {
                RegionalThreats = GenerateRegionalThreats();
            }
            else
            {
                RegionalThreats = threats;
            }
        }

        // Fully implement later
        // These threats modify the keywords of all tiles in the region, as well as settlement conditions and wilderness properties
        // IE a chronic raiding problem will impose the 'raided' condition on 50% of settlements and generate a lot of raiding parties
        private List<RegionalThreats> GenerateRegionalThreats()
        {
            Random rand = new();
            List<RegionalThreats> threats = new();
            threats.Add((RegionalThreats)rand.Next(1, 19));
            return threats;
        }

        // Tile biome inherits from region biome
        private void PopulateRegion()
        {
            TilesList = new();
            Random rand = new();

            Tiles =
               [
                [new(), new(), new(), new(), new()],
                [new(), new(), new(), new(), new()],
                [new(), new(), new(), new(), new()],
                [new(), new(), new(), new(), new()],
                [new(), new(), new(), new(), new()]];

            // Harcoded region size for the moment. Bad but serviceable
            for (int x = 0; x < 5; x++)
            {
                for(int y = 0; y < 5; y++)
                {
                    // To produce fewer settlements
                    int roll = rand.Next(1, 13);
                    if(roll < 4)
                    {

                    }
                    else
                    {
                        roll = rand.Next(4, 9);
                    }
                    TileSubType nextType = (TileSubType)roll;
                    if (nextType == TileSubType.Village || nextType == TileSubType.City || nextType == TileSubType.Keep)
                    {
                        Tiles[x][y] = new Settlement(Biome, x, y, nextType);
                    }
                    else
                    {
                        Tiles[x][y] = new Wilderness(Biome, x, y, nextType);
                    }
                    TilesList.Add(Tiles[x][y]);
                }
            }
            foreach(Tile tile in TilesList)
            {
                tile.SetNeighbors(TilesList);
            }
            

        }
    }
}
