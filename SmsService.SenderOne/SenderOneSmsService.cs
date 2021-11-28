using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using SmsService.Definition.Core;
using SmsService.Domain.contracts;
using SmsService.Domain.Extensions;
using SmsService.Domain.Requests;

namespace SmsService.SenderOne
{
    public class SenderOneSmsService : ISmsService
    {
        private readonly IBus _bus;

        public SenderOneSmsService(IBus bus)
        {
            _bus = bus;
        }
        
        public bool CanSend(CountryCode countryCode)
        {
            return countryCode == CountryCode.FR;
        }

        public Task SendSms(SendSmsApplicationRequest request, CancellationToken cancellationToken = default)
        {
            request.Validate();
            
            //This would be where we would make the http call to the 3rd party to send the sms
            Console.WriteLine($"SenderOneSmsService: Message: {request.Message}, Sent to recipient {request.Recipient}");

            _bus.Publish(request.ToEvent(), cancellationToken);
            
            return Task.CompletedTask;
        }
    }
}