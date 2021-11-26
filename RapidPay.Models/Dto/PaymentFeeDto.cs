namespace RapidPay.Models.Dto
{
    public class PaymentFeeDto
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }
    }
}
