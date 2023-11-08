using CVAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ConvandbContext _context;

        public BookingsController(ConvandbContext context)
        {
            _context = context;
        }

       /* [HttpPost("SaveBooking")]
        public async Task<IActionResult> SaveBooking([FromForm]String booking)
        {
            try
            {
                var toBook = JsonConvert.DeserializeObject<Booking>(booking)!;
                await _context.AddAsync(toBook);
                await _context.SaveChangesAsync();
                return Ok(new {status = "SUCCESS", message = "Booking has been sent to the pier coordinator."});
            }catch(Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException });
            }
        }

        [HttpPost("FetchBooking")]
        public async Task<IActionResult> FetchBooking([FromForm] int branchId)
        {
            try
            {
                var data = await _context.Bookings.Where(x => x.IsDeleted == 0 && x.IsApproved == 0 && x.BranchId == branchId).ToListAsync();

                return Ok(new { Status = "SUCCESS", data = data });
            }catch(Exception e)
            {
                return BadRequest(new {errorMessage = e.Message, innerException = e.InnerException });
            }
        }

        [HttpPost("GetBookingData")]
        public async Task<IActionResult> GetBookingData([FromForm]int bookingId)
        {
            try
            {
                var data = await _context.Bookings.Where(x => x.Id == bookingId).FirstOrDefaultAsync();

                if(data == null)
                {
                    return BadRequest(new { Status = "FAILED", errorMessage = "No data found" });
                }

                return Ok(new { Status = "SUCCESS", data = data });
            }catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException });
            }
        }

        [HttpPost("BookingApproval")]
        public async Task<IActionResult> BookingApproval([FromForm]int isApprove, [FromForm]int bookingId)
        {
            try
            {
                var data = await _context.Bookings.Where(x => x.Id == bookingId).FirstOrDefaultAsync();
                if(data == null)
                {
                    return BadRequest(new { errorMessage = "No such data in the booking."});
                }

                if (isApprove == 1)
                {
                    data.IsApproved = (short)isApprove;
                    await _context.SaveChangesAsync();
                    return Ok(new { status = "SUCCESS", message = "Booking has been Approved by the pier coordinator." });
                }
                else
                {
                    data.IsApproved = (short)isApprove;
                    await _context.SaveChangesAsync();
                    return Ok(new { status = "SUCCESS", message = "Booking has been Disapproved by the pier coordinator." });
                }
            }
            catch(Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException });
            }
        }

        [HttpPost("GetApproveBooking")]
        public async Task<IActionResult> GetApproveBooking([FromForm]int branchId)
        {
            try
            {
                var data = await _context.Bookings.Where(x => x.BranchId == branchId && x.IsApproved == 1 && x.Status == 0).ToListAsync();
                return Ok(data);
            }catch(Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException });
            }
        }

        [HttpPost("GetBookingDocuments")]
        public async Task<IActionResult> GetBookingDocuments()
        {
            try
            {
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new {ErrorMessage = e.Message, innerException = e.InnerException });
            }
        }*/
    }
}
