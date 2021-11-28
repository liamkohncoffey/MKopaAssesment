using SmsService.Definition.Core;

namespace SmsService.Definition.Commands
{
    public class SendSmsCommand : IntegrationCommand
    {
        public CountryCode CountryCode { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }
    }
}