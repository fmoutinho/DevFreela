using DevFreela.Core.DTOs;
using DevFreela.Core.Services;

namespace DevFreela.Infrastructure.Payment
{
    public class PaymentService : IPaymentService
    {
        public async Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            return await Task.FromResult(true);
        }
    }
}
