using CVAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly ConvandbContext _context;

        public UserRolesController(ConvandbContext context)
        {
            _context = context;
        }

        [HttpPost("GetAvailableUserRoles")]
        public async Task<IActionResult> GetAvailableUserRoles()
        {
            try
            {
                var data = await _context.UserRoles.Where(x => x.IsDelete == 0).ToListAsync();

                return Ok(data);
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }
    }
}
