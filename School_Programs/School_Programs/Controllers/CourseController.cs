using System;
using System.Collections.Generic;
using System.Data;
//SqlParameter class running queries
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using School_Programs.Data;
using School_Programs.Models.ViewModels;
using School_Programs.Models;

namespace School_Programs.Controllers
{
    public class CourseController : Controller
    {
        private School_ProgramsContext db = new School_ProgramsContext();
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult New()
        {
            List<Teacher> teachers = db.Teachers.SqlQuery("select * from Teachers").ToList();
            Debug.WriteLine("View initiaze");
            return View(teachers);
        }
        public ActionResult List()
        {
            List<Course> course = db.Courses.SqlQuery("Select * from Courses INNER JOIN Teachers ON  Courses.teacher_id = Teachers.teacher_id").ToList();
            Debug.WriteLine(course);
            return View(course);

        }
        public ActionResult Show(int? id)
        {
            // chirstine class project for reference
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.SqlQuery("Select * from Courses INNER JOIN Teachers ON  Courses.teacher_id = Teachers.teacher_id where course_id=@course_id", new SqlParameter("@course_id", id)).FirstOrDefault();
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }
        [HttpPost]
        public ActionResult Add(string courseName, String courseMessage, int teacher_id)
        {
            //insert data using one string query
            string query = "insert into Courses (courseName, courseMessage, teacher_id) values (@courseName,@courseMessage,@teacher_id)";
            SqlParameter[] sqlparams = new SqlParameter[3]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@courseName", courseName);
            sqlparams[1] = new SqlParameter("@courseMessage", courseMessage);
            sqlparams[2] = new SqlParameter("@teacher_id", teacher_id);

            Debug.WriteLine(query);
            db.Database.ExecuteSqlCommand(query, sqlparams);


            return RedirectToAction("List");
        }
        public ActionResult Update(int id)
        {
            //for fetching  specific course and display in form
            Course selected_course = db.Courses.SqlQuery("select * from Courses where course_id = @id", new SqlParameter("@id", id)).FirstOrDefault();

            List<Teacher> teachers = db.Teachers.SqlQuery("select * from Teachers").ToList();

            //used one class - VIEW MODEL
            UpdateCourse courseUpdate = new UpdateCourse();
            courseUpdate.course = selected_course;
            courseUpdate.teacher = teachers;
            return View(courseUpdate);
        }

        [HttpPost]
        public ActionResult Update(int id, string courseName, String courseMessage, int teacher_id)
        {

            Debug.WriteLine("Update details fetched");

            //Updating logic for courses database goes here
            string query = "UPDATE Courses SET courseName = @courseName, courseMessage = @courseMessage, teacher_id = @teacher_id WHERE course_id = @id";
            SqlParameter[] sqlparams = new SqlParameter[4];
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@id", id);
            sqlparams[1] = new SqlParameter("@courseName", courseName);
            sqlparams[2] = new SqlParameter("@courseMessage", courseMessage);
            sqlparams[3] = new SqlParameter("@teacher_id", teacher_id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
        public ActionResult Delete(int id)
        {
            //delete method for deleting particular course
            //we can do confirm popup by adding additional function
            string query = "Delete from Courses WHERE course_id=@id";
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