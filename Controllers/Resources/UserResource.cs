using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercice03082021.Controllers.Resources
{
    public class UserResource
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
        public string Password { get; set; }

        public virtual ICollection<int> Orders {get;set;}

        public UserResource()
        {
            Orders = new Collection<int>();
        }
    }
}