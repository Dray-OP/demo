using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace demo.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Mời nhập UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Mời nhập PassWord")]
        public string PassWord { get; set; }
        public bool RememberMe { get; set; }
    }
}