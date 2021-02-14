using System.ComponentModel.DataAnnotations;

namespace carShop.Dtos
{
    public abstract class CarManipulationDto
    {
        [Required]
        [MaxLength(50)]
        public string ModelName { get; set; }

        [Required]
        [MaxLength(50)]
        public string OwnerName { get; set; }

        [Required]
        public double MaxSpeed { get; set; }

        [Required]
        public double WorkTime { get; set; }

        [Required]
        public double Price { get; set; }
    }
}