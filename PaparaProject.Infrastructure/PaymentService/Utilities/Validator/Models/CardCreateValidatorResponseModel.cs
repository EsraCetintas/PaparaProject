using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Utilities.Validator.Models
{
    public class CardCreateValidatorResponseModel
    {
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}
