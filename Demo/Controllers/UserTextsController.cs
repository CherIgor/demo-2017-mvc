using System.Threading.Tasks;
using System.Web.Mvc;
using Demo.Helpers;
using Demo.Services.IServices;
using Demo.Services.Services;
using Demo.ViewModels;

namespace Demo.Controllers
{
    public class UserTextsController : Controller
    {
        private readonly IUsersService _usersService;

        public UserTextsController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<ActionResult> Index()
        {
            var savedName = CookieHelper.GetNameFromCookies(HttpContext);

            var res = new UserTextsViewModel
            {
                StoredUserTexts = await _usersService.GetUserTexts(),
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
            var savedName = CookieHelper.GetNameFromCookies(HttpContext);
            var isNameSaved = !string.IsNullOrEmpty(savedName);

            if (ModelState.IsValid)
            {
                var checkUniqueUserName = !isNameSaved;

                var success = false;
                try
                {
                    await _usersService.AddUserTextAsync(model.ViewModelToModel(), checkUniqueUserName);
                    success = true;
                }
                catch (UsersService.UserAlreadyExistsException)
                {
                    ModelState.AddModelError(string.Empty, "User already exists");
                }

                if (success)
                {
                    if (!isNameSaved)
                    {
                        CookieHelper.SetNameToCookies(Response, model.Name);
                    }
                    return RedirectToAction("Index", "UserTexts");
                }
            }

            model.IsNameDisabled = isNameSaved;
            var res = new UserTextsViewModel
            {
                StoredUserTexts = await _usersService.GetUserTexts(),
                NewUserText = model
            };

            return View(res);
        }
    }
}