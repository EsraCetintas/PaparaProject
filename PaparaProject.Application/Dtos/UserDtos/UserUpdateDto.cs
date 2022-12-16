using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.UserDtos
{
    public class UserUpdateDto
    {
        public int FlatId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string IdentityNo { get; set; }
        public string EMail { get; set; }
        public string PhoneNumber { get; set; }
        public string? NumberPlate { get; set; }
    }
}
