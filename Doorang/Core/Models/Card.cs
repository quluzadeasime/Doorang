using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Card:BaseEntity
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Title { get; set; } = null!;
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Subtitle { get; set; } = null!;
        [Required]
        [MinLength(10)]
        [MaxLength(150)]
        public string Description { get; set; } = null!;
        
        public string? ImgUrl { get; set; } = null!;
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }
    }
}
