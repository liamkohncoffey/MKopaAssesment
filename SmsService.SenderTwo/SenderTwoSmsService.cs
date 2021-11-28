using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using SmsService.Definition.Core;
using SmsService.Domain.contracts;
using SmsService.Domain.Extensions;
using SmsService.Domain.Requests;

namespace SmsService.SenderTwo
{
    public class SenderTwoSmsService : ISmsService
    {
        private readonly IBus _bus;

        public SenderTwoSmsService(IBus bus)
        {
            _bus = bus;
        }

        public bool CanSend(CountryCode countryCode)
        {
            return countryCode == CountryCode.GB;
        }

        public Task SendSms(SendSmsApplicationRequest request, CancellationToken cancellationToken = default)
        {
            request.Validate();
            
            //This would be where we would make the http call to the 3rd party to send the sms
            Console.WriteLine($"SenderTwoSmsService: Message: {request.Message}, Sent to recipient {request.Recipient}");

            _bus.Publish(request.ToEvent(), cancellationToken);
            
            return Task.CompletedTask;
        }
    }
}