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
        public List<CreatureTraitTag> CreatureTraitTags { get; set; }
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
            Random rand = new(); // TODO rand should be a singleton
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
                IEnumerable<Monster> humanoidRaiders = MonsterManual.Monsters.Where(monster => monster.CreatureTraitTags.Contains(CreatureTraitTag.Humanoid));
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
                CreatureTraitTags.Add(CreatureTraitTag.Undead);
                CreatureTraitTags.Add(CreatureTraitTag.Necromancy);
                CreatureTraitTags.Add(CreatureTraitTag.Arcane);
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Ancient_Tomb).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Undead);
            }
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Cult_Hideout).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Demon);
                CreatureTraitTags.Add(CreatureTraitTag.Devil);
                CreatureTraitTags.Add(CreatureTraitTag.Daemon);
                CreatureTraitTags.Add(CreatureTraitTag.Humanoid);
                CreatureTraitTags.Add(CreatureTraitTag.Occult);
            }
            
            if (Dungeons.Where(x => x.DungeonType == DungeonType.Bandit_Base).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Humanoid);
            }
            // TODO CONVERT TO CREATURETYPETAGS

            /*
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
                CreatureTraitTags = new();
            }
            if(CreatureFamilyTags is null)
            {
                CreatureFamilyTags = new();
            }
            if (Effects.Contains(WildernessEffects.Druid_Conclave))
            {
               CreatureTraitTags.Add(CreatureTraitTag.Elemental);
               CreatureTraitTags.Add(CreatureTraitTag.Plant);
            }
            if (Effects.Contains(WildernessEffects.Reality_Rift_Fae))
            {
                CreatureTraitTags.Add(CreatureTraitTag.Fey);
            }
            if (Effects.Contains(WildernessEffects.Reality_Rift_Elemental))
            {
                CreatureTraitTags.Add(CreatureTraitTag.Elemental);
            }
            if (Effects.Contains(WildernessEffects.Reality_Rift_Infernal))
            {
                CreatureTraitTags.Add(CreatureTraitTag.Demon);
                CreatureTraitTags.Add(CreatureTraitTag.Devil);
                CreatureTraitTags.Add(CreatureTraitTag.Daemon);
            }
            if (Effects.Contains(WildernessEffects.Reality_Rift_Celestial))
            {
                CreatureTraitTags.Add(CreatureTraitTag.Celestial);
                CreatureTraitTags.Add(CreatureTraitTag.Angel);
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
            while(NumRuins > 0)
            {
                Ruins.Add(Ruin.Create());
                NumRuins--;
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Small_Abandoned_Mage_Tower || x.RuinType == RuinType.Small_Abandoned_Mage_Tower).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Arcane);
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Small_Abandoned_Workshop|| x.RuinType == RuinType.Small_Abandoned_Workshop).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Construct);
                CreatureTraitTags.Add(CreatureTraitTag.Clockwork);
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Genetic_Experimentation_Lab).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Aberration);
                CreatureTraitTags.Add(CreatureTraitTag.Psychopomp);
                CreatureTraitTags.Add(CreatureTraitTag.Mutant);
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Infernal_Gate).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Demon);
                CreatureTraitTags.Add(CreatureTraitTag.Daemon);
                CreatureTraitTags.Add(CreatureTraitTag.Devil);
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Celestial_Gate).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Celestial);
                CreatureTraitTags.Add(CreatureTraitTag.Angel);
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Many_Doors_Gate).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Aasimar);
                CreatureTraitTags.Add(CreatureTraitTag.Aeon);
                CreatureTraitTags.Add(CreatureTraitTag.Aesir);
                CreatureTraitTags.Add(CreatureTraitTag.Agathion);
                CreatureTraitTags.Add(CreatureTraitTag.Air);
                CreatureTraitTags.Add(CreatureTraitTag.Fire);
                CreatureTraitTags.Add(CreatureTraitTag.Elemental);
                CreatureTraitTags.Add(CreatureTraitTag.Unholy);
                // Right now this is just literally 'many doors' to planes, but later I am thinking of it as a door to the plane of many doors.
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Arid_Gate).Count() > 0)
            {
                // TODO: terrain and biome feature needs to be implemented
                //
                //CreatureTypeTags.Add(CreatureTraitTag);
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Frigid_Gate).Count() > 0)
            {
                // TODO: terrain and biome feature needs to be implemented
                CreatureTraitTags.Add(CreatureTraitTag.Cold);
                //Keywords.Add("Frigid"); // That's odd, why is this polar bear wandering around the rainforest?
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Tropical_Gate).Count() > 0)
            {
                //CreatureTypeTags.Add(CreatureTraitTag.);
                // TODO: terrain and biome features need to be implemented
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Temperate_Gate).Count() > 0)
            {
                // Todo: dont make me say it ago
            }
            if (Ruins.Where(x => x.RuinType == RuinType.Abandoned_Ethereal_Gate).Count() > 0)
            {
                CreatureTraitTags.Add(CreatureTraitTag.Ethereal);
            }
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
        // TODO figure out why only some wilderness types (mountain and desert ?? ) are properly generating creatures. 
        // TODO Implement biome system
        // Encounter tables empty until we appropriately refactor the tagging system. :( 
        private void GenerateEncounterTable()
        {
            List<Monster> myMonsters = new();
            
            
            foreach(CreatureTraitTag tag in CreatureTraitTags)
            {
                IEnumerable<Monster> monsters = MonsterManual.Monsters.Where(x => x.CreatureTraitTags.Contains(tag));

                myMonsters.AddRange(monsters);
            }
            EncounterTable = new(myMonsters);
        }
    }
}
