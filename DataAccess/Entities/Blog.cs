#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Blog : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string DinosaurName { get; set; }

        [Required]
        [StringLength(500)]
        public string Features { get; set; }

        public decimal? Score { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public List<BlogTag> BlogTags { get; set; }
    }
}
