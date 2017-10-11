using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter a stop code.");
            var userInput = Console.ReadLine();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Location location = new Location();
            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.tfl.gov.uk/");
            var request = new RestRequest("resource?auth_token={userInput}", Method.GET);
            request.AddParameter("userInput", userInput, ParameterType.UrlSegment);
            request.Resource = "StopPoint/{userInput}/Arrivals?app_id=451e3d76&app_key=e34ecaee185f5709d397ad5533afeb4f";
            IRestResponse response = client.Execute(request);




            //foreach (var result in queryResult.Content)
            //{
            //    var data = JsonConvert.DeserializeObject<Data>(result.ToString());
            //    Console.WriteLine(data.CommonName);
            //
            List<Bus> buses = JsonConvert.DeserializeObject<List<Bus>>(response.Content);
            List<Bus> SortedList = buses.OrderBy(b => Convert.ToInt32(b.TimeToStation)).ToList();
            List<Bus> firstFive = SortedList.GetRange(0, 5);
            

            foreach (var bus in firstFive)
            {
                int timeToStation = Convert.ToInt32(bus.TimeToStation) / 60;
                
                Console.WriteLine(string.Format("Line ID: {0}, Time to Station: {1} minutes, Destination: {2}",bus.LineID, timeToStation, bus.DestinationName));
            }

           



            Console.ReadLine();

        }
    }
}
