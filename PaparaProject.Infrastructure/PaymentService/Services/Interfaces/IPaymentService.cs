using MongoDB.Bson;
using PaparaProject.Infrastructure.PaymentService.Dtos.PaymentDtos;
using PaparaProject.Infrastructure.PaymentService.Utilities.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<CardServiceResult> Pay(PaymentPayDto paymentPayDto);

    }
}
