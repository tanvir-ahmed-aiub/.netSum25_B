using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FormSubmission.Models
{
    public class Student
    {
        [Required]
        [Range(1,40)]
        public int? Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is needed")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(500,MinimumLength =20)]
        public string Address { get; set; }
        
    }
}