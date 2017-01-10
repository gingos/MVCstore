using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCstore.Models
{
    public class Register
    {
        [Required]
        [MaxLength(10, ErrorMessage ="I don't read that much")]
        public string firstName { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage ="I don't read that much")]
        public string lastName { get; set; }


        public int customerNum { get; set; }

        [Required]
        [RegularExpression ("[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$",ErrorMessage = "what have i asked? a frikin email?")]
        public string customerEmail { get; set; }

        [Required]
        [RegularExpression("[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$",ErrorMessage = "what have i asked? a frikin email?")]
        [Compare("customerEmail",ErrorMessage ="same same!")]
        public string customerEmailConfirmation { get; set; }

        [RegularExpression("[05+0-9]",ErrorMessage ="please enter the prostitutes phone")]
        public string customerPhone { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        [Compare("password",ErrorMessage ="same password please")]
        public string passwordConfirm { get; set; }
    }
}