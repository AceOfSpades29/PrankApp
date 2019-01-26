using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PrankApp.Models;

namespace PrankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterDeviceController : ControllerBase
    {
        private PrankAppContext _context;

        public RegisterDeviceController(PrankAppContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult RegisterDevice([FromBody] Device device)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return new JsonResult(BadRequest(device));
                }
                _context.Device.Add(device);
                _context.SaveChanges();
                return new JsonResult($"{{result: {true}}}");
            }
            catch (Exception ex)
            {
                return new JsonResult($"{{result: {false}, message: {ex.Message}, innerException: {ex.InnerException?.Message}}}");
            }
        }

        [HttpDelete]
        [Route("remove/{id}")]
        public JsonResult RemoveDevice(string id)
        {
            try
            {
                var device = _context.Device.FirstOrDefault(d => d.Id == id);
                if (device != null)
                {
                    _context.Device.Remove(device);
                    _context.SaveChanges();
                    return new JsonResult($"{{result: {true}}}");
                }
                return new JsonResult($"{{result: {false}, message: device not found}}");
            }
            catch (Exception ex)
            {
                return new JsonResult($"{{result: {false}, message: {ex.Message}, innerException: {ex.InnerException?.Message}}}");
            }
        }
    }
}