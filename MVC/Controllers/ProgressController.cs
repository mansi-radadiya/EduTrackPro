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
    public class ProgressController : Controller
    {
        public IActionResult Progress_List(){
            return View();
        }
    }
}