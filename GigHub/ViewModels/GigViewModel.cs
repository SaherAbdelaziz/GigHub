using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHub.ViewModels
{
    public class GigViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required(ErrorMessage = "Date should be in future")]
        //   [FutureDate]
        public DateTime DateTime { get; set; }

        [Required]
        public int Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action => (Id == 0) ? "Create" : "Update";

    }
}