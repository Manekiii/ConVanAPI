using CVAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly ConvandbContext _context;

        public BranchesController(ConvandbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBranch()
        {
            return Ok(await _context.Branches.ToListAsync());
        }

        [HttpPost("GetAvailableBranch")]
        public async Task<IActionResult> GetAvailableBranch()
        {
            try
            {
                var data = await _context.Branches.Where(x => x.Status == 1 && x.IsDeleted == 0).ToListAsync();

                if(data.Count == 0)
                {
                    return Ok(new { Message = "No Branches Found", branches = data});
                }
                else
                {
                    return Ok(data);
                }
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }
    }
}
