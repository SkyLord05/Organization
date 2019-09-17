using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.REpository
{
    public class UserRepository:GenericRepository<User>
    {
        public UserRepository(DbOrganization ctx):base(ctx)
        {

        }
    }
}
