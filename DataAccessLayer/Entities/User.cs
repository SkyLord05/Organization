using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public int UserType { get; set; }
        public virtual List<Organization> Yap_Organizasyonlar { get; set; }
        public virtual List<OrganizationMember> Organizasyonlar { get; set; }
        public virtual List<Comment> Yorumlarım { get; set; }
    }
}
