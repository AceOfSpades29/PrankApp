using System.Collections.Generic;

namespace PrankApp.Models
{
    public class DashboardViewModel
    {
        public string Id { get; set; }
        public List<Device> Devices { get; set; }
        public List<Prank> Pranks { get; set; }
    }
}
