using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Autobus> Autobuses { get; set; } 
        public Route()
        {
            Autobuses = new List<Autobus>();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
