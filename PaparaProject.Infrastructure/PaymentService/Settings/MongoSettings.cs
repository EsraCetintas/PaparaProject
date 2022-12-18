using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.PaymentService.Settings
{
    public static class MongoSettings
    {
        public readonly static string MongoConnection = "mongodb+srv://EsraCetintas:135790@paymentcluster.ri9rjjy.mongodb.net/?retryWrites=true&w=majority";
        public readonly static string Database = "PaymentDb";
    }
}
