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
    
    public partial class User
    {
        public User()
        {
            this.Attempt_Log = new HashSet<Attempt_Log>();
            this.Drop_Out = new HashSet<Drop_Out>();
            this.Enrolleds = new HashSet<Enrolled>();
            this.Results = new HashSet<Result>();
            this.Subjects = new HashSet<Subject>();
        }
    
        public string User_Id { get; set; }
        public string Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Role { get; set; }
        public string Contact_No { get; set; }
        public string Batch_Id { get; set; }
        public string Department_Id { get; set; }
        public string Section { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<Attempt_Log> Attempt_Log { get; set; }
        public virtual Batch Batch { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Drop_Out> Drop_Out { get; set; }
        public virtual ICollection<Enrolled> Enrolleds { get; set; }
        public virtual ICollection<Result> Results { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
