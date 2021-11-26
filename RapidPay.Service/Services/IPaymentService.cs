using RapidPay.Models.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RapidPay.Service.Services
{
    public interface IPaymentService
    {
        Task<PaymentFeeDto> Save(PaymentDto payment);
    }
}
