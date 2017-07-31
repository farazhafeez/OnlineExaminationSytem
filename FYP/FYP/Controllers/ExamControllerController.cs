using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP.Models;
using System.Data.Entity;

namespace FYP.Controllers
{
    public class ExamControllerController : Controller
    {
        //
        // GET: /ExamController/

        FYP_DB_Entities obj = new FYP_DB_Entities();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageExam()
        {
            var exams = obj.Exams.Include(a=>a.Schedules).Where(x => x.Status != "Conducted");
            return View(exams);
        }

        public ActionResult CreateExam()
        {
            return View(obj.Departments.ToList());
        }

        [HttpPost]
        public ActionResult ExamCreated(int[] subjects , string examTerm , string examSession)
        {
            Subject s = new Subject();
            Exam e = new Exam();
            Batch b = new Batch();
            int marks=0;

            if (examTerm.Equals("Mid"))
            {
                marks = 30;
            }
            else if(examTerm.Equals("Final"))
            {
                marks = 50;
            }

            var u = obj.Users.Where(x => x.Role.Equals("Student"));
            List<Enrolled> enrollmentList = new List<Enrolled>();
            foreach (var i in subjects)
            {
                s = obj.Subjects.First(x => x.Subject_Id.Equals(i));
                b = obj.Batches.First(x => x.Batch_Id == s.Batch_Id);

                e.Subject_Id = s.Subject_Id;
                e.Batch_Id = s.Batch_Id;
                e.Department_Id = b.Department_Id;
                e.Total_Marks = marks;
                e.Exam_Session = examSession;
                e.Status = "Inactive";
                obj.Exams.Add(e);
                obj.SaveChanges();


                foreach (var j in u)
                {
                    Enrolled enrollment = new Enrolled();
                    if (j.Batch_Id == b.Batch_Id)
                    {
                        enrollment.User_Id = j.User_Id;
                        enrollment.Exam_Id = e.Exam_Id;

                        enrollmentList.Add(enrollment);
                    }
                }
            }
            foreach(var i in enrollmentList)
            {
                obj.Enrolleds.Add(i);
                obj.SaveChanges();
            }
            return View();
        }

        public ActionResult ExamEnrollment()
        {
            return View();
        }

        public ActionResult ExamSchedule(int Exam_Id)
        {
            ViewBag.Exam_Id = Exam_Id;    
            try
            {
                var schedule = obj.Schedules.First(x => x.Exam_Id == Exam_Id);
                return View(schedule);
            }
            catch
            {
                Schedule schedule = new Schedule();
                return View(schedule);
            }
        }

        [HttpPost]
        public ActionResult ExamScheduled(int exam, DateTime time, string room)
        {
            Schedule s = new Schedule();

            s.Exam_Id = exam;
            s.Time = time.ToString();
            s.Room_Id = room;

            obj.Schedules.Add(s);
            obj.SaveChanges();

            return View();
        }

        [HttpPost]
        public JsonResult AjaxMethodForDepartment()
        {
            //IEnumerable<Department> departmentList = Enumerable.Empty<Department>();
            
            try
            {
                 var departmentList = obj.Departments.Select(x => x.Department_Id);
                return Json(departmentList);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult AjaxMethodForBatch(string[] departments)
        {
            //IEnumerable<Batch> batchList = Enumerable.Empty<Batch>();
            try
            {
                var b = departments.SelectMany(d => obj.Batches.Where(x => x.Department_Id == d));
                var batchList = b.Select(x => x.Batch_Id);
                return Json(batchList);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult AjaxMethodForSubject(string[] batches)
        {
            //IEnumerable<Subject> subjectList = Enumerable.Empty<Subject>();
            try
            {
                var s = batches.SelectMany(d => obj.Subjects.Where(x => x.Batch_Id == d));
                var subjectList = s.Select(x => new{ x.Subject_Id , x.Subject_Name});
                return Json(subjectList);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult AjaxMethodForExamSchedule()
        {
            try
            {
                var e = obj.Exams.Where(x => x.Status != "Conducted");
                var examList = e.Select(x => new {x.Exam_Id, x.Subject_Id, x.Subject.Subject_Name, x.Batch_Id });
                return Json(examList);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult AjaxMethodForRoom()
        {
            try
            {
                var roomList = obj.Rooms.Select(x => new { x.Room_Id, x.Room_Capacity });
                return Json(roomList);
            }
            catch
            {
                return Json(null);
            }
        }

        //[HttpPost]
        //public JsonResult AjaxMethodForManageExam()
        //{
        //    try
        //    {
        //        var exams = obj.Exams.ToList();
        //        return Json(exams.Select(x => new {x.Exam_Id, x.Total_Marks, x.Subject.Subject_Name, x.Batch_Id, x.Department_Id, x.Exam_Session, x.Status}));
        //    }
        //    catch
        //    {
        //        return Json(null);
        //    }
        //}
    }
}
