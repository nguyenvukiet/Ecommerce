﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyProject.Models;


namespace MyProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //-- Application object để chứa Commoninfo
            Application["dungChung"] = new Commoninfo();
        }
    }
}
