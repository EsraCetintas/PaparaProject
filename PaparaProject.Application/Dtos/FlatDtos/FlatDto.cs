using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaparaProject.Application.Dtos.FlatTypeDtos;
using PaparaProject.Application.Dtos.UserDtos;

namespace PaparaProject.Application.Dtos.FlatDtos
{
    public class FlatDto
    {
        public string BlockNo { get; set; }
        public string FloorNo { get; set; }
        public string FlatNo { get; set; }
        public bool FlatState { get; set; }

        public FlatTypeDto FlatType { get; set; }
    }
}
