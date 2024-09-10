using DevFreela.Core.DTOs;
using System;
using System.Collections.Generic;
namespace DevFreela.Core.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(PaymentInfoDTO paymentInfoDTO);
    }
}
