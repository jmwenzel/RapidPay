using System.ComponentModel.DataAnnotations;

namespace RapidPay.Models.Dto
{
    public class CardDto
    {
        [Required]
        [StringLength(maximumLength: 15, MinimumLength = 15)]
        public string Number { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
