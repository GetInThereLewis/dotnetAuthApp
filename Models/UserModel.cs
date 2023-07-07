using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace dotnetAuthApp.Models {
    public class UserModel {
        [Key]
        public int UserId{get; set;}
        public string Username{get; set;}
        public string Email{get; set;}
        public string Password{get; set;}

        
        
    }
}