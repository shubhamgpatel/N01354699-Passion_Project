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
    public class Program
    {
        [Key]
        public int program_id { get; set; }
        public string programName { get; set; }
        public float programLength { get; set; }

        public ICollection<ProgramCourse> ProgramCourse { get; set; }

        public static implicit operator Program(Course v)
        {
            throw new NotImplementedException();
        }
    }
}