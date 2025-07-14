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
        public PredationTag PredationTag { get; set; } = PredationTag.SmallPredator;
        public List<CreatureFamilyTag> CreatureFamilyTags { get; set; }
        public List<CreatureTraitTag> CreatureTraitTags { get; set; }
        public Frequency Frequency { get; set; } = Frequency.Often; // TODO: Remove Frequency system, replaced with Rarity.

        public int? Level { get; set; }

        public Rarity Rarity { get; set; } = Rarity.Common;
        public Size Size { get; set; } = Size.Medium;
        public int? HP { get; set; }
        public int? AC { get; set; }
        public int? Fortitude { get; set; }
        public int? Reflex { get; set; }
        public int? Perception { get; set; }
        public int? Will { get; set; }

        public Monster()
        {
            EnvironmentTags = new();
            CreatureFamilyTags = new();
            CreatureTraitTags = new();
        }

        public Monster(string name, List<CreatureTraitTag> creatureType, 
            List<CreatureFamilyTag> creatureFamily, List<EnvironmentTag> environmentTags, 
            PredationTag predation, Frequency frequency, Uri? uri, int level)
        {
            CreatureTraitTags = creatureType;
            CreatureFamilyTags = creatureFamily;
            EnvironmentTags = environmentTags;
            PredationTag = predation;
            Name = name;
            Frequency = frequency;
            Uri = uri;
            Level = level;
            Keywords = new();
        }

        public Monster(string name, Rarity rarity, Size size, List<CreatureTraitTag> traits, int level, int hp, int ac, int fort, int reflex, int will, int perception, Uri uri)
        {
            Name = name;
            Rarity = rarity;
            Size = size;
            CreatureTraitTags = traits;
            Level = level;
            HP = hp;
            AC = ac;
            Fortitude = fort;
            Reflex = reflex;
            Will = will;
            Perception = perception;
            Uri = uri;
            // TODO add environment tagging system (requires data cleaning)
            EnvironmentTags = new();
            EnvironmentTags.Add(EnvironmentTag.Common); 
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
            CreatureFamilyTags = new();
            CreatureTraitTags = new();
        }

        /// <summary>
        /// Resolves a predation tag based on a creature's assigned traits
        /// </summary>
        private void ResolvePredationTag()
        {
            // TODO Add logic for detecting herbivores
            if (CreatureTraitTags.Contains(CreatureTraitTag.Humanoid))
            {
                PredationTag = PredationTag.Humanoid;
            }
            else
            {
                if((int)Size > (int)Size.Medium)
                {
                    PredationTag = PredationTag.LargePredator;
                }
            }
            PredationTag = PredationTag.SmallPredator;
        }
    }
}
