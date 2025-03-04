using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MVC.Controllers
{
    [Route("[controller]")]
    public class SectionController : Controller
    {
        private readonly ILogger<SectionController> _logger;

        public SectionController(ILogger<SectionController> logger)
        {
            _logger = logger;
        }

        public IActionResult SectionList()
        {
            return View();
        }
    }
}