using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarktGuruTask.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
