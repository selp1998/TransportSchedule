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
        public virtual ICollection<Trolleybus> Trolleys { get; set; }

        public virtual ICollection<AutobusStation> AutobusStation { get; set; }
        public Station()
        {
            Trolleys = new List<Trolleybus>();
            AutobusStation = new List<AutobusStation>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
