using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Dtos.FlatDtos
{
    public class FlatUpdateDto
    {
        public int FlatTypeId { get; set; }
        public string BlockNo { get; set; }
        public string FloorNo { get; set; }
        public string FlatNo { get; set; }
        public bool FlatState { get; set; }
    }
}
