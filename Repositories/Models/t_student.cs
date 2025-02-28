using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Repositories.Models
{
    public class t_student
    {
        [Key]
        public int c_student_id { get; set; }
        public int c_user_id { get; set; }
        [Required(ErrorMessage ="Student Name is required")]
        public string c_full_name{get; set; }
        public DateTime c_date_of_birth {get; set;}
        public string c_gender {get; set;}

         public t_class? Class { get; set; }
        public int c_class_id {get; set;}
        public string? c_class_name {get; set;}
          public t_section? Section { get; set; }

        public int c_section_id {get; set;} 
        public string? c_section_name {get; set;}

        public string c_guardian_details {get; set;}
        public DateTime c_enrollment_date {get; set;}
        public string c_image {get; set;}
        public IFormFile StudentImage{get; set;}
        public string c_status {get; set;}
        public DateTime c_created_at {get; set;}
    }
}