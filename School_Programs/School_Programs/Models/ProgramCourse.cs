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
    public class ProgramCourse
    {
        [Key]
        public int programCourse_id { get; set; }

        public int course_id { get; set; }
        [ForeignKey("course_id")]
        public virtual Course Course { get; set; }
        public int program_id { get; set; }
        [ForeignKey("program_id")]
        public virtual Program Program { get; set; }


    }
}