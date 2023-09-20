#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Tag : RecordBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public List<BlogTag> BlogTags { get; set; }
    }
}
