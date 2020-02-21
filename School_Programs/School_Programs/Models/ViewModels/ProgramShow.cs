using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Programs.Models.ViewModels
{
    public class ProgramShow
    {
        //using dictionary for counting and storing into 'int'
        public Dictionary<int, List<Course>> Courses { get; set; }
        public List<Program> programs { get; set; }
    }
}