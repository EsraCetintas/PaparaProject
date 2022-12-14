using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Utilities.Result
{
    public class CardServiceResult
    {
        public bool Result { get; private set; }
        public string Message { get; private set; }

        public CardServiceResult(bool result, string message)
        {
            this.Result = result;
            this.Message = message;
        }   
    }
}
