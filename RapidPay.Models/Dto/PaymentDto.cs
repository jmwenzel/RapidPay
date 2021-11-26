using System.ComponentModel.DataAnnotations;

namespace RapidPay.Models.Dto
{
    public class PaymentDto
    {
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 15)]
        public string CardNumber { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
