#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities
{
    public class Role : RecordBase
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
