namespace DnDGenerator.Models
{
    public class Encounter
    {
        public List<Monster>? Monsters { get; set; }
        public List<Person>? People { get; set; }
        public List<Treasure>? Treasures { get; set; }
        public string? NarrativeText { get; set; }
    }
}