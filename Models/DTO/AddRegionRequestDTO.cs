using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage ="Should be minimun 3 char")]
        [MaxLength(10, ErrorMessage ="Should be max 10 char")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Should be max 100 char")]

        public string Name { get; set; }
        public string? RegionImageURL { get; set; }
    }
}
