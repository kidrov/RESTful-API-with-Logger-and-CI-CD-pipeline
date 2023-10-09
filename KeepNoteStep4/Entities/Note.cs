using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;



namespace Entities
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }

        [Required]
        public string? NoteTitle { get; set; }

        public string? NoteContent { get; set; }

        public string? NoteStatus { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }

        [ForeignKey("ReminderId")]
        public int? ReminderId { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        [JsonIgnore]
        public Reminder? Reminder { get; set; }

        public int CreatedBy { get; set; }
        [Required]
        [ForeignKey("CreatedBy")]
        

        [JsonIgnore]
        public User? User { get; set; }
    }
}

