using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using MVCProject.Repositories;
using MVCProject.ViewModels.SellerDashboardViewModels;
using MVCProject.ViewModels.AdminDashboardViewModels;
using System.Security.Claims;

namespace MVCProject.Controllers
{
    [Authorize(Roles = "Seller")]
    public class SellerDashboardController : Controller
    {
        private readonly UnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public SellerDashboardController(UnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        private string GetCurrentSellerId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var sellerId = GetCurrentSellerId();
            var model = new SellerDashboardIndexVM();

            model.TotalAccommodations = unitOfWork.AccomodationRepositroy.ConuntBySeller(sellerId);
            model.TotalFlights = unitOfWork.FlightRepository.ConuntBySeller(sellerId);
            model.TotalActivities = unitOfWork.ActivityRepository.ConuntBySeller(sellerId);

            model.RecentAccommodations = unitOfWork.AccomodationRepositroy.GetLatestFiveBySellerId(sellerId)
                .Adapt<List<AccommodationVM>>();

            model.RecentFlights = unitOfWork.FlightRepository.GetLatestFiveBySellerId(sellerId)
                .Adapt<List<FlightVM>>();

            model.RecentActivities = unitOfWork.ActivityRepository.GetLatestFiveBySellerId(sellerId)
                .Adapt<List<ActivityVM>>();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Statistics()
        {
            var sellerId = GetCurrentSellerId();
            var model = new SellerStatisticsVM();

            var flightCount =  unitOfWork.FlightRepository.ConuntBySeller(sellerId);
            var activityCount =  unitOfWork.ActivityRepository.ConuntBySeller(sellerId);
            var accommodationCount = unitOfWork.AccomodationRepositroy.ConuntBySeller(sellerId);

            model.ServiceDistribution = new ServiceDistributionVM
            {
                Services = new List<string> { "Flights", "Activities", "Accommodations" },
                Counts = new List<int> { flightCount, activityCount, accommodationCount }
            };


            var sellerBookings =  unitOfWork.BookingRepository.GetBySellerId(sellerId);

            model.BookingStatusBreakdown = new BookingStatusBreakdownVM
            {
                Statuses = new List<string> { "Pending", "Confirmed", "Cancelled" },
                Counts = new List<int>
                {
                    sellerBookings.Count(b => b.Status == Status.Pending),
                    sellerBookings.Count(b => b.Status == Status.Confirmed),
                    sellerBookings.Count(b => b.Status == Status.Cancelled)
                }
            };

            var sixMonthsAgo = DateTime.UtcNow.AddMonths(-6);


            var confirmedSellerBookings = sellerBookings
                .Where(b => b.BookingDate >= sixMonthsAgo)
                .Select(b => new
                {
                    b.BookingDate,
                    SellerRevenue =
                        (b.bookingAccomodations?
                            .Where(ba => ba.Accomodation?.SellerId == sellerId)
                            .Sum(ba => (ba.Accomodation?.PricePerNight * (ba.CheckOutDate.Date - ba.CheckInDate.Date).Days) ?? 0) +
                        (b.BookingActivities?
                            .Where(ba => ba.Activity?.SellerId == sellerId)
                            .Sum(ba => ba.Activity.Price) ?? 0) +
                        (b.Flight != null && b.Flight.SellerId == sellerId ? b.Flight.Price : 0))
                })
                .ToList();

            var revenueByMonth = confirmedSellerBookings
                .GroupBy(b => new { b.BookingDate.Year, b.BookingDate.Month })
                .Select(g => new
                {
                    Month = new DateTime(g.Key.Year, g.Key.Month, 1, 0, 0, 0, DateTimeKind.Utc),
                    Total = g.Sum(b => b.SellerRevenue)
                }).ToList();

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
            return View(model);
        }
    }
}