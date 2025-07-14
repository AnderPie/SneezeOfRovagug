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

        public Region(Biome? biome = null, List<RegionalThreats>? threats = null) 
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
                ApplyThreat(tile);
            }
        }

        /// <summary>
        /// Applies regional threats to its tile parameter.
        /// </summary>
        /// <param name="tile"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void ApplyThreat(Tile tile)
        {

            if(RegionalThreats is null)
            {
                RegionalThreats = GenerateRegionalThreats();
            }
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Underdark_Humanoid_Invasion))
            {
                if(nameof(tile) == nameof(Wilderness)) // Seems insane but OK...
                {
                    ((Wilderness)tile).Dungeons.Add(Dungeon.Create(DungeonType.Underdark_Entrance));
                    ((Wilderness)tile).RaidingParties.Add(MonsterManual.ReturnUnderdarkRaider());
                }
            }
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Underdark_Predator))
            {
                if(tile.TileType == "Wilderness")// Seems insane but OK...
                {
                    ((Wilderness)tile).Dens.Add(new(MonsterManual.ReturnUnderdarkPredator()));
                }
            }
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Dragon))
            {
                // We want to add a chance to encounter the same dragon between tiles, but don't have that logic built yet
                WriteLine("Not implemented yet!");
                /*
                if (tile.TileType == "Wilderness")// Seems insane but OK...
                {
                    ((Wilderness)tile).Dens.Add(new(MonsterManual.ReturnUnderdarkPredator()));
                }
                */
            }
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Infernal_Invasion))
            {
                if (tile.TileType == "Wilderness")
                {
                    ((Wilderness)tile).CreatureTraitTags.Add(CreatureTraitTag.Demon);
                    ((Wilderness)tile).CreatureTraitTags.Add(CreatureTraitTag.Devil);
                    ((Wilderness)tile).CreatureTraitTags.Add(CreatureTraitTag.Daemon);
                }
            }
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Aboleth_Invasion))
            {
                if (tile.TileType == "Wilderness")
                {
                    ((Wilderness)tile).CreatureFamilyTags.Add(CreatureFamilyTag.Aboleth);
                }
            }
            // The idea is that if it is within the region, we add a high level stronghold representing the humanoid invader. but that is not implemented yet
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Humanoid_Invasion_Outside_Region) || RegionalThreats!.Contains(StaticCollections.RegionalThreats.Humanoid_Invasion_Within_Region))
            {
                if (tile.TileType == "Wilderness")
                {
                    ((Wilderness)tile).RaidingParties.Add(MonsterManual.ReturnRaider());
                }
            }
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Necromancer))
            {
                if (tile.TileType == "Wilderness")
                {
                    ((Wilderness)tile).CreatureTraitTags.Add(CreatureTraitTag.Undead);
                }
            }
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Behemoth)){
                if (tile.TileType == "Wilderness")
                {
                    ((Wilderness)tile).Dens.Add(new(MonsterManual.ReturnBehemoth())); // Custom tag for very large wandering non-dragon predators
                }
            }
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Druid))
            {
                if (tile.TileType == "Wilderness")
                {
                    ((Wilderness)tile).Features.Add(WildernessFeature.Druid_Garden); 
                }
            }
            if (RegionalThreats!.Contains(StaticCollections.RegionalThreats.Plague))
            {
                if (tile.TileType == "Wilderness")
                {
                    ((Wilderness)tile).Dens.Add(new(MonsterManual.ReturnPestilenceMonster()));
                }
            }
        }
    }
}
