using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP.Models;

namespace FYP.Controllers
{
    public class TeacherController : Controller
    {
        //
        // GET: /Teacher/
        FYP_DB_Entities obj = new FYP_DB_Entities();
        public ActionResult Index()
        {
            try
            {
                var user = (string)Session["User_Id"];
                User u = obj.Users.First(a => a.User_Id.Equals(user));
                List<Subject> subject = u.Subjects.ToList();
                return View(subject);
            }
            catch
            {
                return RedirectToAction("Add_Question", "Teacher");
            }
        }
        public ActionResult Manage_MCQ(int id)
        {
            Subject sub = obj.Subjects.First(a => a.Subject_Id.Equals(id));
            ViewBag.Subject = sub.Subject_Name;
            ViewBag.subject_id = id;
            var mcqs = sub.Questions.Where(a => a.Type.Equals("MCQ-E") || a.Type.Equals("MCQ-M") || a.Type.Equals("MCQ-D")); 
            return View(mcqs);
        }
        public ActionResult Del_Question(int id)
        {
            Question q=obj.Questions.First(a => a.Question_Id.Equals(id));
            obj.Questions.Attach(q);
            obj.Questions.Remove(q);
            obj.SaveChanges();
            return View();
        }
        public ActionResult Edit_Question(int id)
        {
            return View();
        }
        public ActionResult Add_MCQ(int id)
        {
            ViewBag.abc = id;
            return View();
        }
        [HttpPost]
        public ActionResult Add_MCQ(FormCollection fc,int id)
        {
            Option opt = new Option();
            Question qtn = new Question();
            int counter = 1;
            string question1 = fc["Questions"];
            string type1 = fc["Type"];
            qtn.Questions = question1;
            qtn.Subject_Id = id;
            qtn.Type = type1;
            obj.Questions.Add(qtn);
            obj.SaveChanges();
            int count=fc.Count;
            for (int i = 1; i <= (count-counter) ; i++)
            {
                qtn = obj.Questions.OrderByDescending(a => a.Question_Id).First(a => a.Questions.Equals(question1));
                var option1 = fc["Option" + i];
                var check1 = fc["Check" + i];
                opt.Options = option1;
                opt.Question_Id = qtn.Question_Id;
                if (check1 != null)
                {
                    opt.Correct_Answer = "yes"; 
                    counter++;
                }
                else
                {
                    opt.Correct_Answer = " ";
                }
                obj.Options.Add(opt);
                obj.SaveChanges();
            }
            return View();
        }
        public ActionResult Manage_TrueFalse(int id)
        {
            Subject sub = obj.Subjects.First(a => a.Subject_Id.Equals(id));
            ViewBag.Subject = sub.Subject_Name;
            ViewBag.subject_id = id;
            var tf = sub.Questions.Where(a => a.Type.Equals("TF-E") || a.Type.Equals("TF-M") || a.Type.Equals("TF-D"));
            return View(tf);
        }
        public ActionResult Add_TrueFalse(int id)
        {
            ViewBag.abc = id;
            return View();
        }
        [HttpPost]
        public ActionResult Add_TrueFalse(FormCollection fc, int id)
        {
            Question q = new Question();
            Option o = new Option();
            string question = fc["questions"];
            string option = fc["radio1"];
            string type = fc["Type"];
            q.Questions = question;
            q.Question_Id = id;
            q.Type = type;
            obj.Questions.Add(q);
            obj.SaveChanges();
            q = obj.Questions.OrderByDescending(a => a.Question_Id).First(a => a.Questions.Equals(question));
            o.Options = option;
            o.Question_Id = q.Question_Id;
            o.Correct_Answer = " ";
            obj.Options.Add(o);
            obj.SaveChanges();
            return View();
        }

    }
}
