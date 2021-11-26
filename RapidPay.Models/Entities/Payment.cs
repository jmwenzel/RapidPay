namespace RapidPay.Models.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }
        public decimal Fee { get; set; }

        public decimal TotalAmount { get; set; }

        //public int? CardId { get; set; }
        public virtual Card Card { get; set; }
    }
}
