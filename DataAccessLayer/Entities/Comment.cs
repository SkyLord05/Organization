using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Yorum { get; set; }
        public virtual Organization OrganizationYorum { get; set; }
        public virtual User UserYorum { get; set; }
        public DateTime YorumTime { get; set; }

    }
}
