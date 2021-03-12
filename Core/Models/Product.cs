using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Exercice03082021.Core.Models
{
    public class Product
    {
        [Key]
        public long ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        
        public int Amount { get; set; }

        [Required]
        [MaxLength(2083)]
        public string Slug { get; set; }
        public decimal Price { get; set; }
    }
}