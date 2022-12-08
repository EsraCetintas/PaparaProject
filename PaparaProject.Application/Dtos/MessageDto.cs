using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos
{
    public class MessageDto
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public string Sender { get; set; }
        public bool IsReaded { get; set; }
        public bool IsNew { get; set; }
        public DateTime SendDate { get; set; }
        public UserDto  UserDto { get; set; }
    }
}
