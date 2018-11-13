namespace DHLM.DeviceManagement.API.Services
{    
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class EventOutputProcessor : BackgroundService
    {        
        private readonly ILogger<EventOutputProcessor> _logger;
        //private readonly OrderingBackgroundSettings _settings;
      //  private readonly IEventHub _eventBus;

        //public EventOutputProcessor(IOptions<OrderingBackgroundSettings> settings,
        //IEventBus eventBus,
        //ILogger<EventOutputProcessor> logger)
        public EventOutputProcessor(ILogger<EventOutputProcessor> logger)
        {
            //Constructor’s parameters validations...
            //_eventBus = eventBus;
            //_settings = settings;
            _logger   = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"EventOutputProcessor is starting.");
            //Console.WriteLine("EventOutputProcessor is starting.");

            ///stoppingToken.Register(() => 
            //     _logger.LogDebug($" EventOutputProcessor background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                  //  _logger.LogDebug($"EventOutputProcessor task doing background work.");

                    // This eShopOnContainers method is querying a database table 
                    // and publishing events into the Event Bus (RabbitMS / ServiceBus)
                    //CheckConfirmedGracePeriodOrders();
                    await Task.Delay(1000);
                    //await Task.Delay(_settings.CheckUpdateTime, stoppingToken);
            }

            //_logger.LogDebug($"EventOutputProcessor background task is stopping.");

        }

            public override async Task StopAsync (CancellationToken stoppingToken)
            {
                // Run your graceful clean-up actions
                await Task.Delay(1000);
            }
    }
}