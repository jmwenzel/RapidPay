using System;
using System.Collections.Generic;
using System.Text;

namespace RapidPay.Models.Entities
{
    public class Card
    {
        public int CardId { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}
