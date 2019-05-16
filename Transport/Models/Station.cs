using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Models
{
    public class Station
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Autobus> Buses { get; set; }
        public virtual ICollection<Trolleybus> Trolleys { get; set; }
        public Station()
        {
            Buses = new List<Autobus>();
            Trolleys = new List<Trolleybus>();
        }
    }
}
