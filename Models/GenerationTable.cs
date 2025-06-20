using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnDGenerator.Models
{
    public class GenerationTable
    {
        // Not always a string
        public List<string> Results { get; set; }

        public GenerationTable(List<string> results)
        {
            Results = results;
        }

        public List<string> ReturnResults(int numberToReturn = 1)
        {
            int iterations = numberToReturn;
            Random random = new();
            
            List<string> results = new(); 
       
            while (iterations > 0)
            {
                results.Add(Results[random.Next(0, Results.Count)]);
                iterations--;
            }
            return (results);
        }
    }
}
