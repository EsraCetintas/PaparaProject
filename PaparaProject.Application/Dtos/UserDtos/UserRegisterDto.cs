using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.UserDtos
{
    public class UserRegisterDto
    {
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
    }
}
