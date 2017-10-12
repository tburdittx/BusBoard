using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Deserializers;

namespace BusBoard.ConsoleApp
{
    class Stopcodes
    {
        //[DeserializeAs(Name = "id")]
        public string id { get; set; }

        public Stopcodes()
        {

        }
    }
}
