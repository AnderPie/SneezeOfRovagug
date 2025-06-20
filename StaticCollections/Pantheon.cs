using DnDGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.StaticCollections

{
    public static class Pantheon
    {
        public static List<Diety> Dieties = new()
        {
            // By default, God level is 30, pantheon head is 40
            new(new(){ Race.ElderEvil}, new(){ Domain.Destruction, Domain.Beasts, Domain.Death }, Profession.Monster, Alignment.ChaoticEvil,new("https://2e.aonprd.com/Monsters.aspx?ID=2606"), "Rovagug" ),
            new(new(){ Race.God , Race.Human}, new(){Domain.War, Domain.Strength, Domain.Heroism, Domain.Good }, Profession.Fighter, Alignment.ChaoticGood, null, "Thor") // Lawful?

            //"https://pathfinderwiki.com/wiki/Nethys"
            /*
            new(new(){Race.God , Race.Human}, new(){  }, "Ranger", 30, "Chaotic good", "Freya"),
            new(new(){Race.God , Race.Human}, "Cleric", 40, "Lawful good", "Odin"),
            new(new(){Race.God , Race.Human}, "Rogue", 30, "Chaotic evil", "Loki")
            */
        };
    }
}
