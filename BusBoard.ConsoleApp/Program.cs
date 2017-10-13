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
            var userInput = "490008660N";

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Console.WriteLine("Please enter postcode");
            var postcode = Console.ReadLine();

            var client2 = new RestClient();
            client2.BaseUrl = new Uri("https://api.postcodes.io");
            var request2 = new RestRequest("resource?auth_token={postcode}", Method.GET);
            request2.AddParameter("postcode", postcode, ParameterType.UrlSegment);
            request2.Resource = "/postcodes/{postcode}";
            IRestResponse response2 = client2.Execute(request2);

            Wrapper wrapper = JsonConvert.DeserializeObject<Wrapper>(response2.Content);

            var client3 = new RestClient();
            client3.BaseUrl = new Uri("https://api.tfl.gov.uk/");
            var request3 = new RestRequest("resource?auth_token={userInput}", Method.GET);
            var userInputLon = wrapper.Result.Longitude;
            var userInputLat = wrapper.Result.Latitude;
            request3.AddParameter("userInputLon", userInputLon, ParameterType.UrlSegment);
            request3.AddParameter("userInputLat", userInputLat, ParameterType.UrlSegment);
            request3.Resource = "StopPoint?stopTypes=NaptanPublicBusCoachTram&radius=500&useStopPointHierarchy=true&modes=bus&returnLines=true&lat={userInputLat}&lon={userInputLon}&app_id=451e3d76&app_key=e34ecaee185f5709d397ad5533afeb4f";
            IRestResponse response3 = client3.Execute(request3);
            StopPointWrapper stopPointWrapper = JsonConvert.DeserializeObject<StopPointWrapper>(response3.Content);
            List<Stopcodes> lst = stopPointWrapper.StopPoints.GetRange(0, 2);

            foreach (var stoppoint in lst)
            {
                userInput = stoppoint.id;
                var client = new RestClient();
                client.BaseUrl = new Uri("https://api.tfl.gov.uk/");
                var request = new RestRequest("resource?auth_token={userInput}", Method.GET);
                request.AddParameter("userInput", userInput, ParameterType.UrlSegment);
                request.Resource = "StopPoint/{userInput}/Arrivals?app_id=451e3d76&app_key=e34ecaee185f5709d397ad5533afeb4f";
                IRestResponse response = client.Execute(request);

                List<Bus> buses = JsonConvert.DeserializeObject<List<Bus>>(response.Content);
                List<Bus> SortedList = buses.OrderBy(b => Convert.ToInt32(b.TimeToStation)).ToList();
                //List<Bus> firstFive = SortedList.GetRange(0, 5);
                Console.WriteLine(stoppoint.id);

                foreach (var bus in SortedList)
                {
                    int timeToStation = Convert.ToInt32(bus.TimeToStation) / 60;

                    Console.WriteLine(string.Format("Line ID: {0}, Time to Station: {1} minutes, Destination: {2}", bus.LineID, timeToStation, bus.DestinationName));
                }
            }
            Console.ReadLine();
        }
    }
}
