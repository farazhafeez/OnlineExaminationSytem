using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP.Models
{
    public class QuestionOption
    {
        public Question questions { get; set; }
        public Option options { get; set; }
    }
}