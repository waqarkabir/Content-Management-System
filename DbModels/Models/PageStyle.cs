using DbModels.Models;
using System.ComponentModel.DataAnnotations;

namespace DbModels.Models
{
    public class PageStyle
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Selector { get; set; }

        [Required]
        public string Css { get; set; }

        public int PageId { get; set; }

        public NewPage Page { get; set; }
    }
}
