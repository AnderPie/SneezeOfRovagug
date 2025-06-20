using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    // In the future this class may include:
    //   links to the map of the den
    //   number of den inhabitants
    //   treasure within / around the den
    // For now, just includes the type of monster in the den
    public class Den
    {
        public string MonsterName { get; set; }
        public Monster Monster { get; set; }
        public Encounter? Encounter { get; set; }

        public Den(Monster monster)
        {
            Monster = monster;
            Encounter = new();
            MonsterName = monster.Name;
        }
    }
}
