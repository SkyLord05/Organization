using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GkhanOrganization.Models
{
    public class OrganizationMemberView
    {
        public int Id { get; set; }
        public virtual UserModelView Users { get; set; }
        public virtual OrganizationModelview Organization { get; set; }
    }
}