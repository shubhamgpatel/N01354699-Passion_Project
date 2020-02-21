using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace School_Programs.Models.ViewModels
{
    public class UpdateCourse
    {
        public Course course { get; set; }
        public List<Teacher> teacher { get; set; }
    }
}

/*
1 species    many pets
1 teacher   many course
1 program   many course
*/