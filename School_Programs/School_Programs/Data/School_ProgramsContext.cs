using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace School_Programs.Data
{
    public class School_ProgramsContext : DbContext
    {
        public School_ProgramsContext() : base("name=School_ProgramsContext")
        {
        }

        public System.Data.Entity.DbSet<School_Programs.Models.Teacher> Teachers { get; set; }
        public System.Data.Entity.DbSet<School_Programs.Models.Course> Courses { get; set; }
        public System.Data.Entity.DbSet<School_Programs.Models.Program> Programs { get; set; }
        public System.Data.Entity.DbSet<School_Programs.Models.ProgramCourse> ProgramCourse { get; set; }


    }
}