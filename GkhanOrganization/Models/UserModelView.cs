using GkhanOrganization.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GkhanOrganization.Models
{
    public class UserModelView
    {
        public int Id { get; set; }
        [Required]
        [UniqUserName]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Age { get; set; }
        public int UserType { get; set; }
        public List<OrganizationModelview> Yap_Organizasyonlar { get; set; }
        public List<OrganizationMemberView> Organizasyonlar { get; set; }

    }
}