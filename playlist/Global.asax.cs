using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;

namespace TestTwo_20151
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer(new TestTwo_20151.Models.Initializer());

            ViewModels.ConfigureMaps.ForImage();
            ViewModels.ConfigureMaps.ForMovie();
            ViewModels.ConfigureMaps.ForGenre();
            ViewModels.ConfigureMaps.ForDirector();
        }
    }
}
