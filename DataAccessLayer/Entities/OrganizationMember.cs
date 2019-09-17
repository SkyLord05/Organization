using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class OrganizationMember
    {
        public int Id { get; set; }
        public virtual User Users { get; set; }
        public virtual Organization Organization { get; set; }
        
    }
}
