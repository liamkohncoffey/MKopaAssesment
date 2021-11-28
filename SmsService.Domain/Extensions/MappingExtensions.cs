using SmsService.Definition.Commands;
using SmsService.Definition.Events;
using SmsService.Domain.Requests;

namespace SmsService.Domain.Extensions
{
    public static class MappingExtensions
    {
        public static SmsSentEvent ToEvent(this SendSmsApplicationRequest request)
        {
            return new SmsSentEvent
            {
                Message = request.Message,
                Recipient = request.Recipient,
                CorrelationId = request.CorrelationId
            };
        }
        
        public static SendSmsApplicationRequest ToRequest(this SendSmsCommand command)
        {
            return new SendSmsApplicationRequest
            {
                CorrelationId = command.CorrelationId,
                Message = command.Message,
                Recipient = command.Recipient
            };
        }
    }
}