using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories.Models;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentApiController : ControllerBase
    {
        //   private readonly IStudentInterface _student;
        //   public StudentApiController(IStudentInterface student){
        //      _student = student;
        //   }

        //    [HttpPost]
        //     [Route("Register")]
        //     public async Task<IActionResult> Register([FromForm] t_student student)
        //     {
        //         if (student.StudentImage != null && student.StudentImage.Length > 0)
        //         {

        //             var fileName = student.c_email + Path.GetExtension(student.StudentImage.FileName);

        //             var filePath = Path.Combine("../MVC/wwwroot/student_images", fileName);
        //             student.c_image = fileName;
        //             using (var stream = new FileStream(filePath, FileMode.Create))
        //             {
        //                 student.StudentImage.CopyTo(stream);
        //             }
        //         }
        //         var status = await _student.Register(student);


        //         if (status == 1)
        //         {
        //             return Ok(new { success = true, message = "User Registered" });
        //         }
        //         else if (status == 0)
        //         {

        //             return Ok(new { success = false, message = "User Already Exist" });
        //         }
        //         else
        //         {
        //             return BadRequest(new
        //             {
        //                 success = false,
        //                 message = "There was some error while Registration"
        //             });

        //         }
        //     }

        //     [HttpGet]

        //      public async Task<IActionResult> class_Read()
        //     {
        //         var classes = await _student.GetAllClass();
        //         return Ok(classes);
        //     }

        //     [HttpGet]
        //     [Route("section_Read")]
        //       public async Task<IActionResult> section_Read(string id)
        //     {
        //         var section = await _student.GetSectionByClass(id);
        //         return Ok(section);
        //     }

        private readonly IStudentInterface _student;
        public StudentApiController(IStudentInterface student)
        {
            _student = student;
        }

        // public async Task<ActionResult> List()
        // {
        //     List<t_student> students = await
        //     _student.GetAll();
        //     return Ok(students);
        // }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {

            List<t_student> list;
            list = await _student.GetAll();

            return Ok(list);
        }
        [HttpGet]
        [Route("StudentList")]

        public async Task<IActionResult> StudentList()
        {
            var data = await _student.GetData();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetStudentById/{id}")]
        public async Task<IActionResult> GetStudentById(string id)
        {
            var student = await _student.GetOne(id);
            if (student == null)
                return BadRequest(new { success = false, message = "There was no contact found" });
            return Ok(student);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromForm] t_student student)
        {
            if (student.StudentImage != null && student.StudentImage.Length > 0)
            {

                var fileName = student.c_email + Path.GetExtension(student.StudentImage.FileName);

                var filePath = Path.Combine("../MVC/wwwroot/student_images", fileName);
                student.c_image = fileName;
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    student.StudentImage.CopyTo(stream);
                }
            }
            var status = await _student.Register(student);


            if (status == 1)
            {
                // return Ok(new { success = true, message = "User Registered" });
                try
                {
                    string toEmail = student.c_email;
                    string subject = "Your Account Password";
                    string body = $"Dear {student.c_full_name},\n\nYour account has been successfully registered.\n\nYour password is: {student.c_password}\n\nPlease keep it secure.";

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("jeminlad2003@gmail.com", "pkerjfvwgxwsmezw"); // Use App Password here
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("jeminlad2003@gmail.com");
                            mail.To.Add(toEmail);
                            mail.Subject = subject;
                            mail.Body = body;

                            smtp.Send(mail);
                        }
                    }

                    return Ok(new { success = true, message = "User Registered. Password sent to email." });
                }
                catch (Exception ex)
                {
                    return Ok(new { success = true, message = "User Registered, but failed to send password email. Error: " + ex.Message });
                }

            }
            else if (status == 0)
            {

                return Ok(new { success = false, message = "User Already Exist" });
            }
            else
            {
                return BadRequest(new
                {
                    success = false,
                    message = "There was some error while Registration"
                });

            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromForm] t_student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }
            if (student.StudentImage != null && student.StudentImage.Length > 0)
            {
                var fileName = student.c_email + Path.GetExtension(student.StudentImage.FileName);
                var filePath = Path.Combine("../MVC/wwwroot/student_images", fileName);
                student.c_image = fileName;
                System.IO.File.Delete(filePath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    student.StudentImage.CopyTo(stream);
                }
            }
            int status = await _student.Update(student);
            if (status == 1)
            {
                return Ok(new { success = true, message = "Contact Updated Successfully" });
            }
            else
            {
                return BadRequest(new { success = false, message = "There was some error while Update Contact" });
            }
        }

        [HttpGet]
        [Route("teacher_Read")]
        public async Task<IActionResult> teacher_Read()
        {
            var teacher = await _student.GetAllTeacher();
            return Ok(teacher);
        }

        [HttpGet]
        [Route("class_Read")]
        public async Task<IActionResult> class_Read()
        {
            var classes = await _student.GetAllClass();
            return Ok(classes);
        }

        [HttpGet]
        [Route("section_Read")]
        public async Task<IActionResult> section_Read(string id)
        {
            var section = await _student.GetSectionByClass(id);
            return Ok(section);
        }

        // [HttpDelete]
        // [Route("Delete/{id}")]
        // public async Task<ActionResult> Delete(string id)
        // {
        //     int status = await _student.Delete(id);
        //     if (status == 1)
        //     {
        //         return Ok(new { success = true, message = "student Deleted Successfully!!!!!" });
        //     }
        //     else
        //     {
        //         return BadRequest(new { success = false, message = "There was some error while deleting the student" });
        //     }
        // }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            int status = await _student.Delete(id);
            if (status == 1)
            {
                return Ok(new { success = true, message = "Student Deleted Successfully!!!!!" });
            }
            else
            {
                return BadRequest(new { success = false, message = "There was some error while deleting the student" });
            }
        }



    }
}
