using CVAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VanInpectionsController : ControllerBase
    {
        private readonly ConvandbContext _context;

        public VanInpectionsController(ConvandbContext context)
        {
            _context = context;
        }

        /*[HttpPost("CheckBookingVanInspection")]
        public async Task<IActionResult> CheckBookingVansInspection([FromForm]int bookingId)
        {
            try
            {
                var data = await _context.VanInspections.Where(x => x.BookingId == bookingId).FirstOrDefaultAsync();
                if(data == null)
                {
                    return Ok();
                }
                return Ok(data);
            }catch(Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException.Message });
            }
        }*/

        [HttpPost("GetVanChecklist")]
        public async Task<IActionResult> GetVanChecklist()
        {
            try
            {
                bool? IsOkayClicked = null;
                int? condition = null;
                string? remarks = null;
                var data = await _context.CvChecklists.Select(x => new
                {
                    x.Description,
                    x.Types,
                    condition,
                    remarks,
                    IsOkayClicked,
                }).ToListAsync();

                return Ok(new {check = data});
            }catch(Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException });
            }
        }

        /*[HttpPost("SaveVanInpectionReport")]
        public async Task<IActionResult> SaveVanInspectionReport([FromForm] String vanReport)
        {
            try
            {
                var van = JsonConvert.DeserializeObject<VanInspectionReport>(vanReport);
                van.Hasinitial = 1;
                await _context.AddAsync(van);
                await _context.SaveChangesAsync();

                var data = _context.VanInspectionReports.OrderByDescending(x => x.Id).FirstOrDefault();

                return Ok(new { Status = "SUCCESS", vanId = data.Id });
            }
            catch (Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException.Message });
            }
        }*/

        [HttpPost("CheckConvanChecklist")]
        public async Task<IActionResult> CheckConvanChecklist([FromForm] String checkList)
        {
            try
            {
                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerError = e.InnerException.Message });
            }
        }

        [HttpPost("SaveChecklist")]
        public async Task<IActionResult> SaveChecklist([FromForm]String checklist, [FromForm] int cvanId, [FromForm]int isInital)
        {
            try
            {
                List<VanInspectionChecklist> list = JsonConvert.DeserializeObject<List<VanInspectionChecklist>>(checklist)!;

                foreach (var van in list)
                {
                    van.Virid = cvanId;
                    van.IsInital = (short)isInital;
                    await _context.AddAsync(van);
                } 
                await _context.SaveChangesAsync();

                return Ok(new { Status = "SUCCESS" });
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerError = e.InnerException!.Message });
            }
        }

        [HttpPost("GetInspectionData")]
        public async Task<IActionResult> GetInspectionData(int inspectionId)
        {
            try
            {
                var data = await _context.VanInspectionReports.Where(x => x.Id == inspectionId).Select(x => new VanInspectionDataDTO
                {
                    Id = x.Id,
                    Truckersname = x.Truckersname,
                    Driver = x.Driver,
                    Platenumber = x.Platenumber,
                    Colorcoding = x.Colorcoding.ColorName,
                    Shippingline = x.Containervan.Shippingline.Name,
                    Vannumber = x.Containervan.Vannumber,
                    Status = x.Status.Statusname,
                    Type = x.Type,
                    hasinitial = (int)x.Hasinitial!,
                    hasfinal = (int)x.Hasfinal,
                    eirnumber = x.Containervan.Eirnumber,
                    size = (int)x.Size
                }).FirstOrDefaultAsync();


                return Ok(data);
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("SaveForFinalInspection")]
        public async Task<IActionResult> SaveForFinalInspection(int virId, string newVirValue, string finalchecklist)
        {
            try
            {
                VanInspectionReport vir = JsonConvert.DeserializeObject<VanInspectionReport>(newVirValue)!;
                VanInspectionChecklist finalChecklist = JsonConvert.DeserializeObject<VanInspectionChecklist>(finalchecklist)!;

                var data = await _context.VanInspectionReports.Where(x => x.Id == virId).FirstOrDefaultAsync();

                data.Finaltruckersname = vir.Finaltruckersname;
                data.Finalplatenumber = vir.Finalplatenumber;
                data.Finaldriver = vir.Finaldriver;
                data.Finaldate = vir.Finaldate;
                data.Shipper = vir.Shipper;
                data.Customer = vir.Customer;
                data.Location = vir.Location;
                data.Finalremarks = vir.Finalremarks;
                data.Hasfinal = 1;

                //data.Confirmeddate = DateTime.Now();

                finalChecklist.IsInital = (short)2;
                await _context.AddAsync(finalChecklist);
                await _context.SaveChangesAsync();

                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

     /*   [HttpPost("SaveConvanColorCoding")]
        public async Task<IActionResult> SaveConvanColorCoding([FromForm] int vanInspectionId, [FromForm]string colorCoding)
        {
            try
            {

                var data = _context.VanInspections.Where(x => x.Id == vanInspectionId).FirstOrDefault();

                data.ColorCoding = colorCoding;

                if (data != null)
                {
                    var booking = _context.Bookings.Where(x => x.Id == data.BookingId).FirstOrDefault();

                    booking.Status = 1;
                }

                await _context.SaveChangesAsync();

                return Ok(new { STATUS = "SUCCESS" });
            }
            catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }*/

        /* [HttpPost("GetUnloadingConVan")]
         public async Task<IActionResult> GetUnloadingConVan()
         {
             try
             {
                 var data = await _context.VanInspections.Where(x => x.IsInitial == 1 && x.Booking.Status == 1).ToListAsync();
                 List<VanInspectionChecklist> vanList = new List<VanInspectionChecklist>();
                 foreach(var i in data)
                 {
                     vanList = await _context.VanInspectionChecklists.Where(x => x.VanInspectionId == i.Id).ToListAsync();
                 }

                 return Ok(data);
             }catch(Exception e)
             {
                 return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
             }
         }*/
    }

    public class VanInspectionDataDTO
    {
        public int Id { get; set; }
        public string Truckersname { get; set; }
        public string Driver { get; set; }
        public string Platenumber { get; set; }
        public string Colorcoding { get; set; }
        public string Shippingline { get; set; }
        public string Vannumber { get; set; }
        public string eirnumber { get; set; }
        public int size { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public int hasinitial { get; set; }
        public int hasfinal { get; set; }

    }
}
