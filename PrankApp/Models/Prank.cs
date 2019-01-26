using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PrankApp.Models
{
    public class Prank
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("DeviceList")]
        public List<Device> DeviceList { get; set; }

        [JsonProperty("TurnIndex")]
        public int TurnIndex { get; set; }
    }
}
