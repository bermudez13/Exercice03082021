using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercice03082021.Controllers.Resources
{
    public class OrderResource
    {
        public long UserID { get; set; }

        public long ProductID { get; set; }

        [Required]
        [StringLength(10)]
        public string Date { get; set; }

        public string Status { get; set; }

        public int Amount { get; set; }
    }
}