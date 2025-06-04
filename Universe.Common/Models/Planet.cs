using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Universe.Common.Models
{
    public class Planet
    {
        // Attributes for the Planet class
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Satelite { get; set; } 
        public  double Gravity { get; set; } // in m/s^2


        public IEnumerable<Star>? Stars { get; set; }  
       
    }
}
