﻿using System;
using System.ComponentModel.DataAnnotations;

namespace MarktGuruTask.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }

        public Product()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
}
