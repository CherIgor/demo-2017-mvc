using System.Threading.Tasks;
using System.Web.Mvc;
using Demo.Models;
using Demo.Services.Services;

namespace Demo.Controllers
{
    [Authorize]
    public class UserTextsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var res = new UserTextsViewModel
            {
                OldUserTexts = await UsersService.GetUserTexts(),
                NewUserText = new UserTextViewModel()
            };

            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(UserTextViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await UsersService.AddUserTextAsync(model.ViewModelToModel());
                }
                catch (UsersService.UserAlreadyExistsException)
                {
                    ModelState.AddModelError("", "User already exists");
                }
            }

            var res = new UserTextsViewModel
            {
                OldUserTexts = await UsersService.GetUserTexts(),
                NewUserText = ModelState.IsValid ? new UserTextViewModel() : model
            };

            return View(res);
        }
    }
}