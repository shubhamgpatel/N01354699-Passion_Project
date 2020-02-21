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
    public class ProgramController : Controller
    {
        // GET: Program
        private School_ProgramsContext db = new School_ProgramsContext();
        //private IEnumerable<object> courses_id;

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New()
        {
           // for listing list of courses and displaying in the form
            //List<Course> courses = db.Courses.SqlQuery("select * from Courses").ToList();
            return View();
        }
        public ActionResult List()
        {
            //listing program
          
            List<Program> Program= db.Programs.SqlQuery("Select * from Programs").ToList();
            return View(Program);
        }
        [HttpPost]
        public ActionResult Add(string programName, float programLength)
        {
            string query = "insert into Programs (programName, programLength) values (@programName,@programLength)";
            SqlParameter[] sqlparams = new SqlParameter[2]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@programName", programName);
            sqlparams[1] = new SqlParameter("@programLength", programLength);
            //purposely not asked here for drop down list of all courses here...
            //when we update the program, it will show the list of  all courses.

            Debug.WriteLine(query);
           db.Database.ExecuteSqlCommand(query, sqlparams);
            /*------------------------data inserted in program table-------------------------------------*
            foreach(var i in courses_id)
            {
                string programCourse_query = "insert into ProgramCourses(course_id,program_id) values(@pcourse_id, @program_id) ";
                SqlParameter[] sqlparams_progCourse = new SqlParameter[2]; //0,1,2,3,4 pieces of information to add
           
                sqlparams_progCourse[0] = new SqlParameter("@program_id", program_id);
                sqlparams_progCourse[1] = new SqlParameter("@courses_id", i);

                db.Database.ExecuteSqlCommand(programCourse_query, sqlparams_progCourse);
                //Debug.WriteLine("List :  " + i);
            }
            */

            return RedirectToAction("List");
        }
        public ActionResult Show(int? id)
        {
            // chirstine class project for reference
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program= db.Programs.SqlQuery("Select * from Programs where program_id=@program_id", new SqlParameter("@program_id", id)).FirstOrDefault();
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }
        public ActionResult Update(int  id)
        {
            Program selected_program = db.Programs.SqlQuery("select * from Programs where program_id = @id", new SqlParameter("@id", id)).FirstOrDefault();

            List<Course> courses = db.Courses.SqlQuery("select * from Courses").ToList();
            UpdateProgram programUpdate = new UpdateProgram();
            programUpdate.program = selected_program;
            programUpdate.courses = courses;
            return View(programUpdate);
        }
        [HttpPost]
        public ActionResult Update(int id, string programName, float programLength, int[] course_id)
        {

            Debug.WriteLine("Update details fetched");

            //Updating logic for courses database goes here
            string query = "UPDATE Programs SET programName = @programName, programLength = @programLength WHERE program_id = @id";
            SqlParameter[] sqlparams = new SqlParameter[3];
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@id", id);
            sqlparams[1] = new SqlParameter("@programName", programName);
            sqlparams[2] = new SqlParameter("@programLength", programLength);
            db.Database.ExecuteSqlCommand(query, sqlparams);
            /*------------program database ---------------*/
            foreach (var i in course_id)
            {
                string programCourse_query = "insert into ProgramCourses(course_id,program_id) values(@course_id, @program_id) ";
                SqlParameter[] sqlparams_progCourse = new SqlParameter[2]; //0,1,2,3,4 pieces of information to add

                sqlparams_progCourse[0] = new SqlParameter("@program_id", id);
                sqlparams_progCourse[1] = new SqlParameter("@course_id", i);

                db.Database.ExecuteSqlCommand(programCourse_query, sqlparams_progCourse);
                //Debug.WriteLine("List :  " + i);
            }
            

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
        public ActionResult Delete(int id)
        {
            //delete method for deleting particular course
            //we can do confirm popup by adding additional function
            string query = "Delete from Programs WHERE program_id=@id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparam);

            return RedirectToAction("List");
        }
        //method to release unmanaged resources
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}