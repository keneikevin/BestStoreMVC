using System.ComponentModel.DataAnnotations;

namespace BestStoreMVC.Models
{
    public class ItemDto
    {
        [Required, MaxLength(100)]
        public string Name { get; set; } = "";
        [Required, MaxLength(100)]
        public string Brand { get; set; } = "";
        [Required, MaxLength(100)]
        public string Category { get; set; } = "";
        [Required]
        public string Price { get; set; } = "";
        [Required]
        public string Description { get; set; } = "";
        
    }
}
