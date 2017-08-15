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
            var u = obj.Users.Where(a => a.Role.Equals("Teacher") && a.Status.Equals("Active"));
            return View(u);
        }

        public ActionResult AddTeacher()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddTeacher(User user)
        {
            User u = new User();
            u = user;
            u.Status = "Active";
            u.Role = "Teacher";

            obj.Users.Add(u);
            obj.SaveChanges();
            return RedirectToAction("ManageTeacher", "SuperUser");
        }

        public ActionResult EditTeacher(string User_Id)
        {
            User u = obj.Users.Find(User_Id);
            return View(u);
        }

        [HttpPost]
        public ActionResult EditTeacher(User teacher)
        {
            var teacherToUpdate = obj.Users.First(x => x.User_Id.Equals(teacher.User_Id));
            teacherToUpdate.First_Name = teacher.First_Name;
            teacherToUpdate.Last_Name = teacher.Last_Name;
            teacherToUpdate.Password = teacher.Password;
            teacherToUpdate.Contact_No = teacher.Contact_No;
            teacherToUpdate.Department = teacher.Department;
            obj.SaveChanges();

            //if (TryUpdateModel(teacherToUpdate, "", new string[] { "User_Id", "First_Name", "Last_Name", "Pasword", "Contact_No", "Department_Id" }))
            //{
            //    obj.SaveChanges();
            //}
            return RedirectToAction("ManageTeacher", "SuperUser");
        }





        //Student Accounts
        public ActionResult ManageStudent()
        {
            try
            {
                var u = obj.Users.Where(a => a.Role.Equals("Student") && a.Status.Equals("Active"));

                return View(u);
            }
            catch
            {
                return RedirectToAction("Index", "SuperUser");
            }
        }

        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(User user)
        {
            User u = new User();
            u = user;
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
            var studentToUpdate = obj.Users.First(x => x.User_Id.Equals(student.User_Id));
            studentToUpdate.First_Name = student.First_Name;
            studentToUpdate.Last_Name = student.Last_Name;
            studentToUpdate.Password = student.Password;
            studentToUpdate.Contact_No = student.Contact_No;
            studentToUpdate.Section = student.Section;
            obj.SaveChanges();

            //if (TryUpdateModel(studentToUpdate, "", new string[] { "User_Id", "First_Name", "Last_Name", "Pasword", "Contact_No", "Section" }))
            //{
            //    obj.SaveChanges();
            //}
            return RedirectToAction("ManageStudent", "SuperUser");
        }










        //Exam controller accounts
        public ActionResult ManageExamController()
        {
            try
            {
                var u = obj.Users.Where(a => a.Role.Equals("Exam_Controller") && a.Status.Equals("Active"));

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
        public ActionResult AddExamController(User user)
        {

            User u = new User();
            u = user;
            u.Status = "Active";
            u.Role = "Exam_Controller";

            obj.Users.Add(u);
            obj.SaveChanges();
            return RedirectToAction("ManageExamController", "SuperUser");

        }

        public ActionResult EditExamController(string User_Id)
        {
            User u = obj.Users.Find(User_Id);
            return View(u);
        }

        [HttpPost]
        public ActionResult EditExamController(User examController)
        {
            var examControllerToUpdate = obj.Users.First(x => x.User_Id.Equals(examController.User_Id));
            examControllerToUpdate.First_Name = examController.First_Name;
            examControllerToUpdate.Last_Name = examController.Last_Name;
            examControllerToUpdate.Password = examController.Password;
            examControllerToUpdate.Contact_No = examController.Contact_No;
            obj.SaveChanges();
            //if (TryUpdateModel(examControllerToUpdate, "", new string[] { "User_Id", "First_Name", "Last_Name", "Pasword", "Contact_No"}))
            //{
            //    obj.SaveChanges();
            //}
            return RedirectToAction("ManageExamController", "SuperUser");
        }






        //Subjects
        public ActionResult ManageSubject()
        {
            var s = obj.Subjects.Where(x => x.Status.Equals("Active"));
            return View(s);
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

        public ActionResult ShowTeacherSubjects(string User_Id)
        {
            ViewBag.user = User_Id + "'s";
            var subjects = obj.Subjects.Where(x => x.User_Id.Equals(User_Id) && x.Status.Equals("Active"));
            return View(subjects);
        }









        //Deactivate Users
        public JsonResult AjaxMethodForDeactivatingUser(string User_Id)
        {
            try
            {
                User u = obj.Users.Find(User_Id);
                u.Status = "Inactive";
                obj.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(null);
            }
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







        //Rooms
        public ActionResult ManageRoom()
        {
            return View(obj.Rooms.ToList());
        }

        public ActionResult AddRoom()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddRoom(Room r)
        {
            obj.Rooms.Add(r);
            obj.SaveChanges();
            return RedirectToAction("ManageRoom", "SuperUser");
        }

        [HttpGet]
        public ActionResult EditRoom(string Room_Id)
        {
            var r = obj.Rooms.Find(Room_Id);
            return View(r);
        }

        [HttpPost]
        public ActionResult EditRoom(Room room)
        {
            var r = obj.Rooms.First(x => x.Room_Id == room.Room_Id);
            r.Room_Capacity = room.Room_Capacity;
            obj.SaveChanges();
            return RedirectToAction("ManageRoom","SuperUser");
        }





        public ActionResult ManageFreezeStudents()
        {
            var students = obj.Users.Where(x => x.Batch.Status == "Active" && x.Status == "Active" || x.Status == "Freeze");
            ViewBag.freezeerror = TempData["freezeerror"];
            return View(students);
        }

     
        public ActionResult FreezeStudent(string User_Id)
        {
            try
            {
                var f = obj.Freezes.First(x => x.Student_Id == User_Id);
                TempData["freezeerror"] = "Cannot freeze " + f.Student_Id + " again because this facility was already availed in the past!";
                return RedirectToAction("ManageFreezeStudents", "SuperUser");
            }
            catch
            {

                var u = obj.Users.First(x => x.User_Id == User_Id);
                return View(u);
            }
        }

        [HttpPost]
        public ActionResult FreezeStudent(string User_Id, DateTime Freezing_Date)
        {
            //try
            //{
                //var f = obj.Freezes.First(x => x.Student_Id == User_Id);
                //TempData["freezeerror"] = "Cannot freeze " + f.Student_Id +" again because this facility was already availed in the past!";
            //}
            //catch
            //{
                Freeze freeze = new Freeze();
                var u = obj.Users.First(x => x.User_Id == User_Id);
                freeze.Student_Id = u.User_Id;
                freeze.Freezing_Date = Freezing_Date;
                freeze.Status = "Freeze";
                u.Status = "Freeze";
                obj.Freezes.Add(freeze);
                obj.SaveChanges();
            //}

            return RedirectToAction("ManageFreezeStudents","SuperUser");
        }

        public ActionResult UnFreezeStudent(string User_Id)
        {
            var u = obj.Users.First(x => x.User_Id == User_Id);
            return View(u);
        }

        [HttpPost]
        public ActionResult UnFreezeStudent(string User_Id, DateTime UnFreezing_Date, string Batch_Id)
        {
            var u = obj.Users.First(x => x.User_Id == User_Id);

            var f = obj.Freezes.First(x => x.Student_Id == User_Id);

            u.Status = "Active";
            u.Batch_Id = Batch_Id;

            f.Unfreezing_Date = UnFreezing_Date;
            f.Status = "Active";

            obj.SaveChanges();

            return RedirectToAction("ManageFreezeStudents", "SuperUser");
        }




        //Activate or Deactivate Subject
        public JsonResult AjaxMethodForDisablingSubject(string Subject_Id)
        {
            try
            {
                var s = obj.Subjects.First(a => a.Subject_Id.Equals(Subject_Id));
                s.Status = "Inactive";
                obj.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(null);
            }
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
        public JsonResult AjaxMethodForBatch(string Department_Id)
        {
            try
            {
                var batches = obj.Batches.Where(x => x.Status.Equals("Active") && x.Department_Id.Equals(Department_Id));
                return Json(batches.Select(x => new { x.Batch_Id }));
            }
            catch
            {
                return Json(null);
            }
        }


        [HttpPost]
        public JsonResult AjaxMethodForBatchInFreeze(string User_Id)
        {
            try
            {
                var s = obj.Users.First(x => x.User_Id == User_Id);
                var b = obj.Batches.Where(x => x.Department_Id == s.Department_Id);

                return Json(b.Select(x => new { x.Batch_Id }));
            }
            catch
            {
                return Json(null);
            }
        }


        [HttpPost]
        public JsonResult AjaxMethodForBatchInAddSubject()
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

        [HttpPost]
        public JsonResult AjaxMethodForRemovingRoom(string Room_Id)
        {
            try
            {
                var r = obj.Rooms.Find(Room_Id);
                obj.Rooms.Attach(r);
                obj.Rooms.Remove(r);
                obj.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(null);
            }
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

        [HttpPost]
        public JsonResult IsRoom_IdAvailable(string Room_Id)
        {
            return Json(!obj.Rooms.Any(a => a.Room_Id.Equals(Room_Id)));
        }
    }
}
