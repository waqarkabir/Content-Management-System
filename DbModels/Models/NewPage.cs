using DbModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbModels.Models
{
    public class NewPage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public string Slug { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public int? TemplateId { get; set; }

        public PageTemplate Template { get; set; }

        public ICollection<PageComponent> Components { get; set; }

        public ICollection<PageStyle> Styles { get; set; }
    }
}
