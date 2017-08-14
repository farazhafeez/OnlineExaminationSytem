using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP.Models;

namespace FYP.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        FYP_DB_Entities obj = new FYP_DB_Entities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User u)
        {
            User us = new User();
            
                try
                {
                    us = obj.Users.First(a => a.User_Id.Equals(u.User_Id) && a.Password.Equals(u.Password));
                    if(us.Role.Equals("Teacher"))
                    {
                        Session["User_Id"] = us.User_Id;
                        Session["User_Name"] = us.First_Name +" "+ us.Last_Name;
                        return RedirectToAction("Index","Teacher");
                    }
                    else if (us.Role.Equals("Super_User"))
                    {
                        Session["User_Id"] = us.User_Id;
                        Session["User_Name"] = us.First_Name + " " + us.Last_Name;
                        return RedirectToAction("Index", "SuperUser");
                    }
                    else if (us.Role.Equals("Exam_Controller"))
                    {
                        Session["User_Id"] = us.User_Id;
                        Session["User_Name"] = us.First_Name + " " + us.Last_Name;
                        return RedirectToAction("Index", "ExamController");
                    }
                    else if (us.Role.Equals("Student"))
                    {
                        Session["User_Id"] = us.User_Id;
                        Session["User_Name"] = us.First_Name + " " + us.Last_Name;
                        return RedirectToAction("Index", "Student");
                    }
                }
                catch
                {
                    ViewBag.Message = "Incorrect login details";
                    return View();
                }
           
            return View();
        }

        //[HttpPost]
        public ActionResult LogOut(/*string User_Id*/)
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}
