using System;

namespace SmsService.Definition.Core
{
    public class IntegrationEvent
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Sent { get; set; } = DateTime.UtcNow;

        public Guid CorrelationId { get; set; }
    }
}