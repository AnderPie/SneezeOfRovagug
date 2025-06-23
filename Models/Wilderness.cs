using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    public class Wilderness : Tile
    {
        public List<string> WildernessEffectsString { get; set; }
        public List<WildernessEffects> Effects { get; set; }
        public List<Monster> RaidingParties { get; set; }
        public List<string> FeaturesString { get; set; }

        public List<Monster> DensAsMonster { get; set; }
        public List<WildernessFeature> Features { get; set; }

        private int NumDens = 2;
        public List<Den> Dens { get; set; }
        private int NumDungeons = 2;
        public List<Dungeon> Dungeons { get; set; }
        private int NumRuins = 2;
        public List<Ruin> Ruins { get; set; } // Essentially just a dungeon, but above ground
        public List<OrganizedCrime>? OrganizedCriminals;

        public List<EnvironmentTag> EnvironmentTags { get; set; }
        public List<CreatureTypeTag> CreatureTypeTags { get; set; }
        public List<CreatureFamilyTag> CreatureFamilyTags { get; set; }
        //public List<string> Keywords { get; set; } // Used to search the Monster Manual for potential encounters.
        EncounterTable EncounterTable { get; set; }

        public List<Monster> Monsters { get; set; }
        public Wilderness(Biome biome, int lat, int lon, TileSubType? subType = null )
        {
            Biome = biome;
            TileType = "Wilderness";
            Random rand = new();
            if (subType is null)
            {
                SubType = (TileSubType)rand.Next(5, 11);
            }
            else
            {
                SubType = (TileSubType)subType;
            }
            Name = SubType.ToString(); // Eventually we should do some fun random name generation
            GenerateEffects();
            GenerateFeatures();
            GenerateTagsFromFeatures();
            GenerateRuins();
            GenerateDungeons();
            GenerateEncounterTable();
            GenerateDens();
            foreach(Den den in Dens!)
            {
                if(DensAsMonster is null)
                {
                    DensAsMonster = new();
                }
                else
                {
                    DensAsMonster.Add(den.Monster);
                }
            }
            GenerateFeatureString();
            GenerateEffectsString();
            Lat = lat;
            Lon = lon;
            Monsters = EncounterTable!.Monsters;
        }


        public Wilderness() // For deserialization
        {
            
        }

        // Probably need custom constrocturs for the JSON
        private void GenerateEffectsString()
        {
            WildernessEffectsString = new();
            foreach (WildernessEffects effect in Effects)
            {
                WildernessEffectsString.Add(effect.ToString());
            }
        }

        private void GenerateFeatureString()
        {
            FeaturesString = new();
            foreach (WildernessFeature feature in Features)
            {
                FeaturesString.Add(feature.ToString());
            }
        }

        private void GenerateEffects()
        {
            if(Effects is null)
            {
                Effects = new();
            }
            Random rand = new(); // Should really become a singleton. Oh well
            int roll = rand.Next(1, 15);
            if(roll < 12)
            {
                Effects.Add((WildernessEffects)roll);
            }
            else
            {
                Effects.Add(WildernessEffects.Raiding_Party);
            }

            if (Effects.Contains(WildernessEffects.Raiding_Party))
            {
                GenerateRaidingParty();
            }

            if (Effects.Contains(WildernessEffects.Wandering_Predator))
            {
                NumDens++; // Add a den, representing the monster in question
            }
        }

        // Will lead to undesirable switching of raider type if effects are added mid game. Think this over.
        private void GenerateRaidingParty()
        {
            Random rand = new();
            if(RaidingParties is null)
            {
                RaidingParties = new();
            }
            else
            {
                RaidingParties.Clear();
            }
                
            foreach(WildernessEffects raiders in Effects.Where(x=> x == WildernessEffects.Raiding_Party))
            {
                IEnumerable<Monster> humanoidRaiders = MonsterManual.Monsters.Where(monster => monster.Keywords.Contains("Humanoid"));
                int roll = rand.Next(0, humanoidRaiders.Count());
                RaidingParties.Add(humanoidRaiders.ToList()[roll]);
            }
        }

        private void GenerateFeatures()
        {
            Random rand = new();
            if(Features is null)
            {
                Features = new();
            }
            int iterator = 5; // Number of features to generate. Probably should be a global constant but it can live here for now.
            while(iterator > 0)
            {
                int roll = rand.Next(0, 24);
                if(roll < 6)
                {
                    GenerateEffects(); // Add another effect
                }
                else if(roll < 10) 
                {
                    NumDens++; // Add another monster den
                }
                else if(roll < 12)
                {
                    if(OrganizedCriminals is null)
                    {
                        OrganizedCriminals = new();
                        int crimeRoll = rand.Next(1, 4);
                        OrganizedCriminals.Add((OrganizedCrime)roll);
                    }
                }
                else if(roll < 14)
                {
                    NumRuins++; 
                }
                else if(roll < 17)
                {
                    NumDungeons++;
                }
                else if(roll < 18)
                {
                    Features.Add(WildernessFeature.Trappers);
                }
                else if (roll < 19)
                {
                    Features.Add(WildernessFeature.Refreshing_Glen); // Arguably WildernessFeature should be an enum...
                }
                else if (roll < 20)
                {
                    Features.Add(WildernessFeature.Druid_Garden); // Arguably WildernessFeature should be an enum...
                }
                else if (roll < 21)
                {
                    Features.Add(WildernessFeature.Wizard_Keep); // Arguably WildernessFeature should be an enum...
                }
                else if (roll < 22)
                {
                    Features.Add(WildernessFeature.Deep_Pool); // Arguably WildernessFeature should be an enum...
                }
                else if (roll < 23)
                {
                    Features.Add(WildernessFeature.Marsh); // Arguably WildernessFeature should be an enum...
                }
                else if (roll < 24)
                {
                    Features.Add(WildernessFeature.Ranger_Outpost); // Arguably WildernessFeature should be an enum...
                }
                iterator--;
            }
        }

        public void GenerateWildernessTagsFromDungeons(Wilderness parent) // probably does not need to be public
        {

            if (Dungeons.Where(x => x.DungeonType == DungeonType.Natural_Cave).Count() > 0)
            {
                EnvironmentTags.Add(EnvironmentTag.Underground);
                // Cave is a keyword as well... I worry that my current enums aren't granular enough
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Necromancer_Hideout).Count() > 0)
            {
                CreatureTypeTags.Add(CreatureTypeTag.Undead);
                CreatureTypeTags.Add(CreatureTypeTag.Necromancer);
                CreatureTypeTags.Add(CreatureTypeTag.Wizard);
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Ancient_Tomb).Count() > 0)
            {
                CreatureTypeTags.Add(CreatureTypeTag.Undead);
            }
            /* CONVERT TO CREATURETYPETAGS
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Cult_Hideout).Count() > 0)
            {
                Keywords.Add("Infernal"); // Eventually I would like to add granularity to the type of cults, but for now we can assume they're all demon worshippers
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Bandit_Base).Count() > 0)
            {
                Keywords.Add("Humanoid");
                Keywords.Add("Bandit");
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Underdark_Entrance).Count() > 0)
            {
                Keywords.Add("Underdark");
                Keywords.Add("Subterranean");
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Wizard_Lair).Count() > 0)
            {
                Keywords.Add("Wizard");
                Keywords.Add("Arcane");
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Dragon_Lair).Count() > 0)
            {
                Keywords.Add("Dragon");
                Keywords.Add("Kobold");
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Aboleth_Pool).Count() > 0)
            {
                Keywords.Add("Aboleth");
                Keywords.Add("Aquatic");
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.MindFlayer_Lair).Count() > 0)
            {
                Keywords.Add("Mindflayer");
                Keywords.Add("Psychic");
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Mad_Machinist_Lair).Count() > 0)
            {
                Keywords.Add("Mechanic");
            }
            // Should really extract the type of humanoid from the dungeon and then add that as a keyword. Unfortunately don't have things wired up that way yet. Soon I guess!
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Humanoid_Stronghold).Count() > 0)
            {
                Keywords.Add("Humanoid");
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Cave_Druid_Glen).Count() > 0)
            {
                Keywords.Add("Cave");
                Keywords.Add("Subterranean");
                Keywords.Add("Druid");
            }
            */
        }

        private void GenerateTagsFromFeatures()
        {
            if(CreatureFamilyTags is null)
            {
                CreatureTypeTags = new();
            }
            if(CreatureFamilyTags is null)
            {
                CreatureFamilyTags = new();
            }
            if (Effects.Contains(WildernessEffects.Druid_Conclave))
            {
               CreatureTypeTags.Add(CreatureTypeTag.Druid);
            }
            if (Effects.Contains(WildernessEffects.Reality_Rift_Fae))
            {
                CreatureTypeTags.Add(CreatureTypeTag.Fae);
            }
            if (Effects.Contains(WildernessEffects.Reality_Rift_Elemental))
            {
                CreatureTypeTags.Add(CreatureTypeTag.Elemental);
            }
            if (Effects.Contains(WildernessEffects.Reality_Rift_Infernal))
            {
                CreatureTypeTags.Add(CreatureTypeTag.Infernal);
            }
            if (Effects.Contains(WildernessEffects.Reality_Rift_Celestial))
            {
                CreatureTypeTags.Add(CreatureTypeTag.Celestial);
            }
        }


        private void GenerateDungeons()
        {
            Dungeons = new();
            while(NumDungeons > 0)
            {
                Dungeons.Add(Dungeon.Create());
                NumDungeons--;
            }
        }

        private void GenerateRuins()
        {
            if(Ruins is null)
            {
                Ruins = new();
            }
        /*  CONVERT TO CreatureTypeTags + EnvironmentTags based system!
            while(NumRuins > 0)
            {
                Ruins.Add(Ruin.Create());
                NumRuins--;
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Small_Abandoned_Mage_Tower || x.RuinType == RuinType.Small_Abandoned_Mage_Tower).Count() > 0)
            {
                Keywords.Add("Mage");
                Keywords.Add("Arcane");
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Small_Abandoned_Workshop|| x.RuinType == RuinType.Small_Abandoned_Workshop).Count() > 0)
            {
                Keywords.Add("Mechanic");
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Genetic_Experimentation_Lab).Count() > 0)
            {
                Keywords.Add("Psychic");
                Keywords.Add("Genetic Experimentation");
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Infernal_Gate).Count() > 0)
            {
                Keywords.Add("Infernal");
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Celestial_Gate).Count() > 0)
            {
                Keywords.Add("Celestial");
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Many_Doors_Gate).Count() > 0)
            {
                Keywords.Add("Planeswalker"); // I need to make more use of this keyword!
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Arid_Gate).Count() > 0)
            {
                Keywords.Add("Arid");
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Frigid_Gate).Count() > 0)
            {
                Keywords.Add("Frigid"); // That's odd, why is this polar bear wandering around the rainforest?
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Tropical_Gate).Count() > 0)
            {
                Keywords.Add("Tropical");
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Temperate_Gate).Count() > 0)
            {
                Keywords.Add("Temperate");
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Ethereal_Gate).Count() > 0)
            {
                Keywords.Add("Ethereal");
            }
        */
        }

        // Should change this to exclusively hold predators, rather than any monster returned by encounter table
        private void GenerateDens()
        {
            while(NumDens > 0)
            {
                if(Dens is null)
                {
                    Dens = new();
                }
                Dens.Add(new(EncounterTable.GenerateMonster()));
                NumDens--;
            }
        }

        // Encounter tables empty until we appropriately refactor the tagging system. :( 
        private void GenerateEncounterTable()
        {
            List<Monster> myMonsters = new();
            //Keywords.Add("Common");
            /*
            foreach(string keyword in Keywords)
            {
                IEnumerable<Monster> monsters = MonsterManual.Monsters.Where(x => x.Keywords.Contains(keyword));

                myMonsters.AddRange(monsters);
            }
            */
            EncounterTable = new(myMonsters);
        }
    }
}
