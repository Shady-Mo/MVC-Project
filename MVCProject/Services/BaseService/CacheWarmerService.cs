using System.Net.Http;

namespace MVCProject.Services {
    public class CacheWarmerService : IHostedService {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<CacheWarmerService> _logger;

        public CacheWarmerService(IServiceProvider serviceProvider, ILogger<CacheWarmerService> logger) {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken) {
            _logger.LogInformation("Cache Warmer Service is starting...");

            await Task.Delay(2000, cancellationToken);

            using var scope = _serviceProvider.CreateScope();
            var httpClientFactory = scope.ServiceProvider.GetRequiredService<IHttpClientFactory>();
            var client = httpClientFactory.CreateClient();

            var urlsToWarm = new List<string> {
                "http://localhost:5051/",
                "http://localhost:5051/Home/Filter?location=all&price=max",
                "http://localhost:5051/Home/Filter?location=Egypt&price=max",
            };

            foreach (var url in urlsToWarm) {
                try {
                    _logger.LogInformation($"Warming up: {url}");
                    await client.GetAsync(url, cancellationToken);
                }
                catch (Exception ex) {
                    _logger.LogWarning($"Failed to warm up {url}: {ex.Message}");
                }
            }

            _logger.LogInformation("Cache Warming completed!");
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}