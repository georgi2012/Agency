using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.UnitTests
{
    public static class DataRestrictions
    {
        static DataRestrictions()
        {
            string json;
            using (StreamReader r = new StreamReader("..\\..\\..\\..\\Agency.Api\\appsettings.json"))
            {
                json = r.ReadToEnd();
            }
            var res = (JObject)JsonConvert.DeserializeObject(json);

            MinAirplaneCapacity = int.Parse(res.Value<string>("MinAirplaneCapacity"));
            MaxAirplaneCapacity = int.Parse(res.Value<string>("MaxAirplaneCapacity"));
            MinPricePerKm = decimal.Parse(res.Value<string>("MinPricePerKm"));
            MaxPricePerKm = decimal.Parse(res.Value<string>("MaxPricePerKm"));
            MinBoatCapacity = int.Parse(res.Value<string>("MinBoatCapacity"));
            MaxBoatCapacity = int.Parse(res.Value<string>("MaxBoatCapacity"));
            MinBusCapacity = int.Parse(res.Value<string>("MinBusCapacity"));
            MaxBusCapacity = int.Parse(res.Value<string>("MaxBusCapacity"));
            MinTrainCarts = int.Parse(res.Value<string>("MinTrainCarts"));
            MaxTrainCarts = int.Parse(res.Value<string>("MaxTrainCarts"));
            MinTrainCapacity = int.Parse(res.Value<string>("MinTrainCapacity"));
            MaxTrainCapacity = int.Parse(res.Value<string>("MaxTrainCapacity"));
        }
        public static decimal MinPricePerKm { get; private set; }
        public static decimal MaxPricePerKm { get; private set; }
        public static int MinAirplaneCapacity{get; private set;}
        public static int MaxAirplaneCapacity{get; private set;}
        public static int MinBoatCapacity { get; private set; }
        public static int MaxBoatCapacity { get; private set; }
        public static int MinBusCapacity { get; private set; }
        public static int MaxBusCapacity { get; private set; } 
        public static int MinTrainCarts { get; private set; }
        public static int MaxTrainCarts { get; private set; }
        public static int MinTrainCapacity { get; private set; }
        public static int MaxTrainCapacity { get; private set; }


    }
}
