using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tekhub.Identity.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "* required")]
        [EmailAddress(ErrorMessage = "* valid email address required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* required")]
        [MinLength(6, ErrorMessage = "* minimum 6 letters required")]
        public string Password { get; set; }
    }
}
