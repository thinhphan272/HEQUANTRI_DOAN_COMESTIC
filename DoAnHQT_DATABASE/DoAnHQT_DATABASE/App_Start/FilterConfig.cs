﻿using System.Web;
using System.Web.Mvc;

namespace DoAnHQT_DATABASE
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
