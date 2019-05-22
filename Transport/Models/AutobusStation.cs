using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Models
{
    public class AutobusStation
    {
        public int Id { get; set; }
        public int? AutobusId { get; set; }
        public virtual Autobus Autobus { get; set; }

        public int? StationId { get; set; }
        public virtual Station Station { get; set; }
    }
}
