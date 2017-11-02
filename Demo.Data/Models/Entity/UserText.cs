using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.Data.Models.Entity
{
    public class UserText
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTextId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTimeOffset Added { get; set; }
    }
}