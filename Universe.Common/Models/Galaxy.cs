namespace Universe.Common.Models
{
    public class Galaxy
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        //NAv props
     
        public IEnumerable<Star>? Stars { get; set; } // List of stars in the galaxy
    }
}