using DbModels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbModels.Models
{
    public class PageTemplate
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Html { get; set; }

        public ICollection<NewPage> Pages { get; set; }
    }
}
