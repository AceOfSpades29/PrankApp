﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PrankApp.Models;

namespace PrankApp.Controllers
{
    public class ControlStationController : Controller
    {
        private PrankAppContext _context;
        private PranksController _pranksController;
        public ControlStationController(PrankAppContext context, PranksController pranksController)
        {
            _context = context;
            _pranksController = pranksController;
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
            model.Pranks = _context.Prank.ToList();
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

        public IActionResult Edit(string prankId)
        {
            var prank = _context.Prank.FirstOrDefault(p => p.Id == prankId);
            if (prank != null)
            {
                return View(prank);
            }

            var model = GetModel();
            return View("Dashboard", model);
        }

        public async Task<IActionResult> StartPrank(string prankId)
        {
            var prank = _context.Prank.FirstOrDefault(p => p.Id == prankId);
            if (prank != null)
            {
                await _pranksController.PutPrank(prankId, prank);
            }

            return View("Dashboard", GetModel());
        }
    }
}