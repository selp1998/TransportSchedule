using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Models
{
    public class Autobus
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int RouteId { get; set; }
        public virtual Route Route { get; set; }
    }
}
