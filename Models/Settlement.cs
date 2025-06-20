using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace DnDGenerator.Models
{
    public class Settlement : Tile
    {
        public List<Person>? ImportantPeople { get; set; }
        public List<string>? Threats { get; set; }
        public List<string>? Quests { get; set; }
        public string PrimaryRace { get; set; }
        public string SecondaryRace { get; set; }
        public string TertiaryRace { get; set; }

        public string PatronDiety { get; set; }
        public string SecondaryDiety { get; set; }

        public string TertiaryDiety { get; set; }
        public List<string>? PowerCenters { get; set; }
        public List<Commodities>? MainIndustries { get; set; } // Also main exports

        // Determined based on main industry
        public List<Commodities>? MainImports { get; set; }

        public List<SettlementCondition>? Conditions { get; set; }

        public Settlement() // For deserialization
        {

        }

        public Settlement(Biome biome)
        {
            Biome = biome;
            TileType = "Settlement";
            GenerateConditions();
            GenerateSubType();
            GenerateDemographics();
            GenerateMainIndustries();
            GenerateImportantPeople();
            Name = GenerateRandomName();
            GenerateNeighbors();

        }

        public Settlement(Biome biome, int lat, int lon, TileSubType subType)
        {
            Biome = biome;
            TileType = "Settlement";
            GenerateConditions();
            SubType = subType;
            GenerateDemographics();
            GenerateMainIndustries();
            GenerateImports();
            GenerateImportantPeople();
            Name = GenerateRandomName();
            GenerateNeighbors();
            Lat = lat;
            Lon = lon;
        }

        private void GenerateImports()
        {
            if(MainImports is null)
            {
                MainImports = new();
            }
            if (MainIndustries!.Contains(Commodities.Ores))
            {
                MainImports.Add(Commodities.Metal_Tools);
            }
            if (MainIndustries.Contains(Commodities.Holy_Site))
            {
                MainImports.Add(Commodities.Incense);
            }
            if (MainIndustries.Contains(Commodities.Metal_Tools))
            {
                MainImports.Add(Commodities.Ores);
            }
            if (MainIndustries.Contains(Commodities.Mechanical_Tools))
            {
                MainImports.Add(Commodities.Ores);
                MainImports.Add(Commodities.Metal_Tools);
            }
            if (MainIndustries.Contains(Commodities.Trained_Priests) || MainIndustries.Contains(Commodities.Trained_Wizards))
            {
                MainImports.Add(Commodities.Herbs); // For rituals and alchemy
                MainImports.Add(Commodities.Incense);
            }
            if (MainIndustries.Contains(Commodities.Mercenaries))
            {
                MainImports.Add(Commodities.Metal_Tools);
                MainImports.Add(Commodities.Weapons);
                MainImports.Add(Commodities.Magical_Armor);
                MainImports.Add(Commodities.Magical_Weapons);
                MainImports.Add(Commodities.Wine);
                MainImports.Add(Commodities.Prostitutes);
                MainImports.Add(Commodities.Horses);
                MainImports.Add(Commodities.Trained_Beasts);
                MainImports.Add(Commodities.War_Machines);
                MainImports.Add(Commodities.Modern_Weapons);
                MainImports.Add(Commodities.Preserved_Foods);
                MainImports.Add(Commodities.Grains);
                MainImports.Add(Commodities.Livestock);
            }
            if (MainIndustries.Contains(Commodities.Trained_Solders))
            {
                MainImports.Add(Commodities.Metal_Tools);
                MainImports.Add(Commodities.Weapons);
                MainImports.Add(Commodities.Magical_Armor);
                MainImports.Add(Commodities.Magical_Weapons);
                MainImports.Add(Commodities.Wine);
                MainImports.Add(Commodities.Prostitutes);
                MainImports.Add(Commodities.Horses);
                MainImports.Add(Commodities.Trained_Beasts);
                MainImports.Add(Commodities.War_Machines);
                MainImports.Add(Commodities.Modern_Weapons);
                MainImports.Add(Commodities.Conscripts);
                MainImports.Add(Commodities.Mercenaries);
                MainImports.Add(Commodities.Preserved_Foods);
                MainImports.Add(Commodities.Grains);
                MainImports.Add(Commodities.Livestock);
            }
            if (MainIndustries.Contains(Commodities.Magical_Weapons))
            {
                MainImports.Add(Commodities.Metal_Tools);
                MainImports.Add(Commodities.Weapons);
                MainImports.Add(Commodities.Mana_Crystals);
            }
            if (MainIndustries.Contains(Commodities.Magical_Armor))
            {
                MainImports.Add(Commodities.Metal_Tools);
                MainImports.Add(Commodities.Weapons);
                MainImports.Add(Commodities.Mana_Crystals);
            }
            if (MainIndustries.Contains(Commodities.Magical_Weapons))
            {
                MainImports.Add(Commodities.Metal_Tools);
                MainImports.Add(Commodities.Weapons);
                MainImports.Add(Commodities.Mana_Crystals);
            }
            if (MainIndustries.Contains(Commodities.War_Machines))
            {
                MainImports.Add(Commodities.Mechanical_Tools);
                MainImports.Add(Commodities.Metal_Tools);
                MainImports.Add(Commodities.Scientists);
            }
            if (MainIndustries.Contains(Commodities.Autonomous_Robots))
            {
                MainImports.Add(Commodities.Mechanical_Tools);
                MainImports.Add(Commodities.Metal_Tools);
                MainImports.Add(Commodities.Mana_Crystals);
                MainImports.Add(Commodities.Scientists);
            }
            if (MainIndustries.Contains(Commodities.Modern_Weapons))
            {
                MainImports.Add(Commodities.Mechanical_Tools);
                MainImports.Add(Commodities.Metal_Tools);
                MainImports.Add(Commodities.Scientists);
            }
            if (MainIndustries.Contains(Commodities.Poisons))
            {
                MainImports.Add(Commodities.Herbs);
                MainImports.Add(Commodities.Scientists);
            }
            if (MainIndustries.Contains(Commodities.Assassins))
            {
                MainImports.Add(Commodities.Poisons);
            }
            if (MainIndustries.Contains(Commodities.Fine_Clothes))
            {
                MainImports.Add(Commodities.Furs);
                MainImports.Add(Commodities.Linen);
                MainImports.Add(Commodities.Herbs); // for dye
            }
            if (MainIndustries.Contains(Commodities.Scientists))
            {
                MainImports.Add(Commodities.Herbs);
                MainImports.Add(Commodities.Metal_Tools);
                MainImports.Add(Commodities.Mechanical_Tools);
                MainImports.Add(Commodities.Mana_Crystals);
            }
            if (MainIndustries.Contains(Commodities.Preserved_Foods))
            {
                MainImports.Add(Commodities.Herbs);
                MainImports.Add(Commodities.Grains);
                MainImports.Add(Commodities.Livestock);
            }
            if (MainIndustries.Contains(Commodities.Potions))
            {
                MainImports.Add(Commodities.Herbs);
                MainImports.Add(Commodities.Mana_Crystals);
                MainImports.Add(Commodities.Trained_Wizards);
                MainImports.Add(Commodities.Scientists);
            }
            if (MainIndustries.Contains(Commodities.Medicines))
            {
                MainImports.Add(Commodities.Herbs);
                MainImports.Add(Commodities.Mana_Crystals);
                MainImports.Add(Commodities.Trained_Wizards);
                MainImports.Add(Commodities.Scientists);
            }

            HashSet<Commodities> importsWithoutDuplicates = new(MainImports);

            MainImports = importsWithoutDuplicates.ToList();
        }

        private void GenerateConditions()
        {
            Conditions = new();
            Random rand = new();
            HashSet<int> conditions = new() { rand.Next(1, 17), rand.Next(1, 17) };
            foreach(int cond in conditions)
            {
                Conditions.Add((SettlementCondition)cond);
            }
        }

        /// <summary>
        /// Generates 8 adjacent tiles, each representing a 9 square league area (3 leagues by 3 leagues)
        /// </summary>
        private void GenerateNeighbors()
        {
            Console.WriteLine("Not generated yet");
        }

        private void GenerateMainIndustries()
        {
            MainIndustries = new();
            Random rand = new();
            List<Commodities> industries;
            if (SubType.ToString() == "Village")
            {
                industries = new() { Commodities.Grains, Commodities.Livestock, Commodities.Ores, Commodities.Conscripts, Commodities.Ceramics, Commodities.Holy_Site, Commodities.Incense,
                    Commodities.Metal_Tools, Commodities.Weapons, Commodities.Furs, Commodities.Herbs, Commodities.Mana_Crystals, Commodities.Mechanical_Tools, Commodities.Wine,
                    Commodities.Horses, Commodities.Preserved_Foods, Commodities.Linen};
                MainIndustries.Add(industries[rand.Next(0, industries.Count())]);
            }
            if (SubType.ToString() == "Keep")
            {
                int industryToAdd;
                // Should add rules for shipbuilding
                industries = new() {  Commodities.Grains, Commodities.Livestock, Commodities.Ores, Commodities.Conscripts, Commodities.Ceramics, Commodities.Holy_Site, Commodities.Incense,
                    Commodities.Metal_Tools, Commodities.Weapons, Commodities.Furs, Commodities.Herbs, Commodities.Mana_Crystals, Commodities.Mechanical_Tools, Commodities.Trained_Wizards,
                    Commodities.Trained_Priests, Commodities.War_Machines, Commodities.Modern_Weapons, Commodities.Autonomous_Robots, Commodities.Fine_Clothes, Commodities.Wine, Commodities.Horses,
                    Commodities.Trained_Beasts, Commodities.Trained_Solders, Commodities.Preserved_Foods};
                industryToAdd = rand.Next(0, industries.Count());
                MainIndustries.Add(industries[industryToAdd]);
                industries.Remove(industries[industryToAdd]);
                industryToAdd = rand.Next(0, industries.Count());
                MainIndustries.Add(industries[industryToAdd]);
                industries.Remove(industries[industryToAdd]);
                industryToAdd = rand.Next(0, industries.Count());
                MainIndustries.Add(industries[industryToAdd]);
                industries.Remove(industries[industryToAdd]);
            }
            if (SubType.ToString() == "City")
            {
                int industryToAdd;
                // Should add rules for shipbuilding
                industries = new() { Commodities.Fine_Clothes, Commodities.Assassins, Commodities.Poisons, Commodities.Medicines, Commodities.Wine,
                    Commodities.Trained_Beasts, Commodities.Trained_Priests, Commodities.Trained_Wizards, Commodities.Mercenaries, Commodities.Trained_Solders,
                    Commodities.Scientists, Commodities.Trained_Priests, Commodities.Prostitutes, Commodities.Ores, Commodities.Conscripts, Commodities.Ceramics, Commodities.Holy_Site, Commodities.Incense,
                    Commodities.Metal_Tools, Commodities.Weapons, Commodities.Furs, Commodities.Herbs, Commodities.Mana_Crystals, Commodities.Mechanical_Tools, Commodities.War_Machines,
                    Commodities.Modern_Weapons, Commodities.Autonomous_Robots, Commodities.Preserved_Foods };
                industryToAdd = rand.Next(0, industries.Count());
                MainIndustries.Add(industries[industryToAdd]);
                industries.Remove(industries[industryToAdd]);
                industryToAdd = rand.Next(0, industries.Count());
                MainIndustries.Add(industries[industryToAdd]);
                industries.Remove(industries[industryToAdd]);
                industryToAdd = rand.Next(0, industries.Count());
                MainIndustries.Add(industries[industryToAdd]);
                industries.Remove(industries[industryToAdd]);
            }
        }

        private void GenerateImportantPeople()
        {
            int settlementLevel;

            if (SubType.ToString() == "Village")
            {
                settlementLevel = 4;
            }
            else
            {
                settlementLevel = 10;
            }

            ImportantPeople = new();
            // Generate a mayor
            ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Politician", settlementLevel, title: "Mayor"));

            // Generate a constable
            ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Fighter", settlementLevel, title: "Constable"));


            // Generate one 'guild leader', 'craftsmen, 'merchant' (etc) for each major industry
            if (MainIndustries!.Contains(Commodities.Grains) || MainIndustries!.Contains(Commodities.Incense) || 
                MainIndustries!.Contains(Commodities.Wine) || MainIndustries!.Contains(Commodities.Preserved_Foods))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Commoner", settlementLevel, title: "Agricultural Authority"));
            }
            if (MainIndustries!.Contains(Commodities.Ores) || MainIndustries!.Contains(Commodities.Mana_Crystals))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Artisan", settlementLevel, title: "Guildmaster: Mining"));
            }
            if (MainIndustries!.Contains(Commodities.Conscripts))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Fighter", settlementLevel + 2, title: "Militia Leader"));
            }
            if (MainIndustries!.Contains(Commodities.Herbs))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Druid", settlementLevel + 2, title: "Guildmaster: Foraging"));
            }
            if (MainIndustries!.Contains(Commodities.Ceramics) )
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Artisan", settlementLevel + 2, title: "Guildmaster: Ceramics"));
            }
            if (MainIndustries!.Contains(Commodities.Fine_Clothes))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Artisan", settlementLevel + 2, title: "Guildmaster: Weaving"));
            }
            if (MainIndustries!.Contains(Commodities.Holy_Site))
            {
                // Alignment should be determined by holy site's patron diety
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Cleric", settlementLevel + 2, title: "High Cleric"));
            }
            if (MainIndustries!.Contains(Commodities.Weapons) || MainIndustries!.Contains(Commodities.Metal_Tools))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Artisan", settlementLevel, title: "Guildmaster: Smithing"));
            }
            if (MainIndustries!.Contains(Commodities.Livestock))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Commoner", settlementLevel, title: "Guildmaster: Ranching"));
            }
            if (MainIndustries!.Contains(Commodities.Furs))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Ranger", settlementLevel, title: "Guildmaster: Trapping"));
            }
            if(MainIndustries!.Contains(Commodities.Assassins))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Assassin", settlementLevel + 5, title: "Guildmaster: Assassination"));
            }
            if (MainIndustries!.Contains(Commodities.Trained_Priests))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Cleric", settlementLevel + 5, title: "Headmaster: Cleric"));
            }
            if (MainIndustries!.Contains(Commodities.Trained_Wizards))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Wizard", settlementLevel + 5, title: "Headmaster: Mage"));
            }
            if (MainIndustries!.Contains(Commodities.Poisons) || MainIndustries!.Contains(Commodities.Medicines) || MainIndustries!.Contains(Commodities.Potions))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Alchemist", settlementLevel + 3, title: "Guildmaster: Alchemy"));
            }
            if (MainIndustries!.Contains(Commodities.Scientists))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Scientist", settlementLevel + 3, title: "Headmaster: Sciences"));
            }
            if (MainIndustries!.Contains(Commodities.Politicians))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Scientist", settlementLevel + 3, title: "Headmaster: Politics"));
            }
            if (MainIndustries!.Contains(Commodities.Trained_Solders) || MainIndustries!.Contains(Commodities.Mercenaries))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Fighter", settlementLevel + 3, title: "Headmaster: Military Science"));
            }
            if (MainIndustries!.Contains(Commodities.Prostitutes))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Rogue", settlementLevel + 3, title: "Guildmaster: Prostitution"));
            }
            if (MainIndustries!.Contains(Commodities.Music_Arts))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Bard", settlementLevel + 3, title: "Guildmaster: The Arts"));
            }
            if (MainIndustries!.Contains(Commodities.Trained_Beasts))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Ranger", settlementLevel + 3, title: "Guildmaster: Trained Beasts"));
            }
            if (MainIndustries!.Contains(Commodities.Magical_Weapons) || MainIndustries!.Contains(Commodities.Magical_Armor))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Artisan", settlementLevel + 3, title: "Guildmaster: Enchanting"));
            }
            if (MainIndustries!.Contains(Commodities.War_Machines) || MainIndustries!.Contains(Commodities.Mechanical_Tools) 
                || MainIndustries!.Contains(Commodities.Autonomous_Robots) || MainIndustries!.Contains(Commodities.Modern_Weapons))
            {
                ImportantPeople.Add(new Person(GenerateNewPersonRace(), "Mechanic", settlementLevel + 3, title: "Guildmaster: Mechanics"));
            }
        }

        private string GenerateNewPersonRace()
        {
            Random rand = new();
            int roll = rand.Next(1, 101);
            List<string> races = new()
            {
                "Human",
                "Halfling",
                "Wood elf",
                "High elf",
                "Dragonborne",
                "Tiefling",
                "Dark elf",
                "Half orc",
                "Gnome",
                "Dwarf",
            };
            races.Remove(PrimaryRace);
            races.Remove(SecondaryRace);
            races.Remove(TertiaryRace);
            if(roll < 61)
            {
                return PrimaryRace;
            }
            else if(roll < 81)
            {
                return SecondaryRace;
            }
            else if(roll < 91)
            {
                return TertiaryRace;
            }
            else
            {
                return races[rand.Next(0, races.Count())];
            }
        }

        private void GenerateDemographics()
        {
            List<string> races = new()
            {
                "Human",
                "Halfling",
                "Wood elf",
                "High elf",
                "Dragonborne",
                "Tiefling",
                "Dark elf",
                "Half orc",
                "Gnome",
                "Dwarf",
            };

            Random rand = new();
            int randNum = rand.Next(0, races.Count);
            PrimaryRace = races[randNum];
            races.Remove(races[randNum]);
            randNum = rand.Next(0, races.Count);
            SecondaryRace = races[randNum];
            races.Remove(races[randNum]);
            randNum = rand.Next(0, races.Count);
            TertiaryRace = races[randNum];
        }

        private void GenerateSubType()
        {
            Random rand = new();
            SubType = (TileSubType)rand.Next(1, 4);
        }

        // Generates a name based on the primary race of the village
        private string GenerateRandomName()
        {
            return RaceNameRepository.ReturnRandomName(PrimaryRace);
        }
    }

    public static class RaceNameRepository
    {
        public static readonly Dictionary<string, List<string>> RaceNames = new()
        {
            { "Human", new List<string> { "Lombardy", "Ashwick", "Elderglen", "Fairford", "Westhaven" } },
            { "Halfling", new List<string> { "Willowbrook", "Bramblefoot", "Tumbledown", "Crickhollow", "Thistlemead" } },
            { "Wood elf", new List<string> { "Sylvaris", "Thalorien", "Lethalas", "Greenveil", "Myr’ithil" } },
            { "High elf", new List<string> { "Elarion", "Caelondor", "Ithilmar", "Aelwynne", "Velas’Tel" } },
            { "Dragonborne", new List<string> { "Drakar", "Vezareth", "Thundrakar", "Rhoktan", "Embercairn" } },
            { "Tiefling", new List<string> { "Nyxhaven", "Duskgate", "Embermoor", "Virethorn", "Cindervault" } },
            { "Dark elf", new List<string> { "Vorn’dar", "Myrkulith", "Neth’thalas", "Xal’Zirra", "Umbralith" } },
            { "Half orc", new List<string> { "Grusk", "Ironhold", "Kragmoor", "Bloodroot", "Brok’Tarr" } },
            { "Gnome", new List<string> { "Tinkerton", "Glimmerforge", "Nibblenock", "Sprockerdale", "Whistlewhim" } },
            { "Dwarf", new List<string> { "Stonehelm", "Karak Vorn", "Dungrin’s Hollow", "Thrainhold", "Bronzebeard’s Rest" } }
        };

        // Not exactly robust but fine for now
        public static string ReturnRandomName(string key)
        {
            Random rand = new();
            int randNum = rand.Next(0, RaceNames[key].Count);
            return RaceNames[key][randNum];

        }
    }

}
