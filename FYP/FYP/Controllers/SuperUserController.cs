using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FYP.Models;
using System.Data.Entity.Infrastructure;

namespace FYP.Controllers
{
    public class SuperUserController : Controller
    {
        //
        // GET: /SuperUser/

        FYP_DB_Entities obj = new FYP_DB_Entities();

        public ActionResult Index()
        {
            return View();
        }

        

        
        
        
        
        
        
        
        //Teacher Accounts
        public ActionResult ManageTeacher()
        {
            try
            {
                var u = obj.Users.Where(a => a.Role.Equals("Teacher"));
                return View(u);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult AddTeacher()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult AddTeacher(User us)
        {
                User u = new User();
                u.User_Id = us.User_Id;
                u.First_Name = us.First_Name;
                u.Last_Name = us.Last_Name;
                u.Password = us.Password;
                u.Contact_No = us.Contact_No;
                u.Status = us.Status;
                u.Role = "Teacher";

                obj.Users.Add(u);
                obj.SaveChanges();
                return RedirectToAction("ManageTeacher","SuperUser");
        }

        public ActionResult EditTeacher(String User_Id)
        {

            User u = obj.Users.Find(User_Id);
            List<SelectListItem> l = new List<SelectListItem>();
            l.Add(new SelectListItem { Text = "Exam Controller", Value = "Exam_Controller" });
            l.Add(new SelectListItem { Text = "Teacher", Value = "Teacher" });
            l.Add(new SelectListItem { Text = "Student", Value = "Student" });

            ViewBag.User_Id = User_Id;

            ViewBag.RoleList = l;

            return View(u);
        }

        [HttpPost, ActionName("EditTeacher")]
        public ActionResult EditTeacherPost(string User_Id)
        {
            var UserToUpdate = obj.Users.Find(User_Id);
            if (TryUpdateModel(UserToUpdate, "", new string[] { "User_Id", "Password", "First_Name", "Last_Name", "Contact_No", "Role" }))
            {
                    obj.SaveChanges();
                    return RedirectToAction("ManageTeacher","SuperUser");
            }
            return View(UserToUpdate);
        }










        //Student Accounts
        public ActionResult ManageStudent()
        {
            try
            {   
                var u = obj.Users.Where(a => a.Role.Equals("Student"));

                return View(u);
            }
            catch
            {
                return RedirectToAction("Index", "SuperUser");
            }
        }

        public ActionResult AddStudent()
        {
            List<SelectListItem> dl = obj.Departments.AsEnumerable().Select(a => new SelectListItem { Text = a.Department_Id, Value = a.Department_Id }).ToList();
            ViewBag.d = dl;

            List<SelectListItem> sl = obj.Batches.AsEnumerable().Select(a => new SelectListItem { Text = a.Batch_Id, Value = a.Batch_Id}).ToList();
            ViewBag.s = sl;
            ViewBag.dept = obj.Departments.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(User us)
        {
            User u = new User();
            u.User_Id = us.User_Id;
            u.First_Name = us.First_Name;
            u.Last_Name = us.Last_Name;
            u.Password = us.Password;
            u.Contact_No = us.Contact_No;
            u.Batch_Id = us.Batch_Id;
            u.Department_Id = us.Department_Id;
            u.Status = us.Status;
            u.Role = "Student";

            obj.Users.Add(u);
            obj.SaveChanges();
            return RedirectToAction("ManageStudent", "SuperUser");
        }

        public ActionResult EditStudent(String User_Id)
        {

            User u = obj.Users.Find(User_Id);
            List<SelectListItem> l = new List<SelectListItem>();
            l.Add(new SelectListItem { Text = "Exam Controller", Value = "Exam_Controller" });
            l.Add(new SelectListItem { Text = "Teacher", Value = "Teacher" });
            l.Add(new SelectListItem { Text = "Student", Value = "Student" });
            ViewBag.RoleList = l;

            List<SelectListItem> dl = obj.Departments.AsEnumerable().Select(a => new SelectListItem { Text = a.Department_Id, Value = a.Department_Id }).ToList();
            ViewBag.d = dl;

            List<SelectListItem> sl = obj.Batches.AsEnumerable().Select(a => new SelectListItem { Text = a.Batch_Id, Value = a.Batch_Id }).ToList();
            ViewBag.s = sl;

            return View(u);
        }

        [HttpPost, ActionName("EditStudent")]
        public ActionResult EditStudentPost(string User_Id)
        {
            var UserToUpdate = obj.Users.Find(User_Id);
            if (TryUpdateModel(UserToUpdate, "", new string[] { "User_Id", "Password", "First_Name", "Last_Name", "Contact_No", "Role", "Batch_Id", "Department_Id" }))
            {
                    obj.SaveChanges();
                    return RedirectToAction("ManageStudent", "SuperUser");
            }
            return View(UserToUpdate);
        }










        //Exam controller accounts
        public ActionResult ManageExamController()
        {
            try
            {
                var u = obj.Users.Where(a => a.Role.Equals("Exam_Controller"));

                return View(u);
            }
            catch
            {
                return RedirectToAction("Index", "SuperUser");
            }
        }

        public ActionResult AddExamController()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddExamController(User us)
        {
            
                User u = new User();
                u.User_Id = us.User_Id;
                u.First_Name = us.First_Name;
                u.Last_Name = us.Last_Name;
                u.Password = us.Password;
                u.Contact_No = us.Contact_No;
                u.Status = us.Status;
                u.Role = "Exam_Controller";

                obj.Users.Add(u);
                obj.SaveChanges();
                return RedirectToAction("ManageExamController", "SuperUser");
            
        }

        public ActionResult EditExamController(String User_Id)
        {

            User u = obj.Users.Find(User_Id);
            List<SelectListItem> l = new List<SelectListItem>();
            l.Add(new SelectListItem { Text = "Exam Controller", Value = "Exam_Controller",Selected=true });
            l.Add(new SelectListItem { Text = "Teacher", Value = "Teacher" });
            l.Add(new SelectListItem { Text = "Student", Value = "Student" });

            ViewBag.RoleList = l;

            return View(u);
        }

        [HttpPost, ActionName("EditExamController")]
        public ActionResult EditExamControllerPost(string User_Id)
        {
            var UserToUpdate = obj.Users.Find(User_Id);
            if (TryUpdateModel(UserToUpdate, "", new string[] { "Password", "First_Name", "Last_Name", "Contact_No", "Role"}))
            {
                obj.SaveChanges();
                return RedirectToAction("ManageExamController", "SuperUser");
            }
            return View(UserToUpdate);
        }
        










        //Subjects
        public ActionResult ManageSubject(string User_Id)
        {
            try
            {
                var u = obj.Subjects.Where(a => a.User_Id.Equals(User_Id));
                User un = obj.Users.First(a => a.User_Id.Equals(User_Id));
                ViewBag.User_Name = un.First_Name +" "+ un.Last_Name;
                ViewBag.User_Id = User_Id;

                return View(u);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult AddSubject(string User_Id)
        {
            ViewBag.User_Id = User_Id;
            return View();
        }

        [HttpPost]
        public ActionResult AddSubject(Subject sub,string User_Id)
        {
            Subject s = new Subject();
            s.Subject_Name = sub.Subject_Name;
            s.Status = sub.Status;
            s.User_Id = User_Id;

            obj.Subjects.Add(s);
            obj.SaveChanges();
            return RedirectToAction("ManageSubject", new {User_Id = User_Id });
        }

        public ActionResult EditSubject(int? Subject_Id, string User_Id)
        {
           
            Subject s = obj.Subjects.Find(Subject_Id);
            return View(s);
        }

        [HttpPost, ActionName("EditSubject")]
        public ActionResult EditSubjectPost(int? Subject_Id, string User_Id)
        {
            
            var SubjectToUpdate = obj.Subjects.Find(Subject_Id);
            if (TryUpdateModel(SubjectToUpdate, "", new string[] { "Subject_Name" }))
            {
                    obj.SaveChanges();
                    return RedirectToAction("ManageSubject", new {User_Id = User_Id });
            }
            return View(SubjectToUpdate);
        }

        









        //Activate or Deactivate Users
        public ActionResult EnableUser(string User_Id)
        {
            User u  =   obj.Users.First(a => a.User_Id.Equals(User_Id));
            u.Status = "Active";
            obj.SaveChanges();
            if (u.Role.Equals("Teacher"))
            {
                return RedirectToAction("ManageTeacher", "SuperUser");
            }
            else if(u.Role.Equals("Student"))
            {
                return RedirectToAction("ManageStudent", "SuperUser");
            }
            else if (u.Role.Equals("Exam_Controller"))
            {
                return RedirectToAction("ManageExamController", "SuperUser");
            }
            return RedirectToAction("Index", "SuperUser");
        }

        public ActionResult DisableUser(string User_Id)
        {
            User u = obj.Users.First(a => a.User_Id.Equals(User_Id));
            u.Status = "Inactive";
            obj.SaveChanges();
            if (u.Role.Equals("Teacher"))
            {
                return RedirectToAction("ManageTeacher", "SuperUser");
            }
            else if (u.Role.Equals("Student"))
            {
                return RedirectToAction("ManageStudent", "SuperUser");
            }
            else if (u.Role.Equals("Exam_Controller"))
            {
                return RedirectToAction("ManageExamController", "SuperUser");
            }
            return RedirectToAction("Index", "SuperUser");
        }










        //Departments
        public ActionResult ManageDepartment()
        {
            return View(obj.Departments.ToList());
        }

        public ActionResult AddDepartment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDepartment(Department dept)
        {
            Department d = new Department();
            d = dept;
            obj.Departments.Add(d);
            obj.SaveChanges();
            return RedirectToAction("ManageDepartment", "SuperUser");
        }











        //Batches
        public ActionResult ManageBatch()
        {
            return View(obj.Batches.ToList());
        }

        public ActionResult AddBatch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBatch(Batch batch)
        {
            Batch b = new Batch();
            b = batch;
            obj.Batches.Add(b);
            obj.SaveChanges();
            return RedirectToAction("ManageBatch", "SuperUser");
        }









        //Activate or Deactivate Subject
        public ActionResult EnableSubject(int? Subject_Id, string User_Id)
        {
            Subject s = obj.Subjects.First(a =>a.Subject_Id == Subject_Id);
            s.Status = "Active";
            obj.SaveChanges();
            return RedirectToAction("ManageSubject", new { User_Id = User_Id });
        }

        public ActionResult DisableSubject(int? Subject_Id, string User_Id)
        {
            Subject s = obj.Subjects.First(a =>a.Subject_Id == Subject_Id);
            s.Status = "Inactive";
            obj.SaveChanges();
            return RedirectToAction("ManageSubject", new { User_Id = User_Id });
        }










        //Custom validation for user id
        public JsonResult IsUser_IdAvailable(string User_Id)
        {
            return Json(!obj.Users.Any(a => a.User_Id.Equals(User_Id)), JsonRequestBehavior.AllowGet);
        }

    }
}
