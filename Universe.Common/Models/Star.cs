namespace Universe.Common.Models
{
    public class Star
    {
        // Props
        public int Id { get; set; }
        public required string Name { get; set; }
        public required bool IsDeath { get; set; }

        public IEnumerable<Planet>? Planets { get; set; } 

        public Galaxy? Galaxy { get; set; } 
        public IEnumerable<Planet>? Planets { get; set; } // List of planets in the galaxy
    }
}