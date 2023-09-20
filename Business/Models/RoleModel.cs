#nullable disable

using AppCore.Records.Bases;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class RoleModel : RecordBase
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }
    }
}
