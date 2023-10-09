using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        public string? CategoryDescription { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CategoryCreationDate { get; set; } = DateTime.UtcNow;

        public int CategoryCreatedBy { get; set; }
         
        [ForeignKey("CategoryCreatedBy")] // Foreign key to establish relationship with Notes
            
        public User? User { get; set; }
    }
}
