namespace DevFreela.Core.DTOs
{
    public class PaymentInfoDTO
    {
        public PaymentInfoDTO(string creditCardNumber, string cvv, string expiresAt, string fullName)
        {
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            FullName = fullName;
        }

        public string CreditCardNumber { get; set; }
        public string Cvv { get; set; }
        public string ExpiresAt { get; set; }
        public string FullName { get; set; }
    }
}
