using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace MVCstore.Models
{
    public class Customers
    {
        [Required]
        [MaxLength(10, ErrorMessage ="I don't read that much")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(10, ErrorMessage ="I don't read that much")]
        public string LastName { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerNumber { get; set; }

        [Required]
        [RegularExpression ("[a-z0-9._%+-]+@[a-z0-9.-]+.[a-z]{2,4}$",ErrorMessage = "what have i asked? a frikin email?")]
        public string Email { get; set; }

       
        public string PhoneNumber { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Hash function to hash code the password of the user, using MD5 algorithm
        /// </summary>
        /// <param name="password"></param>
        /// <returns>ToBase64String of hashBytes</returns>
        public void MD5Hash()
        {
            var bytes = new UTF8Encoding().GetBytes(PasswordHash);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            this.PasswordHash = Convert.ToBase64String(hashBytes);
        }
    }
}