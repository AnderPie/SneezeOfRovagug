using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace DnDGenerator.Models
{
    public class Monster
    {
        public string Name { get; set; }

        public Uri? Uri { get; set; }
        public List<string> Keywords { get; set; } // To be replaced with more nuanced enums

        public List<EnvironmentTag> EnvironmentTags { get; set; }
        public PredationTag PredationTag { get; set; }
        public List<CreatureFamilyTag> CreatureFamilyTags { get; set; }
        public List<CreatureTypeTag> CreatureTypeTags { get; set; }
        public Frequency Frequency { get; set; }

        public int Level { get; set; }

        public Monster()
        {
            
        }

        public Monster(string name, List<CreatureTypeTag> creatureType, 
            List<CreatureFamilyTag> creatureFamily, List<EnvironmentTag> environmentTags, 
            PredationTag predation, Frequency frequency, Uri? uri, int level)
        {
            CreatureTypeTags = creatureType;
            CreatureFamilyTags = creatureFamily;
            EnvironmentTags = environmentTags;
            PredationTag = predation;
            Name = name;
            Frequency = frequency;
            Uri = uri;
            Level = level;
            Keywords = new();
        }

        public Monster(string name, List<string> keywords, Frequency frequency, 
            Uri uri, int level)
        {
            this.Name = name;
            this.Keywords = keywords;
            this.Frequency = frequency;
            this.Uri = uri;
            Level = level;
            EnvironmentTags = new();
            PredationTag Tag = new();
            CreatureFamilyTags = new();
            CreatureTypeTags = new();
        }
    }
}
