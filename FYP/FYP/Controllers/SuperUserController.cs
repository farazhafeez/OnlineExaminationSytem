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
using System.Data.Entity.Validation;

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
        public ActionResult AddTeacher(string User_Id , string First_Name, string Last_Name, string Password, string Contact_No, string Status)
        {
            User u = new User();
            u.User_Id = User_Id;
            u.First_Name = First_Name;
            u.Last_Name = Last_Name;
            u.Password = Password;
            u.Contact_No = Contact_No;
            u.Status = Status;
            u.Role = "Teacher";

            obj.Users.Add(u);
            obj.SaveChanges();
            return RedirectToAction("ManageTeacher", "SuperUser");
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
                return RedirectToAction("ManageTeacher", "SuperUser");
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
            //List<SelectListItem> dl = obj.Departments.AsEnumerable().Select(a => new SelectListItem { Text = a.Department_Id, Value = a.Department_Id }).ToList();
            //ViewBag.d = dl;

            //List<SelectListItem> sl = obj.Batches.AsEnumerable().Select(a => new SelectListItem { Text = a.Batch_Id, Value = a.Batch_Id }).ToList();
            //ViewBag.s = sl;
            //ViewBag.dept = obj.Departments.ToList();
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
            u.Status = "Active";
            u.Role = "Student";

            obj.Users.Add(u);
            obj.SaveChanges();
            return RedirectToAction("ManageStudent", "SuperUser");
        }

        public ActionResult EditStudent(String User_Id)
        {
            User u = obj.Users.Find(User_Id);
            return View(u);
        }

        [HttpPost]
        public ActionResult EditStudent(User student)
        {
            var StudentToUpdate = obj.Users.First(x => x.User_Id.Equals(student.User_Id));
            if (TryUpdateModel(StudentToUpdate, "", new string[] { "User_Id", "First_Name", "Last_Name", "Pasword", "Contact_No", "Department_Id", "Batch_Id", "Section" }))
            {
                obj.SaveChanges();
            }
            return RedirectToAction("ManageStudent", "SuperUser");
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
            l.Add(new SelectListItem { Text = "Exam Controller", Value = "Exam_Controller", Selected = true });
            l.Add(new SelectListItem { Text = "Teacher", Value = "Teacher" });
            l.Add(new SelectListItem { Text = "Student", Value = "Student" });

            ViewBag.RoleList = l;

            return View(u);
        }

        [HttpPost, ActionName("EditExamController")]
        public ActionResult EditExamControllerPost(string User_Id)
        {
            var UserToUpdate = obj.Users.Find(User_Id);
            if (TryUpdateModel(UserToUpdate, "", new string[] { "Password", "First_Name", "Last_Name", "Contact_No", "Role" }))
            {
                obj.SaveChanges();
                return RedirectToAction("ManageExamController", "SuperUser");
            }
            return View(UserToUpdate);
        }











        //Subjects
        public ActionResult ManageSubject(/*changed*//*string User_Id*/)
        {
            var s = obj.Subjects.Where(x => x.Status.Equals("Active"));
            return View(s);
            //changed            
            //try
            //{
            //    var u = obj.Subjects.Where(a => a.User_Id.Equals(User_Id));
            //    User un = obj.Users.First(a => a.User_Id.Equals(User_Id));
            //    ViewBag.User_Name = un.First_Name +" "+ un.Last_Name;
            //    ViewBag.User_Id = User_Id;

            //    return View(u);
            //}
            //catch
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
        }

        public ActionResult AddSubject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSubject(Subject sub, string[] Section)
        {
            sub.Section = null;

            Subject s = new Subject();
            s.Subject_Id = sub.Subject_Id;
            int a = 1;
            foreach(var i in Section)
            {
                if(a == 1)
                {
                    s.Section = i;
                    a++;
                }
                else
                {
                    s.Section = s.Section + "," + i;
                }
            }
            s.Subject_Name = sub.Subject_Name;
            s.User_Id = sub.User_Id;
            s.Batch_Id = sub.Batch_Id;
            s.Status = "Active";
            obj.Subjects.Add(s);
            obj.SaveChanges();

            return RedirectToAction("ManageSubject");
        }

        public ActionResult EditSubject(string Subject_Id)
        {
            try
            {
                var s = obj.Subjects.First(x => x.Subject_Id.Equals(Subject_Id));
                return View(s);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditSubject(Subject subject)
        {
            var SubjectToUpdate = obj.Subjects.First(x => x.Subject_Id.Equals(subject.Subject_Id));
            if (TryUpdateModel(SubjectToUpdate, "", new string[] { "Subject_Name", "User_Id", "Batch_Id", "Section" }))
            {
                obj.SaveChanges();
            }
            return RedirectToAction("ManageSubject", "SuperUser");
        }











        //Activate or Deactivate Users
        public ActionResult EnableUser(string User_Id)
        {
            User u = obj.Users.First(a => a.User_Id.Equals(User_Id));
            u.Status = "Active";
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
        public ActionResult AddDepartment(string Department_Id)
        {
            Department d = new Department();
            d.Department_Id = Department_Id;
            obj.Departments.Add(d);
            obj.SaveChanges();
            return RedirectToAction("ManageDepartment", "SuperUser");
        }











        //Batches
        public ActionResult ManageBatch()
        {
            var b = obj.Batches.Where(x => x.Status.Equals("Active"));
            return View(b);
        }

        public ActionResult AddBatch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddBatch(Batch batch)
        {
            Batch b = new Batch();
            batch.Status = "Active";
            b = batch;
            obj.Batches.Add(b);
            obj.SaveChanges();
            return RedirectToAction("ManageBatch", "SuperUser");
        }









        //Activate or Deactivate Subject
        public ActionResult EnableSubject(int? Subject_Id, string User_Id)
        {
            Subject s = obj.Subjects.First(a => a.Subject_Id.Equals(Subject_Id));
            s.Status = "Active";
            obj.SaveChanges();
            return RedirectToAction("ManageSubject", new { User_Id = User_Id });
        }

        public ActionResult DisableSubject(int? Subject_Id, string User_Id)
        {
            Subject s = obj.Subjects.First(a => a.Subject_Id.Equals(Subject_Id));
            s.Status = "Inactive";
            obj.SaveChanges();
            return RedirectToAction("ManageSubject", new { User_Id = User_Id });
        }



        [HttpPost]
        public JsonResult AjaxMethodForDepartment()
        {
            try
            {
                var dep = obj.Departments.ToList();
                return Json(dep.Select(x => new { x.Department_Id }));
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult AjaxMethodForBatch()
        {
            try
            {
                var batches = obj.Batches.Where(x => x.Status.Equals("Active"));
                return Json(batches.Select(x => new { x.Batch_Id }));
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult AjaxMethodForCompleteBatch(string Batch_Id)
        {
            try
            {
                Batch b = obj.Batches.Find(Batch_Id);
                b.Status = "Completed";
                obj.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult AjaxMethodForGettingTeachers()
        {
            try
            {
                var teacher = obj.Users.Where(x => x.Role.Equals("Teacher") && x.Status.Equals("Active"));
                return Json(teacher.Select(x => new { x.User_Id, x.First_Name, x.Last_Name }));
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult AjaxMethodForSection()
        {
            List<string> sections = new List<string>();
            sections.Add("a");
            sections.Add("b");
            sections.Add("c");
            sections.Add("d");
            sections.Add("e");
            sections.Add("f");
            sections.Add("g");
            sections.Add("h");
            sections.Add("i");
            sections.Add("j");
            sections.Add("k");
            sections.Add("l");
            sections.Add("m");
            sections.Add("n");
            sections.Add("o");
            sections.Add("p");
            sections.Add("q");
            sections.Add("r");
            sections.Add("s");
            sections.Add("t");
            sections.Add("u");
            sections.Add("v");
            sections.Add("w");
            sections.Add("x");
            sections.Add("y");
            sections.Add("z");
            return Json(sections);
        }






        //RemoteValidations
        [HttpPost]
        public JsonResult IsUser_IdAvailable(string User_Id)
        {
            return Json(!obj.Users.Any(a => a.User_Id.Equals(User_Id)));
        }

        [HttpPost]
        public JsonResult IsDepartment_IdAvailable(string Department_Id)
        {
            return Json(!obj.Departments.Any(a => a.Department_Id.Equals(Department_Id)));
        }

        [HttpPost]
        public JsonResult IsBatch_IdAvailable(string Batch_Id)
        {
            return Json(!obj.Batches.Any(a => a.Batch_Id.Equals(Batch_Id)));
        }
        
        [HttpPost]
        public JsonResult IsSubject_IdAvailable(string Subject_Id)
        {
            return Json(!obj.Subjects.Any(a => a.Subject_Id.Equals(Subject_Id)));
        }
    }
}
