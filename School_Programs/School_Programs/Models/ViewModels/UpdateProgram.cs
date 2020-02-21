using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Programs.Models.ViewModels
{
    public class UpdateProgram
    {
        public Program program{ get; set; }
        public List<Course> courses{ get; set; }
    }
}
