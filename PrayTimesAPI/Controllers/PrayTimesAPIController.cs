using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using PrayTimesAPI.Services;

namespace PrayTimesAPI.Controllers
{
    [Route("api")]
    public class PrayTimesAPIController : Controller
    {
        // [Route("MyName")]
        public string[] Times{ get; set;}
        public IActionResult Index()
        {
            if (!HttpContext.Request.Query.TryGetValue("lat", out StringValues lat)) return BadRequest("lat must be sent");
            if (!HttpContext.Request.Query.TryGetValue("lng", out StringValues lng)) return BadRequest("lng must be sent");
            if (!HttpContext.Request.Query.TryGetValue("tz", out StringValues tz)) return BadRequest("tz must be sent");
            if (!double.TryParse(lat, out double dblLat)) return BadRequest("lat is an incorrect double");
            if (!double.TryParse(lng, out double dblLng)) return BadRequest("lng is an incorrect double");
            if (!double.TryParse(tz, out double dblTZ)) return BadRequest("tz is an incorrect double");
            var dt = DateTime.Now;
            if (HttpContext.Request.Query.TryGetValue("dt", out StringValues strdt)) {
            if (!DateTime.TryParse(strdt, out dt)) return BadRequest("dt is an incorrect double");
            }
            var p = new PrayTime();
            p.setCalcMethod(PrayTime.Jafari);
            Console.WriteLine($"lat: { dblLat}");
            Times = p.getDatePrayerTimes(dt.Year, dt.Month, dt.Day, dblLng,  dblLat, dblTZ, 7);
            return Ok(Times);
        }
    }
}
