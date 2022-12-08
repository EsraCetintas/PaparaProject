using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos
{
    public class FlatDto
    {
        public string BlockNo { get; set; }
        public string FloorNo { get; set; }
        public string FlatNo { get; set; }
        public bool FlatState { get; set; }

        public UserDto UserDto { get; set; }
        public FlatTypeDto FlatTypeDto { get; set; }
    }
}
