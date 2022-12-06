using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Domain.Entities
{
    public class Flat : BaseEntity
    {
        public int UserId { get; set; }
        public int FlatTypeId { get; set; }
        public string BlockNo { get; set; }
        public string FloorNo { get; set; }
        public string FlatNo { get; set; }
        public bool FlatState { get; set; }
        public User User { get; set; }
        public FlatType FlatType { get; set; }
    }
}
