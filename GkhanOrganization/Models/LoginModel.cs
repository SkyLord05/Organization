using GkhanOrganization.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GkhanOrganization.Models
{
    public class LoginModel
    {
        [Required]
        [LoginUserNameControl]
        public string UserName { get; set; }
        [Required]
        [LoginPasswordControl]
        public string Password { get; set; }
    }
}