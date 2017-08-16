using FYP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP.Controllers
{
    public class ExamResultController : Controller
    {
        //
        // GET: /ExamResult/
        FYP_DB_Entities obj = new FYP_DB_Entities();
        public ActionResult ResultForSuperUser()
        {
            return View(obj.Results.ToList());
        }

        public ActionResult ResultForExamController()
        {
            return View(obj.Results.ToList());
        }

    }
}
