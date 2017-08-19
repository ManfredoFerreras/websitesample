using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using AmazonTaxClaim.Models;
using EPS.BO.ADO;
using EPS.BO.Models;

namespace AmazonTaxClaim.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }


        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new LoginViewModel());
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ClienesADO oAdo = new ClienesADO();

            var users = oAdo.ReturnByEPS(model.UserName);


            if (users.CTE_CODIGO_VOICE == model.Password)
            {
                string sName  = users.CTE_NOMBRE.TrimEnd() +" " + users.CTE_APELLIDO.ToString() + "(" + users.CTE_NUMERO_EPS.TrimEnd() +")";

                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, sName), }, DefaultAuthenticationTypes.ApplicationCookie);

                //identity.Name = users.CTE_NUMERO_EPS.TrimEnd() + "-" + users.CTE_NUMERO_EPS.TrimEnd() + users.CTE_APELLIDO.ToString();



                Authentication.SignIn(new AuthenticationProperties
                {
                    IsPersistent = false
                }, identity);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}