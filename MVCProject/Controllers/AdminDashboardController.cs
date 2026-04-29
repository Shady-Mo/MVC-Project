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
        private readonly UserManager<AppUser> userManager;

        public AdminDashboardController(UnitOfWork unitOfWork, 
                                        RoleManager<IdentityRole> roleManager, 
                                        UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, string searchName = "")
        {
            const int pageSize = 10;
            var allUsers = unitOfWork.UserRepository.GetAll().ToList();

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                allUsers = allUsers.Where(u => u.FullName.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            int totalUsers = allUsers.Count;
            int activeUsers = allUsers.Count(u => !u.IsBanned);
            int bannedUsers = allUsers.Count(u => u.IsBanned);
            int totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);

            if (page < 1) page = 1;
            if (page > totalPages && totalPages > 0) page = totalPages;

            var paginatedUsers = allUsers
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var usersVM = paginatedUsers.Adapt<List<DisplayUserVM>>();

            foreach (var userVM in usersVM)
            {
                var userEntity = paginatedUsers.First(u => u.Id == userVM.Id);
                var roles = await userManager.GetRolesAsync(userEntity);
                userVM.Role = roles.FirstOrDefault() ?? "No Role";
            }

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.ActiveUsers = activeUsers;
            ViewBag.BannedUsers = bannedUsers;
            ViewBag.PageUsers = usersVM.Count;
            ViewBag.SearchName = searchName;

            return View("Index", usersVM);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var res = await userManager.DeleteAsync(user);
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

                var res = await userManager.CreateAsync(user, addUserVM.Password);
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, addUserVM.SelectedRole);

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
                addUserVM.RoleList.Add(new SelectListItem { 
                    Text = role.Name, Value = role.Name,
                    Selected = role.Name == addUserVM.SelectedRole
                });
            }

            return View(addUserVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var model = user.Adapt<EditUserVM>();

            var roleForUser = await userManager.GetRolesAsync(user);
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
                var user = await userManager.FindByIdAsync(editUserVM.Id);
                if (user == null) return NotFound();

                editUserVM.Adapt(user);

                var res = await userManager.UpdateAsync(user);
                if (res.Succeeded)
                {
                    var currentUserRoles = await userManager.GetRolesAsync(user);

                    if (currentUserRoles.Any())
                        await userManager.RemoveFromRolesAsync(user, currentUserRoles);

                    await userManager.AddToRoleAsync(user, editUserVM.SelectedRole);

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
        [HttpPost]
        public async Task<IActionResult> BanUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsBanned = true;
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UnBanUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsBanned = false;
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
