using DataAccessLayer;
using DataAccessLayer.REpository;
using GkhanOrganization.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GkhanOrganization.CustomControllerFactory
{
    public class MyControllerFactory:DefaultControllerFactory
    {
        static OrganizationRepository Orgrep;
        static UserRepository userrep;
        static OrganizationMemberRepository Orgmemrep;
        static CommentRepository commentrepository;
        static DbOrganization ctx;
        public override IController CreateController(RequestContext requestContext, string controllerName)
        {
            if (ctx==null)
            {
                ctx = new DbOrganization();
            }
            if (commentrepository == null)
            {
                commentrepository = new CommentRepository(ctx);
            }
            if (Orgrep==null)
            {
                Orgrep = new OrganizationRepository(ctx);
            }
            if (userrep == null)
            {
                userrep = new UserRepository(ctx);
            }
            if (Orgmemrep == null)
            {
                Orgmemrep = new OrganizationMemberRepository(ctx);
            }

            if (controllerName=="Home")
            {
                return new HomeController(Orgrep, userrep, Orgmemrep, commentrepository);
            }
            if (controllerName=="User")
            {
                return new UserController(userrep);
            }


            return base.CreateController(requestContext, controllerName);
        }
    }
}