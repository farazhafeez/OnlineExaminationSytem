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
        public ActionResult ExamCreated(string[] subjects , string examTerm , string examSession)
        {
            Subject subject = new Subject();
            Exam exam = new Exam();
            Batch batch = new Batch();
            int marks=0;

            var students = obj.Users.Where(x => x.Role.Equals("Student") && x.Status.Equals("Active"));
            List<Enrolled> enrollmentList = new List<Enrolled>();

            var allEnrolleds = obj.Enrolleds.ToList();
            foreach(var i in allEnrolleds)
            {
                obj.Enrolleds.Remove(i);
                obj.SaveChanges();
            }

            var allDropOuts = obj.Drop_Out.ToList();
            foreach (var i in allDropOuts)
            {
                obj.Drop_Out.Remove(i);
                obj.SaveChanges();
            }

            if (examTerm.Equals("Mid"))
            {
                marks = 30;

                foreach (var i in subjects)
                {
                    subject = obj.Subjects.First(x => x.Subject_Id.Equals(i));
                    batch = obj.Batches.First(x => x.Batch_Id.Equals(subject.Batch_Id));

                    exam.Subject_Id = subject.Subject_Id;
                    exam.Batch_Id = subject.Batch_Id;
                    exam.Department_Id = batch.Department_Id;
                    exam.Total_Marks = marks;
                    exam.Exam_Session = examSession;
                    exam.Status = "Active";
                    obj.Exams.Add(exam);
                    obj.SaveChanges();


                    foreach (var j in students)
                    {
                        Enrolled enrollment = new Enrolled();
                        if (j.Batch_Id == batch.Batch_Id && subject.Section.Contains(j.Section))
                        {
                            enrollment.User_Id = j.User_Id;
                            enrollment.Exam_Id = exam.Exam_Id;

                            enrollmentList.Add(enrollment);
                        }
                    }
                }
            }
            else if(examTerm.Equals("Final"))
            {
                marks = 50;
                
                    var midExams = obj.Exams.Where(x => x.Exam_Session.Equals(examSession) && x.Status.Equals("Mid_Conducted"));
                    foreach(var i in midExams)
                    {
                        i.Total_Marks = marks;
                        i.Status = "Final_Pending";

                        foreach (var j in students)
                        {
                            Enrolled enrollment = new Enrolled();
                            if (j.Batch_Id == i.Batch_Id && i.Subject.Section.Contains(j.Section))
                            {
                                enrollment.User_Id = j.User_Id;
                                enrollment.Exam_Id = i.Exam_Id;

                                enrollmentList.Add(enrollment);
                            }
                        }
                    }
                obj.SaveChanges();
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
                var b = departments.SelectMany(d => obj.Batches.Where(x => x.Department_Id == d && x.Status.Equals("Active")));
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
            
            try
            {
                var subjects = batches.SelectMany(d => obj.Subjects.Where(x => x.Batch_Id == d));
                var subjectList = subjects.Select(x => new{ x.Subject_Id , x.Subject_Name , x.Section , x.Batch_Id});
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
