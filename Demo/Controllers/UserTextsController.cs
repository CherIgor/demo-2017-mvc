using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Demo.Services.Services;
using Demo.ViewModels;
using Microsoft.AspNet.Identity;

namespace Demo.Controllers
{
    public class UserTextsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var savedName = GetNameFromCookies();

            var res = new UserTextsViewModel
            {
                StoredUserTexts = await UsersService.GetUserTexts(),
                NewUserText = new UserTextViewModel
                {
                    Name = savedName,
                    IsNameDisabled = !string.IsNullOrEmpty(savedName)
                }
            };

            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(UserTextViewModel model)
        {
            var savedName = GetNameFromCookies();
            var isNameSaved = !string.IsNullOrEmpty(savedName);

            if (ModelState.IsValid)
            {
                var checkUniqueUserName = !isNameSaved;

                var success = false;
                try
                {
                    await UsersService.AddUserTextAsync(model.ViewModelToModel(), checkUniqueUserName);
                    success = true;
                }
                catch (UsersService.UserAlreadyExistsException)
                {
                    ModelState.AddModelError("", "User already exists");
                }

                if (success)
                {
                    if (!isNameSaved)
                    {
                        SetNameToCookies(model.Name);
                    }
                    return RedirectToAction("Index", "UserTexts");
                }
            }

            model.IsNameDisabled = isNameSaved;
            var res = new UserTextsViewModel
            {
                StoredUserTexts = await UsersService.GetUserTexts(),
                NewUserText = model
            };

            return View(res);
        }

        private string GetNameFromCookies()
        {
            string name = null;
            var encTicket = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (encTicket != null)
            {
                try
                {
                    var ticket = FormsAuthentication.Decrypt(encTicket.Value);
                    if (ticket?.Expiration > DateTime.Now)
                    {
                        name = ticket.UserData;
                    }
                }
                catch
                {
                }
            }

            return name;
        }

        private void SetNameToCookies(string name)
        {
            var isPersistent = true;
            var userData = name;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                User.Identity.GetUserName(),
                DateTime.Now,
                DateTime.Now.AddMinutes(30),
                isPersistent,
                userData,
                FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            string encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
        }
    }
}