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

        [Required]
        [FutureDate]
        public DateTime DateTime { get; set; }

        [Required]
        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

    }
}