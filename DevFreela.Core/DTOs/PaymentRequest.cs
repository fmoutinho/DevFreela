using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.DTOs
{
    public class PaymentRequest
    {
        public PaymentRequest(int projectId ,string creditCardNumber, string cvv, string expiresAt, string fullName, decimal value)
        {
            ProjectId = projectId;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            FullName = fullName;
            Value = value;
        }

        public int ProjectId { get; private set; }
        public string CreditCardNumber { get; private set; }
        public string Cvv { get; private set; }
        public string ExpiresAt { get; private set; }
        public string FullName { get; private set; }
        public decimal Value { get; private set; }
    }
}
