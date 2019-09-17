using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GkhanOrganization.CustomValidation
{
    public class LoginUserNameControlAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var valstr = value.ToString();
            DbOrganization db = new DbOrganization();
            var dogrumu = db.Users.Where(c => c.UserName == valstr).Count();
            if (dogrumu<=0)
            {
                ErrorMessage = "UserName is not Correct!";
                return false;
            }
            return true;
        }
    }
}