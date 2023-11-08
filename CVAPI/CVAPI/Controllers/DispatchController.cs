using CVAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        private readonly ConvandbContext _context;

        public DispatchController(ConvandbContext context)
        {
            _context = context;
        }

        [HttpPost("SaveDispatch")]
        public async Task<IActionResult> SaveDispatch(string dispatchData)
        {
            try
            {
                var finalv = JsonConvert.DeserializeObject<DispatchData>(dispatchData)!;

                var data = await _context.VanInspectionReports.Where(x => x.Containervanid == finalv.convanid).FirstOrDefaultAsync();

                data.Finaltruckersname = finalv.truckersname;
                data.Finaldriver = finalv.driver;
                data.Finalplatenumber = finalv.plate;
                data.Customer = finalv.customer;
                data.Location = finalv.destination;
                data.Finaldate = finalv.finaldatetime;
                data.Finalremarks = finalv.remarks;


                var convan = await _context.ContainerVans.Where(x => x.Id == finalv.convanid).FirstOrDefaultAsync();
                convan.Isdispatch = 1;

               /* foreach (var dispatch in newDispatch.convanidlist)
                {
                    var convan = await _context.ContainerVans.Where(x => x.Id == dispatch).FirstOrDefaultAsync();
                    convan.Isdispatch = 1;
                    
                    await _context.AddAsync(new Models.Dispatch
                    {
                        TruckersName = newDispatch.truckersname,
                        Plate = newDispatch.plate,
                        Driver = newDispatch.driver,
                        EmptyLoad = newDispatch.emptyload,
                        Destination = newDispatch.destination,
                        Remarks = newDispatch.remarks,
                        ContainerVanId = dispatch,
                        CreatedByUserId = newDispatch.userid,
                    });


                }*/

                await _context.SaveChangesAsync();

                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("GetFilteredConvan")]
        public async Task<IActionResult> GetFilteredConvan([FromForm]int? shipid, [FromForm]int? cvsize)
        {
            try
            {
                if(shipid == null && cvsize == null)
                {
                    var data = await _context.VanInspectionReports.Where(x => x.Hasinitial == 1 && x.Hasfinal == 1 && x.Containervan.Isdispatch == 0 && (x.Colorcodingid != 3 || x.Colorcodingid != 4)).Select(x => new
                    {
                        cvId = x.Containervan.Id,
                        cvNumber = x.Containervan.Vannumber,
                        cvProcuredFrom = x.Containervan.Procuredemptycvan,
                        cvDateArrive = x.Containervan.Datearrive,
                        cvColor = x.Colorcoding.ColorName,
                        cvSize = x.Size
                    }).ToListAsync();


                    return Ok(data);
                }
                else if(shipid != null && cvsize == null)
                {
                    var data = await _context.VanInspectionReports.Where(x => x.Hasinitial == 1 && x.Hasfinal == 0 && x.Containervan.Shippinglineid == shipid).Select(x => new
                    {
                        cvId = x.Containervan.Id,
                        cvNumber = x.Containervan.Vannumber,
                        cvProcuredFrom = x.Containervan.Procuredemptycvan,
                        cvDateArrive = x.Containervan.Datearrive,
                        cvColor = x.Colorcoding.ColorName,
                        cvSize = x.Size
                    }).ToListAsync();


                    return Ok(data);
                }
                else if (shipid == null && cvsize != null)
                {
                    var data = await _context.VanInspectionReports.Where(x => x.Hasinitial == 1 && x.Hasfinal == 0 && x.Size == cvsize).Select(x => new
                    {
                        cvId = x.Containervan.Id,
                        cvNumber = x.Containervan.Vannumber,
                        cvProcuredFrom = x.Containervan.Procuredemptycvan,
                        cvDateArrive = x.Containervan.Datearrive,
                        cvColor = x.Colorcoding.ColorName,
                        cvSize = x.Size
                    }).ToListAsync();


                    return Ok(data);
                }
                else if(shipid != null && cvsize != null)
                {
                    var data = await _context.VanInspectionReports.Where(x => x.Hasinitial == 1 && x.Hasfinal == 0 && x.Containervan.Shippinglineid == shipid && x.Size == cvsize).Select(x => new
                    {
                        cvId = x.Containervan.Id,
                        cvNumber = x.Containervan.Vannumber,
                        cvProcuredFrom = x.Containervan.Procuredemptycvan,
                        cvDateArrive = x.Containervan.Datearrive,
                        cvColor = x.Colorcoding.ColorName,
                        cvSize = x.Size
                    }).ToListAsync();


                    return Ok(data);
                }


                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("GetLoadedContainer")]
        public async Task<IActionResult> GetLoadedContainer()
        {
            try
            {
                var data = await _context.ContainerVans.Where(x => x.Cvstatusid == 3 || x.Cvstatusid == 1).Select(x => new
                {
                    cvId = x.Id,
                    cvNumber = x.Vannumber,
                    cvProcuredFrom = x.Procuredemptycvan,
                    cvDateArrive = x.Datearrive,
                    cvSize = x.Size
                }).ToListAsync();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }
    }

    public class DispatchData
    {
        public string? truckersname { get; set; }
        public string? plate { get; set; }
        public string? driver { get; set; }
        public string? customer { get; set; }
        public string? destination { get; set; }
        public DateTime? finaldatetime { get; set; }
        public string? remarks { get; set; }
        public int? userid { get; set; }
        public int? convanid{ get; set; }
    }
}
