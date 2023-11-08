using CVAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerVanStatusesController : ControllerBase
    {
        private readonly ConvandbContext _context;

        public ContainerVanStatusesController(ConvandbContext context)
        {
            _context = context;
        }

        [HttpPost("GetConvanStatus")]
        public async Task<IActionResult> GetConvanStatus()
        {
            try
            {
                var data = await _context.CvStatuses.Where(x => x.Isdelete == 0 && x.Isforempty == 1).Select(x => new
                {
                    x.Id,
                    x.Statusname
                }).ToListAsync();

                return Ok(data);
            }catch(Exception e)
            {
                return BadRequest(new {ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("GetConvanColor")]
        public async Task<IActionResult> GetConvanColor()
        {
            try
            {
                var data = await _context.CvColorCodings.Select(x => new
                {
                    x.Id,
                    x.ColorName
                }).ToListAsync();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }

        }
    }
}
