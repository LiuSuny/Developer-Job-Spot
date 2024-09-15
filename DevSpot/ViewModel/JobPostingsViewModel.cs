using System.ComponentModel.DataAnnotations;

namespace DevSpot.ViewModel
{
    public class JobPostingsViewModel
    {

       
        [Required]
        public string title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Company { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;

    }
}
