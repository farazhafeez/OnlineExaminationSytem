//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FYP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Result
    {
        public int Result_Id { get; set; }
        public string User_Id { get; set; }
        public Nullable<int> Exam_Id { get; set; }
        public Nullable<int> Mid_Marks { get; set; }
        public Nullable<int> Final_Marks { get; set; }
        public string Date_Published { get; set; }
    
        public virtual Exam Exam { get; set; }
        public virtual User User { get; set; }
    }
}
