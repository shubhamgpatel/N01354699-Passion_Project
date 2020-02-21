using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Install  entity framework 6 
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Programs.Models
{
    public class Teacher
    {
        [Key]
        public int teacher_id { get; set; }
        public string teacherName { get; set; }
        public string teacherEmail { get; set; }

        public string teacherPhone { get; set; }

        public ICollection<Course> Course { get; set; }
    }
}