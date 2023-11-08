using CVAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.IO.Pipelines;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingLinesController : ControllerBase
    {

        private readonly ConvandbContext _context;

        public ShippingLinesController(ConvandbContext context)
        {
            _context = context;
        }

        [HttpPost("GetShippingLines")]
        public async Task<IActionResult> GetShippingLines()
        {
            try
            {
                var data = await _context.ShippingLines.OrderByDescending(x => x.IsActive).ThenBy(x => x.Id).ToListAsync();

                return Ok(data);
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("AddShipingLine")]
        public async Task<IActionResult> GetShippingLine(string ship)
        {
            try
            {
                ShippingLine newSL = JsonConvert.DeserializeObject<ShippingLine>(ship)!;

                var duplicate = await _context.ShippingLines.Where(x => x.Name == newSL.Name).FirstOrDefaultAsync();

                if(duplicate != null)
                {
                    return BadRequest(new { Message = "Duplicate Shipping Line." });
                }

                await _context.AddAsync(newSL);
                await _context.SaveChangesAsync();

                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("EditShippingLine")]
        public async Task<IActionResult> EditShippingLine(string ship)
        {
            try
            {
                ShippingLine newSL = JsonConvert.DeserializeObject<ShippingLine>(ship)!;

                var duplicate = await _context.ShippingLines.Where(x => x.Id != newSL.Id).ToListAsync();

                foreach(var d in duplicate)
                {
                    if (d.Name == newSL.Name)
                    {
                        return BadRequest(new { Message = "Shipping Line Name already exist" });
                    }
                }


                var data = await _context.ShippingLines.Where(x => x.Id == newSL.Id).FirstOrDefaultAsync();
                data.Name = newSL.Name;
                data.Address = newSL.Address;
                data.ModifiedDate = DateTime.Now;

                await _context.SaveChangesAsync();

                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new {ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("ChangeStatusShippingLine")]
        public async Task<IActionResult> ChangeStatusShippingLine(int slid)
        {
            try
            {
                var data = await _context.ShippingLines.Where(x => x.Id == slid).FirstOrDefaultAsync();
                if(data.IsActive == 1)
                {
                    data.IsActive = 0;

                }
                else
                {
                    data.IsActive = 1;
                }
                await _context.SaveChangesAsync();

                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMesage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("GetAvailableShippingLine")]
        public async Task<IActionResult> GetAvailableShippingLine(string? search)
        {
            try
            {
                if(search == null)
                {
                    var searchData = _context.ShippingLines.Where(x => x.IsActive == 1).ToList();

                    return Ok(searchData);
                }
                else
                {
                    var searchData = _context.ShippingLines.Where(x => x.IsActive == 1 && x.Name.ToLower().Contains(search.ToLower())).ToList();

                    return Ok(searchData);
                }
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("GetShippinglineData")]
        public async Task<IActionResult> GetShippinglineData(int shippinglineId)
        {
            try
            {
                var data = await _context.ShippingLines.Where(x => x.Id == shippinglineId).FirstOrDefaultAsync();

                return Ok(data);
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }
    }
}
