using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Hosting;
using SmsService.Definition.Commands;
using SmsService.Definition.Core;

namespace SmsService
{
    public class SmsSender : BackgroundService
    {
        private readonly IBus _bus;

        public SmsSender(IBus bus)
        {
            _bus = bus;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await _bus.Publish(new SendSmsCommand
                {
                    Message = "Bonjour le monde",
                    CorrelationId = Guid.NewGuid(),
                    Recipient = "+31648429403",
                    CountryCode = CountryCode.FR
                }, cancellationToken);
                
                await _bus.Publish(new SendSmsCommand
                {
                    Message = $"hello world",
                    CorrelationId = Guid.NewGuid(),
                    Recipient = "+31648429403",
                    CountryCode = CountryCode.GB
                }, cancellationToken);
                
                await Task.Delay(5000, cancellationToken);
            }
        }
    }
}
