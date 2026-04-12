using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Models
{
    public class Pitch
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string? Image { get; set; }
     //   [Display(Name = "Product Color")]
       // public string ProductColor { get; set; }
        [Required]
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Pitch Type")]
        [Required]
        public int PitchTypeId { get; set; }
        [ForeignKey("PitchTypeId")]
        public PitchType? PitchType { get; set; }
        [Display(Name = "Special Tag")]
        [Required]
         public int SpecialTagId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; }
       // public int ProductBrandId { get; set; }
        [ForeignKey("SpecialTagId")]
        public virtual SpecialTag? SpecialTag { get; set; }
    //    public  ProductBrand? ProductBrand { get; set; }
     
}
}
