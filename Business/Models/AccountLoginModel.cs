#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class AccountLoginModel
    {
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(20, ErrorMessage = "{0} must be maximum {1} characters!")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(20, ErrorMessage = "{0} must be maximum {1} characters!")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
