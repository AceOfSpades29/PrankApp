using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrankApp.Models;

namespace PrankApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PranksController : ControllerBase
    {
        private readonly PrankAppContext _context;

        public PranksController(PrankAppContext context)
        {
            _context = context;
        }

        // GET: api/Pranks
        [HttpGet]
        public IEnumerable<Prank> GetPranks()
        {
            return _context.Prank;
        }

        // GET: api/Pranks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrank([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prank = await _context.Prank.FindAsync(id);

            if (prank == null)
            {
                return NotFound();
            }

            return Ok(prank);
        }

        // PUT: api/Pranks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrank([FromRoute] string id, [FromBody] Prank prank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prank.Id)
            {
                return BadRequest();
            }

            _context.Entry(prank).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrankExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Pranks
        [HttpPost]
        public async Task<IActionResult> PostPrank([FromBody] Prank prank)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Prank.Add(prank);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrank", new { id = prank.Id }, prank);
        }

        [HttpGet]
        [Route("CheckMyTurn/{id}")]
        public async Task<JsonResult> CheckMyTurn([FromRoute] string id)
        {
            //check pranks for this phone id
            Random rnd = new Random();
            int number = rnd.Next(0, 10);
            if (number > 5)
            {
                return new JsonResult("true");
            }
            return new JsonResult("false");
        }

        [HttpGet]
        [Route("ReallyCheckMyTurn/{id}")]
        public async Task<JsonResult> ReallyCheckMyTurn([FromRoute] string id)
        {
            var pranks = GetPranks();
            foreach (var prank in pranks)
            {
                for (var i = 0; i < prank.DeviceList.Count; i++)
                {
                    var device = prank.DeviceList[i];
                    if (device.Id == id && i == prank.TurnIndex)
                    {
                        return new JsonResult("true");
                    }
                }
            }
            return new JsonResult("false");
        }

        [HttpPost]
        [Route("MyTurnIsOver")]
        public async Task MyTurnIsOver(string id)
        {
            var pranks = GetPranks();
            foreach (var prank in pranks)
            {
                foreach (var device in prank.DeviceList)
                {
                    if (device.Id == id)
                    {
                        prank.TurnIndex++;
                        try
                        {
                            _context.Entry(prank).State = EntityState.Modified;
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }
        }

        // DELETE: api/Pranks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrank([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prank = await _context.Prank.FindAsync(id);
            if (prank == null)
            {
                return NotFound();
            }

            _context.Prank.Remove(prank);
            await _context.SaveChangesAsync();

            return Ok(prank);
        }

        private bool PrankExists(string id)
        {
            return _context.Prank.Any(e => e.Id == id);
        }
    }
}