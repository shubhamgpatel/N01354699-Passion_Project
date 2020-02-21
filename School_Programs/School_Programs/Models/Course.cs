using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Installed  entity framework 6 reference : Christine notes
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace School_Programs.Models
{
    public class Course
    {
        [Key]
        public int course_id { get; set; }
        public string courseName { get; set; }
        public string courseMessage { get; set; }

        // one teacher can teach any courses
        public int teacher_id { get; set; }
        [ForeignKey("teacher_id")]
        public virtual Teacher Teacher { get; set; }
        
        public ICollection<ProgramCourse> ProgramCourses { get; set; }
    }
}