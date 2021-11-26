using System;

namespace RapidPay.Service.Services
{
    public class UniversalFeeExchange : IUniversalFeeExchange
    {
        private decimal _fee;
        public decimal Fee { 
            get
            {
                var currentTime = DateTime.Now;
                var lastExecutionInHours = currentTime.Subtract(_LastExecution).Hours;
                if (lastExecutionInHours > 1)
                {
                    for (var i = 0; i < lastExecutionInHours; i++)
                        _fee *= GetRandom();

                    _LastExecution = _LastExecution.AddHours(lastExecutionInHours);
                }
                
                return Math.Round(_fee, 2);
            }
        }

        private DateTime _LastExecution;

        public UniversalFeeExchange()
        {
            _LastExecution = DateTime.Now;
            _fee = GetRandom();
        }

        private decimal GetRandom()
        {
            var maxRange = 2;
            var minRange = 0;

            Random r = new Random();
            return Convert.ToDecimal(r.NextDouble() * (maxRange - minRange) + minRange);
        }
    }
}
