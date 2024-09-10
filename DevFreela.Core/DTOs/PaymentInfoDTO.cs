namespace DevFreela.Core.DTOs
{
    public class PaymentInfoDTO
    {
        public PaymentInfoDTO(int id, string creaditCardNumber, string cvv, string expiresAt, string fullName)
        {
            Id = id;
            CreaditCardNumber = creaditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            FullName = fullName;
        }

        public int Id { get; set; }
        public string CreaditCardNumber { get; set; }
        public string Cvv { get; set; }
        public string ExpiresAt { get; set; }
        public string FullName { get; set; }
    }
}
