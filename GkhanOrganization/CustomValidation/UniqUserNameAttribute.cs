using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GkhanOrganization.CustomValidation
{
    public class UniqUserNameAttribute:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var username = value.ToString();
            DbOrganization db = new DbOrganization();
            var varmi = db.Users.Where(c => c.UserName == username).Count();
            if (varmi>0)
            {
                ErrorMessage = "This UserName Exist please Try Another Username..";
                return false;
            }
            return true;
        }
    }
}