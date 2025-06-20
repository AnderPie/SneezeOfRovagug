using DnDGenerator.StaticCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    public class EncounterTable
    {

        public List<Monster> PossibleMonsters { get; set; }
        public List<Monster> Monsters { get; set; }

        // We will have to use some creativity to figure out how to generate realistic encounters from monsters
        // (humanoids and attack beasts or mounts, lone predators, grouped predators, etc.)
        // In the meantime, let us rely on individual monsters and spice things up after the fact during play.
        public List<Tuple<Frequency, Encounter>> PossibleEncounters {get; set; }
        public List<Tuple<int, Encounter>> Encounters { get; set; }

        public EncounterTable()
        {
            Encounters = new();
            PossibleEncounters = new();
            PossibleMonsters = new();
        }

        public EncounterTable(List<Monster> possibleMonsters)
        {
            PossibleMonsters = possibleMonsters;
            Encounters = new();
            PossibleEncounters = new();
        }

        public static List<Monster> GenerateEncounterTable(List<Monster> allMonsters, int maxEncounters = 100)
        {
            var rand = new Random();

            // Step 1: Build a weighted list
            var weightedList = new List<Monster>();
            foreach (var monster in allMonsters)
            {
                int weight = (int)monster.Frequency;
                for (int i = 0; i < weight; i++)
                {
                    weightedList.Add(monster);
                }
            }

            // Step 2: Shuffle the weighted list and select up to maxEncounters unique monsters
            var selected = new HashSet<Monster>();
            while (selected.Count < maxEncounters && weightedList.Count > 0)
            {
                int index = rand.Next(weightedList.Count);
                var chosen = weightedList[index];

                if (!selected.Contains(chosen))
                {
                    selected.Add(chosen);
                }

                weightedList.RemoveAll(m => m == chosen); // Prevent excessive repeats
            }

            return selected.ToList();
        }



        // Randomly select a monster from the pool of available monsters
        // Hardly efficient but she gets the job done.
        public Monster GenerateMonster()
        {
            if(Monsters is null)
            {
                Monsters = GenerateEncounterTable(PossibleMonsters);
            }

            Random rand = new();
            
            return Monsters[rand.Next(0, Monsters.Count())];
        }


        public void AddPossibleEncounter(Tuple<Frequency, Encounter> possible)
        {
            PossibleEncounters.Add(possible);
        }
        public void PopulateEncounters()
        {
            // Add together the total of all possible encounters * their frequency

            // The denominator for determining an event's liklihood
            int total = 0;
            foreach(Tuple<Frequency, Encounter> PossibleEncounter in PossibleEncounters)
            {
                total += (int)PossibleEncounter.Item1;
            }

            int iterator = 0;
            // Probability is equal to frequency / total (int is equal to this * 100 + current number, to determine roll on D100
            foreach(Tuple<Frequency, Encounter> PossibleEncounter in PossibleEncounters)
            {
                iterator += (int)((int)PossibleEncounter.Item1 / total);
                Encounters.Add( new(iterator, PossibleEncounter.Item2));
            }
        }
    }
}
