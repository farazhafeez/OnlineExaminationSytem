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
    
    public partial class Exam
    {
        public Exam()
        {
            this.Drop_Out = new HashSet<Drop_Out>();
            this.Enrolleds = new HashSet<Enrolled>();
            this.Papers = new HashSet<Paper>();
            this.Results = new HashSet<Result>();
            this.Schedules = new HashSet<Schedule>();
        }
    
        public int Exam_Id { get; set; }
        public Nullable<int> Total_Marks { get; set; }
        public string Subject_Id { get; set; }
        public string Batch_Id { get; set; }
        public string Department_Id { get; set; }
        public string Exam_Session { get; set; }
        public string Status { get; set; }
    
        public virtual Department Department { get; set; }
        public virtual ICollection<Drop_Out> Drop_Out { get; set; }
        public virtual ICollection<Enrolled> Enrolleds { get; set; }
        public virtual Subject Subject { get; set; }
        public virtual ICollection<Paper> Papers { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
