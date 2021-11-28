using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmsService.Definition.Commands;
using SmsService.Domain.contracts;
using SmsService.Domain.Extensions;

namespace SmsService.Integration.Handlers
{
    public class SendSmsCommandConsumer : IConsumer<SendSmsCommand>
    {
        private readonly IEnumerable<ISmsService> _smsService;
        private readonly ILogger<SendSmsCommandConsumer> _logger;

        public SendSmsCommandConsumer(IEnumerable<ISmsService> smsService, ILogger<SendSmsCommandConsumer> logger)
        {
            _smsService = smsService;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<SendSmsCommand> context)
        {
            _logger.LogInformation($"message received: SendSmsCommand: {JsonConvert.SerializeObject(context.Message)}");
            var service = _smsService.FirstOrDefault(c => c.CanSend(context.Message.CountryCode));
            
            if (service == null)
            {
                _logger.LogError($"the country: {context.Message.CountryCode} doesn't have a sms provider registered");
                throw new NotImplementedException($"the country: {context.Message.CountryCode} doesn't have sms provider registered");
            }
            
            return service.SendSms(context.Message.ToRequest(), context.CancellationToken);
        }
    }
}