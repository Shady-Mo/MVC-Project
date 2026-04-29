using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using System.Threading.Tasks;
using MVCProject.ViewModels.ProfileViewModels;
using Mapster;
using Microsoft.AspNetCore.OutputCaching;

namespace MVCProject.Controllers {
    public class ProfileController : Controller {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ProfileController(UserManager<AppUser> userManager,
                SignInManager<AppUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [OutputCache(PolicyName = "ProfileExpiry")]
        public async Task<IActionResult> Index() {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return RedirectToAction("Login", "Account");
            }

            var model = user.Adapt<ProfileViewModel>();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile() {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return RedirectToAction("Login", "Account");
            }

            var model = user.Adapt<UpdateProfileViewModel>();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(UpdateProfileViewModel model) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) {
                return RedirectToAction("Login", "Account");
            }

            model.Adapt(user);

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) {
                await _signInManager.RefreshSignInAsync(user);

                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }
    }
}
