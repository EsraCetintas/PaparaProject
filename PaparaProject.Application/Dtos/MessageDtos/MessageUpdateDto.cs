using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.MessageDtos
{
    public class MessageUpdateDto
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public bool IsReaded { get; set; }
        public bool IsNew { get; set; }
    }
}
