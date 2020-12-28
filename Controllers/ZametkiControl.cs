using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZametkiList.Models;

namespace ZametkiList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ZametkiController : ControllerBase
    {
        public static List<zam> zametki = new List<zam>();
        private readonly ILogger<ZametkiController> _logger;

        public ZametkiController(ILogger<ZametkiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<zam> Get()
        {
            return zametki;
        }

        [HttpGet("last-id")]
        public int GetLastId()
        {
            return zametki.Count > 0 ? zametki[^1].id + 1 : 0;
        }

        [HttpPut]
        [Consumes("application/json")]
        public IActionResult Add(zam s)
        {
            zametki.Add(s);
            return Created("/zam", s);
        }

        [HttpPost]
        public IActionResult Edit(zam s)
        {
            zam temp = zametki.Find(zametki => zametki.id == s.id);
            temp.zametki = s.zametki;
           
            return Accepted();
        }

        [HttpDelete]
        public IActionResult Delete(IEnumerable<int> m)
        {
            List<zam> temp = zametki.FindAll(s => m.Any(i => i == s.id));
            foreach(var t in temp)
                zametki.Remove(t);
            return Accepted();
        }
    }
}