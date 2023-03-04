using DbModels.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DbModels.Models
{
    public class PageComponent
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Html { get; set; }

        public int PageId { get; set; }

        public NewPage Page { get; set; }

        public ICollection<PageStyle> PageStyles { get; set; }
    }
}
