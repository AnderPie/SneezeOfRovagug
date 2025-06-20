using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    public class Person
    {
        public string Race { get; set; } 
        public Race? RaceRefactored { get; set; } // Still want the program to compile lol
        public string Name { get; set; }
        public string? Title { get; set; }
        public string CharacterClass { get; set; }
        public int Level { get; set; }
        public Alignment?  Alignment { get; set; }
        public Diety?  PatronDiety { get; set; }

        public Person()
        {

        }

        public Person(string race, string charClass, int lvl, string? name = null, string? title = null, Alignment? alignment = null)
        {
            Race = race;
            Level = lvl;
            Title = title;
            CharacterClass = charClass;
            if(alignment is null)
            {
                Alignment = DetermineAlignment();
                PatronDiety = DetermineReligion();
            }
            else
            {
                Alignment = alignment;
            }
            if (name is null)
            {
                Name = GenerateRandomPersonName(Race);
            }
            else
            {
                Name = name;
            }

            Title = title;
        }

        private Diety DetermineReligion()
        {
            return Pantheon.Dieties.First(); // Finish implementing based off of a probability matrix using race and alignment
        }

        private Alignment DetermineAlignment()
        {
            Random rand = new();
            short roll = (short)rand.Next(1, 10);

            return (Alignment)roll;
        }

        private string GenerateRandomPersonName(string race)
        {
            var rnd = new Random();

            var nameData = new Dictionary<string, (List<string> FirstNames, List<string> LastNames)>
            {
                ["Human"] = (
                    new() { "Alric", "Beatrice", "Cedric", "Elena", "Roderick" },
                    new() { "Stone", "Blackwell", "Kensley", "Marwood", "Thorne" }),

                ["Halfling"] = (
                    new() { "Pip", "Mira", "Dobby", "Rosie", "Waldo" },
                    new() { "Goodbarrel", "Brandybuck", "Greenleaf", "Hilltopple", "Underbough" }),

                ["Wood elf"] = (
                    new() { "Thalanil", "Aeris", "Sylwen", "Faelar", "Lirael" },
                    new() { "Duskrunner", "Leafwhisper", "Moonshade", "Starbrook", "Windgrove" }),

                ["High elf"] = (
                    new() { "Aelion", "Lira", "Vaelis", "Theren", "Elyra" },
                    new() { "Silvercrest", "Brightmoon", "Elarith", "Sunfall", "Starseer" }),

                ["Dragonborne"] = (
                    new() { "Zarkhan", "Emberra", "Kaelthas", "Vessirax", "Dravinaar" },
                    new() { "Flamescale", "Ironwing", "Stormclaw", "Ashbane", "Vulkorr" }),

                ["Tiefling"] = (
                    new() { "Nyx", "Vyre", "Xaros", "Lilura", "Kaelen" },
                    new() { "Duskbane", "Noctalis", "Whisperthorn", "Cindervex", "Shadeborn" }),

                ["Dark elf"] = (
                    new() { "Zirra", "Malreth", "Voriel", "Xune", "Nethra" },
                    new() { "Duskwalker", "Umbroth", "Nighthollow", "Vel'Zar", "Shadowreign" }),

                ["Half orc"] = (
                    new() { "Grath", "Urza", "Brogan", "Varka", "Dorn" },
                    new() { "Rockmaw", "Fangthorn", "Bloodgrin", "Ironjaw", "Skullsplitter" }),

                ["Gnome"] = (
                    new() { "Fizz", "Tilda", "Nim", "Jorbin", "Penny" },
                    new() { "Copperpot", "Whistlewick", "Tinkerbrow", "Nackle", "Sprock" }),

                ["Dwarf"] = (
                    new() { "Thrain", "Brynna", "Kord", "Durin", "Helga" },
                    new() { "Stoneforge", "Ironfist", "Boulderhelm", "Flintbeard", "Deepdelver" })
            };

            if (nameData.TryGetValue(race, out var namePool))
            {
                var first = namePool.FirstNames[rnd.Next(namePool.FirstNames.Count)];
                var last = namePool.LastNames[rnd.Next(namePool.LastNames.Count)];
                return $"{first} {last}";
            }

            return "Nameless Wanderer";
        }
    }

    
}
