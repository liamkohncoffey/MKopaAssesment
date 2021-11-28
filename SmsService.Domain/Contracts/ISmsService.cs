using System.Threading;
using System.Threading.Tasks;
using SmsService.Definition.Core;
using SmsService.Domain.Requests;

namespace SmsService.Domain.contracts
{
    public interface ISmsService
    {
        bool CanSend(CountryCode countryCode);
        
        Task SendSms(SendSmsApplicationRequest request, CancellationToken cancellationToken = default);
    }
}