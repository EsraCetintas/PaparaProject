using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaparaProject.Application.Dtos.UserDtos;

namespace PaparaProject.Application.Dtos.MessageDtos
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Context { get; set; }
        public string Sender { get; set; }
        public bool IsReaded { get; set; }
        public bool IsNew { get; set; }
        public DateTime SendDate { get; set; }
        public UserDto User { get; set; }
    }
}
