using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class GigViewModel
    {
        [Required]
        public string Venue { get; set; }

        [Required(ErrorMessage = "Date should be in future")]
        [FutureDate]
        public DateTime DateTime { get; set; }

        [Required]
        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

    }
}