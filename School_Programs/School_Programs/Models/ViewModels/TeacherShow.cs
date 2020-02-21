using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Programs.Models.ViewModels
{
    public class TeacherShow
    {   //use to show list of different courses taught by teacher on show page
        //eg : Bernie teach shows Project management, Web info architecture.
        public List<Course> courses { get; set; }
        public Teacher teacher { get; set; }
    }
}