
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.BO.CC;
using WebSite.BO.Models;

namespace WebSite.Controllers
{
  
    public class NavbarController : Controller
    {

        public ActionResult Navbar()
        {

            MenuHandle oMenu = new MenuHandle();

            var items = oMenu.MenuSuperior();

            return PartialView("_Navbar", items);
        }
    }
}