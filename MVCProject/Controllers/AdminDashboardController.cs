using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.ViewModels.UserViewModels;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly RoleManager<IdentityRole> roleManager;
        public UserManager<AppUser> UserManager;

        public AdminDashboardController(UnitOfWork unitOfWork, 
                                        RoleManager<IdentityRole> roleManager, 
                                        UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.roleManager = roleManager;
            UserManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var allUsers = unitOfWork.UserRepository.GetAll();
            var usersVM = allUsers.Adapt<List<DisplayUserVM>>();
            return View("Index", usersVM);
        }
        public async Task<IActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var res = await UserManager.DeleteAsync(user);
            if (res.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return BadRequest("Could not delete user.");
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddUserVM();

            var roles = await roleManager.Roles.ToListAsync();
            foreach (var role in roles)
            {
                model.RoleList.Add(new SelectListItem { Text = role.Name, Value = role.Name});
            }
            return View("Add", model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddUserVM addUserVM)
        {
            if (ModelState.IsValid)
            {
                var user = addUserVM.Adapt<AppUser>();

                var res = await UserManager.CreateAsync(user, addUserVM.Password);
                if (res.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, addUserVM.SelectedRole);

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            var dbRoles = await roleManager.Roles.ToListAsync();
            foreach (var role in dbRoles)
            {
                addUserVM.RoleList.Add(new SelectListItem { Text = role.Name, Value = role.Name });
            }

            return View(addUserVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var model = user.Adapt<EditUserVM>();

            var roleForUser = await UserManager.GetRolesAsync(user);
            model.SelectedRole = roleForUser.FirstOrDefault();

            var allRoles = await roleManager.Roles.ToListAsync();
            foreach (var role in allRoles)
            {
                model.RoleList.Add(new SelectListItem { 
                    Text = role.Name, Value = role.Name, Selected = role.Name == model.SelectedRole 
                });
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserVM editUserVM)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUserVM.Id);
                if (user == null) return NotFound();

                editUserVM.Adapt(user);

                var res = await UserManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    var currentUserRoles = await UserManager.GetRolesAsync(user);

                    if (currentUserRoles.Any())
                        await UserManager.RemoveFromRolesAsync(user, currentUserRoles);

                    await UserManager.AddToRoleAsync(user, editUserVM.SelectedRole);

                    if (!string.IsNullOrWhiteSpace(editUserVM.Password))
                    {
                        var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                        await UserManager.ResetPasswordAsync(user, token, editUserVM.Password);
                    }

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in res.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            var allRoles = await roleManager.Roles.ToListAsync();
            foreach (var role in allRoles)
            {
                editUserVM.RoleList.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Name,
                    Selected = role.Name == editUserVM.SelectedRole
                });
            }
            return View(editUserVM);
        }
    }
}
