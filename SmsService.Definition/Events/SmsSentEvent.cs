using SmsService.Definition.Core;

namespace SmsService.Definition.Events
{
    public class SmsSentEvent : IntegrationEvent
    {
        public string Recipient { get; set; }
        public string Message { get; set; }
    }
}