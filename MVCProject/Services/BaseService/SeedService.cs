using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data;
using MVCProject.Models;

namespace MVCProject.Services.BaseService {
    public class SeedService {
        public static async Task SeedDatabase(IServiceProvider serviceProvider,
                int numberOfCustomers = 15,
                int numberOfSellers = 20,
                int numberOfAccommodations = 105,
                int numberOfActivities = 110,
                int numberOfFlights = 100) {
            using (var scope = serviceProvider.CreateScope()) {
                AppDbContext _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                RoleManager<IdentityRole> _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<AppUser> _userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                ILogger<SeedService> _logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

                try {
                    _logger.LogInformation("Ensuring the database is created.");
                    await _context.Database.MigrateAsync();

                    //_logger.LogInformation("Seeding roles.");
                    //await AddRoleAsync(_roleManager, "Admin");
                    //await AddRoleAsync(_roleManager, "Seller");
                    //await AddRoleAsync(_roleManager, "Customer");

                    //await SeedAdminAsync(_userManager, _logger);
                    //await SeedCustomersAsync(_userManager, numberOfCustomers, _logger);
                    //await SeedSellersAsync(_userManager, numberOfSellers, _logger);

                    //await SeedAccommodationsAsync(_context, numberOfAccommodations, _logger);
                    //await SeedActivitiesAsync(_context, numberOfActivities, _logger);
                    //await SeedFlightsAsync(_context, numberOfFlights, _logger);

                    //_logger.LogInformation("Seeding bookings with accommodations and activities.");
                    //await SeedBookingsAsync(_context, _logger);
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }

        public static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName) {
            if (!await roleManager.RoleExistsAsync(roleName)) {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded) {
                    throw new Exception($"Failed to create role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        private static async Task SeedAdminAsync(UserManager<AppUser> userManager, ILogger logger) {
            string adminEmail = "admin@example.com";
            if (await userManager.FindByEmailAsync(adminEmail) != null)
                return;

            var adminUser = new AppUser {
                FullName = "Shady Mohamed",
                Address = "Cairo",
                Email = adminEmail,
                EmailConfirmed = true,
                UserName = "Shady_Mo",
                PhoneNumber = "0123456789",
                IsBanned = false
            };

            var result = await userManager.CreateAsync(adminUser, "Admin@123");
            if (result.Succeeded) {
                await userManager.AddToRoleAsync(adminUser, "Admin");
                logger.LogInformation("Admin user created.");
            }
            else {
                logger.LogError("Admin creation failed: {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        private static async Task SeedCustomersAsync(UserManager<AppUser> userManager, int count, ILogger logger) {
            int created = 0;
            for (int i = 1; i <= count; i++) {
                string email = $"customer{i}@example.com";
                if (await userManager.FindByEmailAsync(email) != null)
                    continue;

                var customer = new AppUser {
                    FullName = $"Customer {i}",
                    Address = $"Customer Address {i}",
                    Email = email,
                    EmailConfirmed = true,
                    UserName = $"customer_{i}",
                    PhoneNumber = $"0111{i:D4}",
                    IsBanned = false
                };

                var result = await userManager.CreateAsync(customer, "Pass@123");
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(customer, "Customer");
                    created++;
                }
                else {
                    logger.LogError($"Failed to create customer {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            logger.LogInformation($"Seeded {created} new customers.");
        }

        private static async Task SeedSellersAsync(UserManager<AppUser> userManager, int count, ILogger logger) {
            int created = 0;
            for (int i = 1; i <= count; i++) {
                string email = $"seller{i}@example.com";
                if (await userManager.FindByEmailAsync(email) != null)
                    continue;

                var seller = new Seller {
                    FullName = $"Seller {i}",
                    Address = $"Seller Address {i}",
                    Email = email,
                    EmailConfirmed = true,
                    UserName = $"seller_{i}",
                    PhoneNumber = $"0122{i:D4}",
                    IsBanned = false
                };

                var result = await userManager.CreateAsync(seller, "Pass@123");
                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(seller, "Seller");
                    created++;
                }
                else {
                    logger.LogError($"Failed to create seller {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
            logger.LogInformation($"Seeded {created} new sellers.");
        }

        private static async Task SeedAccommodationsAsync(AppDbContext context, int targetCount, ILogger logger) {
            if (await context.Accomodations.AnyAsync())
                return;

            var sellers = await context.Users.OfType<Seller>().ToListAsync();
            if (!sellers.Any()) {
                logger.LogWarning("No sellers found. Cannot seed accommodations.");
                return;
            }

            var random = new Random();
            var countries = new[] { "France", "Egypt", "United States", "Italy", "Spain", "Germany", "United Arab Emirates", "United Kingdom", "Turkey", "Greece", "Thailand", "Mexico", "Japan", "Australia", "Brazil" };
            var hotelPrefixes = new[] { "Grand", "Royal", "Sunset", "Ocean", "Mountain", "City", "Plaza", "Garden", "Palace", "Beach" };
            var hotelSuffixes = new[] { "Hotel", "Resort", "Inn", "Suites", "Lodge", "Villa" };

            int added = 0;
            int attempts = 0;
            int maxAttempts = targetCount * 3;

            while (added < targetCount && attempts < maxAttempts) {
                attempts++;
                string country = countries[random.Next(countries.Length)];
                string city = GetRandomCityForCountry(country, random);
                string prefix = hotelPrefixes[random.Next(hotelPrefixes.Length)];
                string suffix = hotelSuffixes[random.Next(hotelSuffixes.Length)];
                string name = $"{prefix} {suffix} {city}";

                bool exists = await context.Accomodations.AnyAsync(a => a.Name == name);
                if (exists)
                    continue;

                var accommodation = new Accomodation {
                    Name = name,
                    Location = $"{city}, {country}",
                    PricePerNight = Math.Round((decimal)(random.Next(50, 800) + random.NextDouble()), 2),
                    AvailableRooms = random.Next(10, 500),
                    Image = $"26a686db-75b5-43b0-8681-e04eee9dc913_hotel1.jpg",
                    SellerId = sellers[random.Next(sellers.Count)].Id
                };

                await context.Accomodations.AddAsync(accommodation);
                added++;
            }

            if (added > 0)
                await context.SaveChangesAsync();
            logger.LogInformation($"Added {added} new accommodations (target {targetCount}).");
        }

        private static async Task SeedActivitiesAsync(AppDbContext context, int targetCount, ILogger logger) {
            if (await context.Activities.AnyAsync())
                return;

            var sellers = await context.Users.OfType<Seller>().ToListAsync();
            if (!sellers.Any()) {
                logger.LogWarning("No sellers found. Cannot seed activities.");
                return;
            }

            var random = new Random();
            var activityNames = new[]
            {
                "City Tour", "Museum Visit", "Cooking Class", "Hiking Expedition", "Boat Cruise",
                "Wine Tasting", "Desert Safari", "Snorkeling", "Cultural Show", "Zip Lining",
                "Historical Walk", "Art Workshop", "Wildlife Safari", "Helicopter Ride", "Food Tour"
            };
            var countries = new[] { "France", "Egypt", "United States", "Italy", "Spain", "Germany", "United Arab Emirates", "United Kingdom", "Turkey", "Greece", "Thailand", "Mexico", "Japan", "Australia", "Brazil" };

            int added = 0;
            int attempts = 0;
            int maxAttempts = targetCount * 3;

            while (added < targetCount && attempts < maxAttempts) {
                attempts++;
                string country = countries[random.Next(countries.Length)];
                string city = GetRandomCityForCountry(country, random);
                string activityName = activityNames[random.Next(activityNames.Length)];
                string fullName = $"{activityName} in {city}";

                bool exists = await context.Activities.AnyAsync(a => a.Name == fullName);
                if (exists)
                    continue;

                var activity = new Activity {
                    Name = fullName,
                    Location = $"{city}, {country}",
                    Date = DateTime.UtcNow.AddDays(random.Next(1, 90)),
                    Price = Math.Round((decimal)(random.Next(10, 300) + random.NextDouble()), 2),
                    Capacity = random.Next(5, 100),
                    Img = $"1.jpg",
                    SellerId = sellers[random.Next(sellers.Count)].Id
                };

                await context.Activities.AddAsync(activity);
                added++;
            }

            if (added > 0)
                await context.SaveChangesAsync();
            logger.LogInformation($"Added {added} new activities (target {targetCount}).");
        }

        private static async Task SeedFlightsAsync(AppDbContext context, int targetCount, ILogger logger) {
            if (await context.Flights.AnyAsync())
                return;

            var sellers = await context.Users.OfType<Seller>().ToListAsync();
            if (!sellers.Any()) {
                logger.LogWarning("No sellers found. Cannot seed flights.");
                return;
            }

            var random = new Random();
            var airlines = new[] { "EgyptAir", "Air France", "Emirates", "British Airways", "Lufthansa", "American Airlines", "Delta", "Qatar Airways", "Turkish Airlines", "Etihad", "Singapore Airlines", "Cathay Pacific" };
            var airports = new[] { "France", "Egypt", "United States", "Italy", "Spain", "Germany", "United Arab Emirates", "United Kingdom", "Turkey", "Greece", "Thailand", "Mexico", "Japan", "Australia", "Brazil" };

            int added = 0;
            int attempts = 0;
            int maxAttempts = targetCount * 3;

            while (added < targetCount && attempts < maxAttempts) {
                attempts++;
                string airline = airlines[random.Next(airlines.Length)];
                string departure = airports[random.Next(airports.Length)];
                string destination;
                do { destination = airports[random.Next(airports.Length)]; } while (destination == departure);

                var departureDate = DateTime.UtcNow.AddDays(random.Next(1, 60)).AddHours(random.Next(0, 23));

                var departureDateLower = departureDate.AddHours(-6);
                var departureDateUpper = departureDate.AddHours(6);

                bool exists = await context.Flights.AnyAsync(f =>
                    f.Airline == airline &&
                    f.DepartureAirport == departure &&
                    f.DestinationAirport == destination &&
                    f.DepartureDateTime >= departureDateLower &&
                    f.DepartureDateTime <= departureDateUpper);

                if (exists)
                    continue;

                int flightHours = random.Next(2, 14);
                var arrivalDate = departureDate.AddHours(flightHours);

                var flight = new Flight {
                    Airline = airline,
                    DepartureAirport = departure,
                    DestinationAirport = destination,
                    DepartureDateTime = departureDate,
                    ArrivalDateTime = arrivalDate,
                    Price = Math.Round((decimal)(random.Next(100, 2000) + random.NextDouble()), 2),
                    AvailableSeats = random.Next(20, 400),
                    SellerId = sellers[random.Next(sellers.Count)].Id
                };

                await context.Flights.AddAsync(flight);
                added++;
            }

            if (added > 0)
                await context.SaveChangesAsync();
            logger.LogInformation($"Added {added} new flights (target {targetCount}).");
        }

        private static string GetRandomCityForCountry(string country, Random random) {
            var citiesByCountry = new Dictionary<string, string[]> {
                ["France"] = new[] { "Paris", "Nice", "Lyon", "Marseille", "Bordeaux" },
                ["Egypt"] = new[] { "Cairo", "Alexandria", "Giza", "Luxor", "Aswan" },
                ["USA"] = new[] { "New York", "Los Angeles", "Chicago", "Miami", "Las Vegas" },
                ["Italy"] = new[] { "Rome", "Venice", "Florence", "Milan", "Naples" },
                ["Spain"] = new[] { "Madrid", "Barcelona", "Seville", "Valencia", "Granada" },
                ["Germany"] = new[] { "Berlin", "Munich", "Hamburg", "Frankfurt", "Cologne" },
                ["UAE"] = new[] { "Dubai", "Abu Dhabi", "Sharjah" },
                ["UK"] = new[] { "London", "Manchester", "Edinburgh", "Birmingham" },
                ["Turkey"] = new[] { "Istanbul", "Ankara", "Antalya", "Izmir" },
                ["Greece"] = new[] { "Athens", "Santorini", "Mykonos", "Thessaloniki" },
                ["Thailand"] = new[] { "Bangkok", "Phuket", "Chiang Mai", "Pattaya" },
                ["Mexico"] = new[] { "Mexico City", "Cancun", "Guadalajara", "Tijuana" },
                ["Japan"] = new[] { "Tokyo", "Osaka", "Kyoto", "Yokohama" },
                ["Australia"] = new[] { "Sydney", "Melbourne", "Brisbane", "Perth" },
                ["Brazil"] = new[] { "Sao Paulo", "Rio de Janeiro", "Brasilia", "Salvador" }
            };

            if (citiesByCountry.TryGetValue(country, out var cities))
                return cities[random.Next(cities.Length)];
            else
                return country;
        }

        private static async Task SeedBookingsAsync(AppDbContext context, ILogger logger) {
            if (await context.Bookings.AnyAsync())
                return;

            var customers = await context.Users.Where(u => !(u is Seller)).ToListAsync();
            var flights = await context.Flights.ToListAsync();
            var accommodations = await context.Accomodations.ToListAsync();
            var activities = await context.Activities.ToListAsync();

            if (!customers.Any() || !flights.Any() || !accommodations.Any() || !activities.Any()) {
                logger.LogWarning("Missing required data for seeding bookings.");
                return;
            }

            var random = new Random();
            var statuses = new[] { Status.Pending, Status.Confirmed, Status.Cancelled };
            int bookingsAdded = 0;
            int bookingAccommodationsAdded = 0;
            int bookingActivitiesAdded = 0;

            int bookingCount = random.Next(30, 51);
            for (int i = 0; i < bookingCount; i++) {
                var customer = customers[random.Next(customers.Count)];
                var flight = flights[random.Next(flights.Count)];

                var booking = new Booking {
                    UserId = customer.Id,
                    FlightId = flight.Id,
                    Country = flight.DestinationAirport,
                    BookingDate = DateTime.UtcNow.AddDays(-random.Next(0, 30)),
                    Status = statuses[random.Next(statuses.Length)]
                };

                decimal bookingTotal = flight.Price;

                int accommodationCount = random.Next(1, 4);
                for (int j = 0; j < accommodationCount; j++) {
                    var accommodation = accommodations[random.Next(accommodations.Count)];

                    if (booking.bookingAccomodations.Any(ba => ba.AccomodationId == accommodation.Id)) 
                        continue;

                    var checkInDate = flight.DepartureDateTime.Date.AddDays(random.Next(0, 2));
                    var checkOutDate = checkInDate.AddDays(random.Next(2, 8));
                    var nights = (checkOutDate - checkInDate).Days;

                    booking.bookingAccomodations.Add(new BookingAccomodation {
                        Booking = booking,
                        AccomodationId = accommodation.Id,
                        CheckInDate = checkInDate,
                        CheckOutDate = checkOutDate
                    });

                    bookingTotal += accommodation.PricePerNight * nights;
                    bookingAccommodationsAdded++;
                }

                int activityCount = random.Next(1, 4);
                for (int j = 0; j < activityCount; j++) {
                    var activity = activities[random.Next(activities.Count)];

                    if (booking.BookingActivities.Any(ba => ba.ActivityId == activity.Id)) 
                        continue;

                    booking.BookingActivities.Add(new BookingActivity {
                        Booking = booking,
                        ActivityId = activity.Id
                    });

                    bookingTotal += activity.Price;
                    bookingActivitiesAdded++;
                }

                booking.TotalAmount = bookingTotal;
                await context.Bookings.AddAsync(booking);
                bookingsAdded++;
            }

            if (bookingsAdded > 0) {
                await context.SaveChangesAsync();
            }

            logger.LogInformation($"Seeded {bookingsAdded} bookings with {bookingAccommodationsAdded} accommodation bookings and {bookingActivitiesAdded} activity bookings.");
        }
    }
}