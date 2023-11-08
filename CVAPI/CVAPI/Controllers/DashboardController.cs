using CVAPI.Models;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing;

namespace CVAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ConvandbContext _context;

        public DashboardController(ConvandbContext context)
        {
            _context = context;
        }

        [HttpPost("GetContainerCountsPerSites")]
        public async Task<IActionResult> GetContainerCounts()
        {
            try
            {
                var data = _context.VanInspectionReports.Select(x => new
                {
                    shippingline = x.Containervan.Shippingline.Name,
                    convan = x.Containervan,
                    colorcoding = x.Colorcoding,
                });

                var totalDispatch = await _context.ContainerVans.Where(x => x.Isdispatch == 1).CountAsync();
                var forFinal = await _context.VanInspectionReports.Where(x => x.Hasfinal == 0 && x.Hasinitial == 1).CountAsync();
                var totalReceived = await _context.ContainerVans.CountAsync();
                var shippinglineList = await _context.ShippingLines.Select(x => x.Id).ToListAsync();

                List<ShippingLineConvan> convanPerSites = new();

                foreach (var shippingline in shippinglineList)
                {
                    var shipname = _context.ShippingLines.Where(x => x.Id == shippingline).Select(x => x.Name).FirstOrDefault();

                    var cvList = _context.ContainerVans.Where(x => x.Shippinglineid == shippingline).ToList();

                    if (cvList.Count() > 0)
                    {
                        convanPerSites.Add(new ShippingLineConvan
                        {
                            shippinglineid = shippingline,
                            shippinglinename = shipname,
                            containervanlist = cvList,
                        });
                    }
                }


                var colorList = _context.CvColorCodings.ToList();
                int colorcount;
                List<ConvanColorPerShippingline> cvcolorPershippingline = new();

                var convancolorpersites = await _context.VanInspectionReports.GroupBy(v => new { v.Containervan.Shippinglineid, v.Containervan.Shippingline.Name, v.Colorcoding.ColorName, v.Colorcodingid }).Select(g => new
                {
                    shippinglineid = g.Key.Shippinglineid,
                    shippinglinename = g.Key.Name,
                    colorid = g.Key.Colorcodingid,
                    colorname = g.Key.ColorName,
                    colorcount = g.Count()
                }).Distinct().ToListAsync();

                /* var shipColors = await _context.VanInspectionReports
                     .GroupBy(v => new { v.Containervan.Shippinglineid, v.Containervan.Shippingline.Name })
                     .Select(g => new {
                         shippinglineid = g.Key.Shippinglineid,
                         shippinglinename = g.Key.Name,
                         colorList = g.Select(c => new {
                             name = c.Colorcoding.ColorName,
                             id = c.Colorcodingid
                         })
                         .GroupBy(color => new { color.name, color.id })
                         .Select(colorGroup => new {
                             name = colorGroup.Key.name,
                             id = colorGroup.Key.id,
                             count = colorGroup.Count()
                         })
                         .ToList()
                     }).ToListAsync();*/

                var shipColors = await _context.ShippingLines
                    .Select(shippingLine => new ShipColor
                    {
                        ShippingLineId = shippingLine.Id,
                        ShippingLineName = shippingLine.Name,
                        ColorList = _context.CvColorCodings
                        .Select(color => new ColorCount
                        {
                            ColorId = color.Id,
                            ColorName = color.ColorName,
                            Count = _context.VanInspectionReports
                            .Count(v => v.Containervan.Shippingline.Id == shippingLine.Id && v.Colorcodingid == color.Id && v.Hasfinal == 0)
                        }).OrderBy(x => x.ColorId)
                        .ToList()
                    })
                    .ToListAsync();

                var shipStatuses = await _context.ShippingLines
                    .Select(shippingLine => new ShipStatus
                    {
                        ShippingLineId = shippingLine.Id,
                        ShippingLineName = shippingLine.Name,
                        ConvanCount = _context.VanInspectionReports.Count(x => x.Containervan.Shippinglineid == shippingLine.Id && x.Hasfinal == 0),
                        StatusList = _context.CvStatuses
                        .Select(status => new StatusCount
                        {
                            StatusId = status.Id,
                            StatusName = status.Statusname,
                            Count = _context.VanInspectionReports
                            .Count(v => v.Containervan.Shippingline.Id == shippingLine.Id && v.Statusid == status.Id && v.Hasfinal == 0)
                        }).OrderBy(x => x.StatusId)
                        .ToList()
                    })
                    .ToListAsync();

                return Ok(new { totalReceived = totalReceived, forFinal = forFinal, totalDispatch = totalDispatch, cvcolorpersites = shipColors, cvstatuspersites = shipStatuses, cvPerSites = convanPerSites, });
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message, InnerException = e.InnerException });
            }
        }
    }

    public class ShippingLineConvan
    {
        public int? shippinglineid { get; set; }
        public string? shippinglinename { get; set; }
        public List<ContainerVan>? containervanlist { get; set; }
    }

    public class ConvanColorPerShippingline
    {
        public int? shippinglineid { get; set; }
        public string? shippinglinename { get; set; }
        public int? colorid { get; set; }
        public string? colorname { get; set; }
        public int? colorcount { get; set; }


    }

    public class ColorCount
    {
        public int ColorId { get; set; }
        public string ColorName { get; set; }
        public int Count { get; set; }
    }

    public class ShipColor
    {
        public int ShippingLineId { get; set; }
        public string ShippingLineName { get; set; }
        public List<ColorCount> ColorList { get; set; }
    }

    public class ShipStatus
    {
        public int ShippingLineId { get; set; }
        public string ShippingLineName { get; set; }
        public int ConvanCount { get; set; }
        public List<StatusCount> StatusList { get; set; }
    }
    public class StatusCount
    {
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int Count { get; set; }
    }
}
