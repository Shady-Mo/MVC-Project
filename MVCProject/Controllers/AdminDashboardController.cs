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
using MVCProject.ViewModels.AdminDashboardViewModels;

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
        public async Task<IActionResult> Index(int page = 1, string searchName = "", string role = "", string status = "")
        {
            const int pageSize = 10;
            var allUsers = unitOfWork.UserRepository.GetAll().ToList();

            // Get all user roles for dropdown
            var allRoles = await roleManager.Roles.Select(r => r.Name).ToListAsync();
            ViewBag.Roles = allRoles;
            ViewBag.SelectedRole = role;
            ViewBag.SelectedStatus = status;

            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(searchName))
            {
                allUsers = allUsers.Where(u => u.FullName.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Apply role filter if provided
            if (!string.IsNullOrWhiteSpace(role))
            {
                var usersWithRole = new List<AppUser>();
                foreach (var user in allUsers)
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    if (userRoles.Contains(role))
                    {
                        usersWithRole.Add(user);
                    }
                }
                allUsers = usersWithRole;
            }

            // Apply status filter if provided
            if (!string.IsNullOrWhiteSpace(status))
            {
                if (status == "active")
                {
                    allUsers = allUsers.Where(u => !u.IsBanned).ToList();
                }
                else if (status == "banned")
                {
                    allUsers = allUsers.Where(u => u.IsBanned).ToList();
                }
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

        [HttpGet]
        public async Task<IActionResult> Statistics()
        {
            var model = new DashboardStatisticsVM();

            // 1. Revenue Overview - Last 6 months of confirmed bookings
            var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);
            var confirmedBookings = await unitOfWork.BookingRepository
                .GetAll()
                .Where(b => b.Status == Status.Confirmed && b.BookingDate >= sixMonthsAgo)
                .ToListAsync();

            var revenueByMonth = confirmedBookings
                .GroupBy(b => new { b.BookingDate.Year, b.BookingDate.Month })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Month)
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1, 0, 0, 0, DateTimeKind.Utc),
                    Total = g.Sum(b => b.TotalAmount)
                })
                .ToList();

            // Ensure all 6 months are represented (even with 0 revenue)
            var allMonths = new List<DateTime>();
            for (int i = 5; i >= 0; i--)
            {
                var date = DateTime.UtcNow.AddMonths(-i);
                allMonths.Add(new DateTime(date.Year, date.Month, 1, 0, 0, 0, DateTimeKind.Utc));
            }

            model.RevenueOverview = new RevenueOverviewVM
            {
                Months = allMonths.Select(m => m.ToString("MMM yyyy")).ToList(),
                Revenue = allMonths.Select(m =>
                    revenueByMonth.FirstOrDefault(r => r.Month == m)?.Total ?? 0
                ).ToList()
            };

            // 2. Service Distribution - Counts from join tables
            var flightBookings = await unitOfWork.BookingRepository
                .GetAll()
                .Where(b => b.FlightId != 0)
                .CountAsync();

            var activityBookings = (await unitOfWork.BookingRepository
                .GetAll()
                .Include(b => b.BookingActivities)
                .ToListAsync())
                .SelectMany(b => b.BookingActivities ?? new List<BookingActivity>())
                .Count();

            var accommodationBookings = (await unitOfWork.BookingRepository
                .GetAll()
                .Include(b => b.bookingAccomodations)
                .ToListAsync())
                .SelectMany(b => b.bookingAccomodations ?? new List<BookingAccomodation>())
                .Count();

            model.ServiceDistribution = new ServiceDistributionVM
            {
                Services = new List<string> { "Flights", "Activities", "Accommodations" },
                Counts = new List<int> { flightBookings, activityBookings, accommodationBookings }
            };

            // 3. Booking Status Breakdown
            var bookingsByStatus = await unitOfWork.BookingRepository
                .GetAll()
                .GroupBy(b => b.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToListAsync();

            var pendingCount = bookingsByStatus.FirstOrDefault(b => b.Status == Status.Pending)?.Count ?? 0;
            var confirmedCount = bookingsByStatus.FirstOrDefault(b => b.Status == Status.Confirmed)?.Count ?? 0;
            var cancelledCount = bookingsByStatus.FirstOrDefault(b => b.Status == Status.Cancelled)?.Count ?? 0;

            model.BookingStatusBreakdown = new BookingStatusBreakdownVM
            {
                Statuses = new List<string> { "Pending", "Confirmed", "Cancelled" },
                Counts = new List<int> { pendingCount, confirmedCount, cancelledCount }
            };

            // 4. Top 5 Destinations
            var topDestinations = await unitOfWork.BookingRepository
                .GetAll()
                .Where(b => !string.IsNullOrWhiteSpace(b.Country))
                .GroupBy(b => b.Country)
                .OrderByDescending(g => g.Count())
                .Take(5)
                .Select(g => new { Country = g.Key, Count = g.Count() })
                .ToListAsync();

            model.TopDestinations = new TopDestinationsVM
            {
                Countries = topDestinations.Select(d => d.Country).ToList(),
                BookingCounts = topDestinations.Select(d => d.Count).ToList()
            };

            return View(model);
        }
    }
}
