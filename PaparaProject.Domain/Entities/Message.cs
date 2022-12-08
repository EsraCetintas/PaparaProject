using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Domain.Entities
{
    public class Message : BaseEntity
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public bool IsReaded { get; set; }
        public bool IsNew { get; set; }

        public User User { get; set; }
    }
}
