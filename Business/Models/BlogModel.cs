#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class BlogModel : RecordBase
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(100)]
        [DisplayName("Name")]
        public string DinosaurName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(500)]
        public string Features { get; set; }

        [Range(1, 5, ErrorMessage = "{0} must be between {1} and {2}!")]
        public decimal? Score { get; set; }

        [DisplayName("User")]
        [Required(ErrorMessage = "{0} is required!")]
        public int? UserId { get; set; }

        [DisplayName("User Name")]
        public string UserNameDisplay { get; set; }

        [DisplayName("Tags")]
        public List<TagModel> TagsDisplay { get; set; }

        [DisplayName("Tags")]
        [Required(ErrorMessage = "{0} are required!")]
        public List<int> TagIds { get; set; }
    }
}
