using System;
using System.Text.RegularExpressions;
using SmsService.Definition.Core.Exceptions;

namespace SmsService.Domain.Requests
{
    public class SendSmsApplicationRequest
    {
        public Guid CorrelationId { get; set; }
        public string Recipient { get; set; }
        public string Message { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Message))
            {
                throw new ValidationException("Message must contain value");
            }

            if(!Regex.IsMatch(Recipient, "^\\+[1-9]{1}[0-9]{7,11}$"))
            {
                throw new ValidationException($"{Recipient} is not a valid phone number");
            }
        }
    }
}