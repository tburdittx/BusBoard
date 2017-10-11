using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace BusBoard.ConsoleApp
{
    class Bus
    {

        [DeserializeAs(Name = "lineId")]
        public string LineID { get; set; }
        [DeserializeAs(Name = "timetoStation")]
        public string TimeToStation { get; set; }
        [DeserializeAs(Name = "destinationName")]
        public string DestinationName { get; set; }

        public Bus(string data)
        {
            
        }

    }
}
