﻿using System;
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
            var exams = obj.Exams.Include(a=>a.Schedules).Where(x => x.Status != "Mid_Conducted" && x.Status != "Final_Conducted");
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
            Schedule schedule = new Schedule();
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

            var allSchedules = obj.Schedules.ToList();
            foreach(var i in allSchedules)
            {
                obj.Schedules.Remove(i);
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
                    exam.Status = "Enable";
                    schedule.Exam_Id = exam.Exam_Id;
                    obj.Exams.Add(exam);
                    obj.Schedules.Add(schedule);
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
                        i.Status = "Enable";

                        schedule.Exam_Id = i.Exam_Id;
                        obj.Schedules.Add(schedule);
                        obj.SaveChanges();

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

        public ActionResult Enrolleds(int Exam_Id)
        {
            var enrolleds = obj.Enrolleds.Where(x => x.Exam_Id == Exam_Id);
            return View(enrolleds);
        }


        public ActionResult DropOuts(int Exam_Id)
        {
            var dropOuts = obj.Drop_Out.Where(x => x.Exam_Id == Exam_Id);
            return View(dropOuts);
        }


        public ActionResult ExamSchedule(int Exam_Id)
        {
            var schedule = obj.Schedules.First(x => x.Exam_Id == Exam_Id);
            return View(schedule);
        }


        [HttpPost]
        public JsonResult Drop(int Enrolled_Id)
        {
            try
            {
                var e = obj.Enrolleds.Find(Enrolled_Id);
                Drop_Out d = new Drop_Out();
                d.Exam_Id = e.Exam_Id;
                d.User_Id = e.User_Id;

                obj.Drop_Out.Add(d);
                obj.Enrolleds.Remove(e);
                obj.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(null);
            }
        }


        [HttpPost]
        public JsonResult Enroll(int Drop_Out_Id)
        {
            try
            {
                var d = obj.Drop_Out.Find(Drop_Out_Id);
                Enrolled e = new Enrolled();
                e.Exam_Id = d.Exam_Id;
                e.User_Id = d.User_Id;

                obj.Enrolleds.Add(e);
                obj.Drop_Out.Remove(d);
                obj.SaveChanges();
                return Json(true);
            }
            catch
            {
                return Json(null);
            }
        }


        [HttpPost]
        public ActionResult AddExamSchedule(Schedule schedule)
        {
            var s = obj.Schedules.First(x => x.Schedule_Id.Equals(schedule.Schedule_Id));
            s.Time_From = schedule.Time_From;
            s.Time_To = schedule.Time_To;
            s.Room_Id = schedule.Room_Id;
            obj.SaveChanges();    
            return RedirectToAction("ManageExam", "ExamController");
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
                var rooms = obj.Rooms.ToList();
                var roomList = rooms.Select(x => new { x.Room_Id, x.Room_Capacity });
                return Json(roomList);
            }
            catch
            {
                return Json(null);
            }
        }
    }
}
