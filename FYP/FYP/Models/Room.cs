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
    
    public partial class Room
    {
        public Room()
        {
            this.Schedules = new HashSet<Schedule>();
        }
    
        public string Room_Id { get; set; }
        public Nullable<int> Room_Capacity { get; set; }
    
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
