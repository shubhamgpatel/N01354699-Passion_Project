using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Programs.Models.ViewModels
{
    public class TeachersList
    {
        //using dictionary for counting and storing into 'int'
        public Dictionary<int, List<Course>> teachersCourses { get; set; }
        public List<Teacher> teachers { get; set; }
    }
}