using DbModels.Models;
using System.ComponentModel.DataAnnotations;

namespace DbModels.Models
{
    public class PageImage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Required]
        [StringLength(255)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public int PageId { get; set; }

        public NewPage Page { get; set; }
    }
}
