using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace BusBoard.ConsoleApp
{
    class Postcodes
    {
        [DeserializeAs(Name = "postcode")]
        public string Postcode { get; set; }
        [DeserializeAs(Name = "longitude")]
        public string Longitude { get; set; }
        [DeserializeAs(Name = "latitude")]
        public string Latitude { get; set; }

        public Postcodes()
        {
            
        }
    }
}
