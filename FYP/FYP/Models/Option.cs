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
    
    public partial class Option
    {
        public int Option_Id { get; set; }
        public string Options { get; set; }
        public string Correct_Answer { get; set; }
        public Nullable<int> Question_Id { get; set; }
    
        public virtual Question Question { get; set; }
    }
}
