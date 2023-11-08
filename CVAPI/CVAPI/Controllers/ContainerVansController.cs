using CVAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Newtonsoft.Json;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerVansController : ControllerBase
    {
        private readonly ConvandbContext _context;

        public ContainerVansController(ConvandbContext context)
        {
            _context = context;
        }

        [HttpPost("GetAvailableContainer")]
        public async Task<IActionResult> GetAvailableContainer()
        {
            try
            {

                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new { errorMessage = e.Message, innerError = e.InnerException });
            }
        }

        [HttpPost("SaveContainer")]
        public async Task<IActionResult> SaveContainer([FromForm]string newVan, [FromForm]string newvir, [FromForm]string checklist, [FromForm]string inspectiontype, [FromForm]int createdby)
        {
            try
            {
                ContainerVan cv = JsonConvert.DeserializeObject<ContainerVan>(newVan)!;
                VanInspectionReport vir = JsonConvert.DeserializeObject<VanInspectionReport>(newvir)!;
                List<VanInspectionChecklist> list = JsonConvert.DeserializeObject<List<VanInspectionChecklist>>(checklist)!;

                cv.Createdbyuserid = createdby;
                cv.Size = vir.Size;
                cv.Cvstatusid = vir.Statusid;
                await _context.AddAsync(cv);
                await _context.SaveChangesAsync();

                vir.Containervanid = cv.Id;
                vir.Createdbyuserid = createdby;
                vir.Initialinspectoruserid = createdby;
                vir.Initialinspectiondate = DateTime.Now;
                if (inspectiontype == "I")
                {
                    vir.Hasinitial = 1;
                    vir.Initialdate = DateTime.Now;
                }
                else
                {
                    vir.Finaltruckersname = vir.Truckersname;
                    vir.Finaldriver = vir.Driver;
                    vir.Finalplatenumber = vir.Platenumber;
                    vir.Hasfinal = 1;
                    vir.Finaldate = vir.Finaldate;
                    vir.Confirmedbyuserid = createdby;
                    vir.Confirmeddate = DateTime.Now;
                }
                await _context.AddAsync(vir);
                await _context.SaveChangesAsync();

                foreach (var van in list)
                {
                    van.Virid = vir.Id;
                    if (inspectiontype == "I")
                    {
                        van.IsInital = (short)1;
                    }
                    else
                    {
                        van.IsInital = (short)2;
                    }
                        await _context.AddAsync(van);
                }
                await _context.SaveChangesAsync();
                return Ok(new {StatusMessage = "SUCCESS"});
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("SaveFinalInspection")]
        public async Task<IActionResult> SaveFinalInspection([FromForm]int virid, [FromForm]int colorcodingid, [FromForm]string checklist, [FromForm]int createdby)
        {
            try
            {
                List<VanInspectionChecklist> finalList = JsonConvert.DeserializeObject<List<VanInspectionChecklist>>(checklist)!;

                var data = await _context.VanInspectionReports.Where(x => x.Id == virid).FirstOrDefaultAsync();

                /*data.Finaltruckersname = finalv.Finaltruckersname;
                data.Finaldriver = finalv.Finaldriver;
                data.Finalplatenumber = finalv.Finalplatenumber;
                data.Shipper = finalv.Shipper;
                data.Customer = finalv.Customer;
                data.Location = finalv.Location;
                data.Finaldate = finalv.Finaldate;*/
                data.Colorcodingid = colorcodingid;
                data.Hasfinal = 1;
                data.Confirmedbyuserid = createdby;
                data.Confirmeddate = DateTime.Now;
                data.Modifiedbyuserid = createdby;
                data.Modifieddate = DateTime.Now;

              /*  var convan = _context.ContainerVans.Where(x => x.Id == data.Containervanid).FirstOrDefault();
                convan.Isdispatch = 1;*/

                foreach(var i in finalList)
                {
                    i.IsInital = 2;
                    i.Virid = data.Id;

                    await _context.AddAsync(i);
                }

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("FetchListofCvan")]
        public async Task<IActionResult> FetchListofCvan()
        {
            try
            {
                var data = await _context.VanInspectionReports.Where(x => x.Hasinitial == 1 && x.Hasfinal == 1 && x.Containervan.Isdispatch == 0).Select(x => new
                {
                   cvid = x.Containervan.Id,
                   cvnumber = x.Containervan.Vannumber,
                   cveir = x.Containervan.Eirnumber,
                   cvteu = x.Containervan.Size,
                   cvcolorcodingid = x.Colorcodingid,
                   cvcolorcoding = x.Colorcoding.ColorName,
                   cvshippinglineid = x.Containervan.Shippinglineid,
                    cvshippinglinename = x.Containervan.Shippingline.Name,
                }).ToListAsync();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("ConvanForFinal")]
        public async Task<IActionResult> ConvanForFinal()
        {
            try
            {
                var data = await _context.VanInspectionReports.Where(x => x.Hasinitial == 1 && x.Hasfinal == 0 && x.Colorcodingid == 3).Select(x => new
                {
                    cvId = x.Containervan.Id,
                    cvNumber = x.Containervan.Vannumber,
                    cvProcuredFrom = x.Containervan.Procuredemptycvan,
                    cvDateArrive = x.Containervan.Datearrive,
                    cvColor = x.Colorcoding.ColorName
                }).ToListAsync();

                return Ok(data);
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }

        [HttpPost("SaveHustlingloaded")]
        public async Task<IActionResult> SaveHustlingLoaded(string convandata)
        {
            try
            {
                ContainerVan newData = JsonConvert.DeserializeObject<ContainerVan>(convandata)!;
                newData.Hasdetention = 1;
                newData.Cvstatusid = 3;
                await _context.AddAsync(newData);
                await _context.SaveChangesAsync();

                return Ok();
            }catch(Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }
    }
}
