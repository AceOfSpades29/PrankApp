using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrankApp.Models
{
    public class Prank
    {
        public string Name { get; set; }

        public string Id { get; set; }


        public List<Device> DeviceList { get; set; }


        public int TurnIndex { get; set; }

    }
}
