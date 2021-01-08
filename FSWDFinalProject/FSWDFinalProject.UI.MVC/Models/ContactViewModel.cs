using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FSWDFinalProject.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "* Name is Required *")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Email is Required *")]
        public string Email { get; set; }

        public string Subject { get; set; }

        [Required(ErrorMessage = "* Message is Required *")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}