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
    public class ClassController : Controller
    {
        private readonly ILogger<ClassController> _logger;

        public ClassController(ILogger<ClassController> logger)
        {
            _logger = logger;
        }

        public IActionResult ClassList()
        {
            return View();
        }
    }
}