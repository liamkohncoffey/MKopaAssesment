using System;

namespace SmsService.Definition.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message): base(message) { }
    }
}