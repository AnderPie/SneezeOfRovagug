using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    public class Diety : Person
    {
        public List<Domain> Domains { get; set; }
        public List<Race> Races{ get; set; } // Predominant races among worshippers, as well as own status as a God or ElderEvil
        public Profession Profession { get; set; }

        public Uri Uri { get; set; }
        
        string Name { get; set; }

        public Diety(List<Race> races, List<Domain> domains, Profession profession, Alignment? alignment, Uri uri, string name)
        {
            Races = races;
            Domains = domains;
            Profession = profession;
            Alignment = alignment;
            Uri = uri;
            Name = name;
        }

    }
}
