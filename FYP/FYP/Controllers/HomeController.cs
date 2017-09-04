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
            User user;
            if (Session["User_Id"] != null && Session["User_Password"] != null)
            {
                var user_id = (string)Session["User_Id"];
                var password = (string)Session["User_Password"];
                user = obj.Users.First(a => a.User_Id == user_id && a.Password == password);

                if (user.Role == "Teacher")
                {
                    return RedirectToAction("Index", "Teacher");
                }
                else if (user.Role == "Student")
                {
                    return RedirectToAction("Index", "Student");
                }
                else if (user.Role == "Super_User")
                {
                    return RedirectToAction("Index", "SuperUser");
                }
                else if (user.Role == "Exam_Controller")
                {
                    return RedirectToAction("Index", "ExamController");
                }
            }
            else
            {
                return View();
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(User u)
        {
            User us = new User();
            if (ModelState.IsValid)
            {
                try
                {
                    us = obj.Users.First(a => a.User_Id.Equals(u.User_Id) && a.Password.Equals(u.Password));
                    if (us.Role.Equals("Teacher"))
                    {
                        Session["User_Id"] = us.User_Id;
                        Session["User_Password"] = us.Password;
                        Session["User_Name"] = us.First_Name + " " + us.Last_Name;
                        return RedirectToAction("Index", "Teacher");
                    }
                    else if (us.Role.Equals("Student"))
                    {
                        Session["User_Id"] = us.User_Id;
                        Session["User_Password"] = us.Password;
                        Session["User_Name"] = us.First_Name + " " + us.Last_Name;
                        return RedirectToAction("Index", "Student");
                    }
                    else if (us.Role.Equals("Super_User"))
                    {
                        Session["User_Id"] = us.User_Id;
                        Session["User_Password"] = us.Password;
                        Session["User_Name"] = us.First_Name + " " + us.Last_Name;
                        return RedirectToAction("Index", "SuperUser");
                    }
                    else if (us.Role.Equals("Exam_Controller"))
                    {
                        Session["User_Id"] = us.User_Id;
                        Session["User_Password"] = us.Password;
                        Session["User_Name"] = us.First_Name + " " + us.Last_Name;
                        return RedirectToAction("Index", "ExamController");
                    }
                }
                catch
                {
                    ViewBag.Message = "User not found";
                    return View();
                }
            }
            else
            {
                return View();
            }
            return View();
        }

        public ActionResult LogOut()
        {
            Session["User_Id"] = null;
            Session["User_Password"] = null;

            return RedirectToAction("Index","Home");
        }
    }
}
