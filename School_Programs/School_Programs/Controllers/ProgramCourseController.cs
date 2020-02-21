using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using School_Programs.Data;
using School_Programs.Models;
using School_Programs.Models.ViewModels;
namespace School_Programs.Controllers
{
    public class ProgramCourseController : Controller
    {
        private School_ProgramsContext db = new School_ProgramsContext();
        // GET: ProgramCourse
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            //dynamic listing teacher
            // we will display how many courses does one 1 teacher teach
            //e.g : Sean teaches 2 courses. -> javascript, security 
            //we should receive a count for that teacher
            List<Course> courses = db.Courses.SqlQuery("Select * from Courses,Programs,ProgramCourse WHERE course.course_id = ProgramCourse.course_id AND ProgramCourse.program_id = Programs.program_id").ToList();
            /*
             ProgramShow programList = new ProgramShow();
             programList.programs = Program;
             programList.Courses = new Dictionary<int, List<Course>>();*/
            return View(courses);
        }
    }
}