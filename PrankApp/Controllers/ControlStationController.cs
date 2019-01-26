using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PrankApp.Models;

namespace PrankApp.Controllers
{
    public class ControlStationController : Controller
    {
        private PrankAppContext _context;

        public ControlStationController(PrankAppContext context)
        {
            _context = context;
        }
        public IActionResult Dashboard()
        {
            var model = GetModel();

            return View(model);
        }

        private DashboardViewModel GetModel()
        {
            var model = new DashboardViewModel();

            model.Devices = _context.Device.ToList();
            return model;
        }

        public IActionResult Delete(string id)
        {
            var device = _context.Device.FirstOrDefault(d => d.Id == id);
            if (device != null)
            {
                _context.Device.Remove(device);
                _context.SaveChanges();
            }

            var model = GetModel();
            return View("Dashboard", model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Device device)
        {
            if (!ModelState.IsValid)
            {
                return View(device);
            }

            _context.Device.Add(device);
            _context.SaveChanges();
            var model = GetModel();
            return View("Dashboard", model);
        }
    }
}