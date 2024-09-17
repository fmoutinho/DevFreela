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
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _paymentBaseUrl;
        public PaymentService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _paymentBaseUrl = configuration.GetSection("Services:Payments").Value;
        }
        public async Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO)
        {
            var url = $"{_paymentBaseUrl}/api/payments";
            var paymentInfoJson = new StringContent(JsonSerializer.Serialize(paymentInfoDTO), Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient("Payments");

            var response = await httpClient.PostAsync(url, paymentInfoJson);

            return response.IsSuccessStatusCode;
        }
    }
}
