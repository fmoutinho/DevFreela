using DevFreela.Core.DTOs;
using DevFreela.Core.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace DevFreela.Infrastructure.Payment
{
    public class PaymentService : IPaymentService
    {
        private readonly IMessageBusService _messageBusService;
        private const string QUEUE_NAME = "Payments";
        public PaymentService(IMessageBusService messageBusService)
        {
            _messageBusService = messageBusService;
        }
        public void ProcessPayment(PaymentRequest PaymentRequest)
        {
            var paymentInfoJson = JsonSerializer.Serialize(PaymentRequest);

            var paymentInfoBytes = Encoding.UTF8.GetBytes(paymentInfoJson);

            _messageBusService.Publish(QUEUE_NAME, paymentInfoBytes);
        }
    }
}
