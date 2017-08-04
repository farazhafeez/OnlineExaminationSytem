//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.ComponentModel.DataAnnotations;
//using System.Web.Mvc;

//namespace FYP.Models
//{
//    [MetadataType(typeof(UserMetaData))]
//    public partial class User
//    {
//    }

//    public class UserMetaData
//    {
//        [Remote("IsUser_IdAvailable", "SuperUser", ErrorMessage = "User id already in use!")]
//        public string User_Id { get; set; }
//    }
//}