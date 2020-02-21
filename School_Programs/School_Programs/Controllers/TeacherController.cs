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
    public class TeacherController : Controller
    {
        private School_ProgramsContext db = new School_ProgramsContext();

        // GET: Teacher
        public ActionResult List()
        {
            //dynamic listing teacher
            // we will display how many courses does one 1 teacher teach
            //e.g : Sean teaches 2 courses. -> javascript, security 
            //we should receive a count for that teacher
            List<Teacher> teachers = db.Teachers.SqlQuery("Select * from Teachers").ToList();
            Debug.WriteLine("loaded : " + teachers);
            // VIEW MODEL -> teacherlist
            TeachersList teachersList = new TeachersList();
            teachersList.teachers = teachers;
            //list course will send all the variables
            teachersList.teachersCourses = new Dictionary<int, List<Course>>();
            for (int i = 0; i < teachers.Count; i++)
            {
                //https://www.tutorialsteacher.com/csharp/csharp-dictionary for reference
                //https://webcheatsheet.com/asp/dictionary_object.php
                List<Course> courses = db.Courses.SqlQuery("Select * from Courses INNER JOIN Teachers ON  Courses.teacher_id = Teachers.teacher_id where Teachers.teacher_id = @teacher_id", new SqlParameter("@teacher_id", teachers[i].teacher_id)).ToList();
                teachersList.teachersCourses.Add(teachers[i].teacher_id, courses);
                //viewmodel.dictionary.add(teacher[1].id, specific course)
                //add method used froom last semester
                
            }
            Debug.WriteLine(teachersList);
            
            return View(teachersList);

        }

        [HttpPost]
        public ActionResult Add(string teacherName, string teacherEmail, string teacherPhone)
        {
            Debug.WriteLine("In add Method ");
            string query = "insert into Teachers (teacherName, teacherEmail, teacherPhone) values (@teacherName,@teacherEmail,@teacherPhone)";
            SqlParameter[] sqlparams = new SqlParameter[3]; //0,1,2,3,4 pieces of information to add
            //each entry contains key => value pair
            sqlparams[0] = new SqlParameter("@teacherName", teacherName);
            sqlparams[1] = new SqlParameter("@teacherEmail", teacherEmail);
            sqlparams[2] = new SqlParameter("@teacherPhone", teacherPhone);

   
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
        public ActionResult New(){
            return View();
          }
      
        // GET: Teacher/Details/5
        public ActionResult Show(int? id)
        {
            //method use to display specific teacher
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.SqlQuery("select * from Teachers where teacher_id=@teacher_id", new SqlParameter("@teacher_id", id)).FirstOrDefault();
            if (teacher == null)
            {
                return HttpNotFound();
            }
            TeacherShow teacherShow = new TeacherShow();
            List<Course> courses = db.Courses.SqlQuery("Select * from Courses INNER JOIN Teachers ON  Courses.teacher_id = Teachers.teacher_id where Teachers.teacher_id = @teacher_id", new SqlParameter("@teacher_id", id)).ToList();

            teacherShow.teacher = teacher;
            teacherShow.courses = courses;

            return View(teacherShow);
        }
        
        public ActionResult Update(int id)
        {
            //need information about a particular teacher
            // this update is to fetch 
            Teacher selected_teacher = db.Teachers.SqlQuery("select * from Teachers where teacher_id = @id", new SqlParameter("@id", id)).FirstOrDefault();

        
            return View(selected_teacher);
        }

        [HttpPost]
        public ActionResult Update(int id, string teacherName, string teacherEmail, string teacherPhone)
        {

            Debug.WriteLine("Editing  " + teacherName);

            //logic for updating tecaher in the db
            string query = "UPDATE Teachers SET teacherName = @teacherName, teacherEmail = @teacherEmail, teacherPhone = @teacherPhone WHERE teacher_id = @id";
            SqlParameter[] sqlparams = new SqlParameter[4];
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@id", id);
            sqlparams[1] = new SqlParameter("@teacherName", teacherName);
            sqlparams[2] = new SqlParameter("@teacherEmail", teacherEmail);
            sqlparams[3] = new SqlParameter("@teacherPhone", teacherPhone);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
        public ActionResult Delete(int id)
        {
            string query = "Delete from Teachers WHERE teacher_id=@id";
            SqlParameter sqlparam = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparam);

            return RedirectToAction("List");
        }

        public ActionResult Index()
        {
            return View();
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