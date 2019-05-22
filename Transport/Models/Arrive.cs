using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Models
{
    public class Arrive
    {
        public int Id { get; set; }
        public int? AutobusStationId { get; set; }
        public virtual AutobusStation AutobusStation { get; set; }
        public string Time { get; set; }
    }
}
