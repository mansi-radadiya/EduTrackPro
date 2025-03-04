using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    public class StudentController : Controller
    {
        // GET: StudentController
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }
        public ActionResult Index()
        {
            return View();
        }
         public IActionResult Register()
        {
                       return View();


         }
         public IActionResult StudentList()
         {
                       return View();

         }
    }
}
