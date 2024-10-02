using DevFreela.Core.DTOs;
using System;
using System.Collections.Generic;
namespace DevFreela.Core.Services
{
    public interface IPaymentService
    {
        void ProcessPayment(PaymentInfoDTO paymentInfoDTO);

    }
}
