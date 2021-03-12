using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Exercice03082021.Core.Models
{
    public class User
    {
        [Key]
        public long ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }


        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        [Index(IsUnique=true)]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        [Index(IsUnique=true)]
        public string Username { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(255)]
        [JsonIgnore]
        public string Password { get; set; }
        
    }
}