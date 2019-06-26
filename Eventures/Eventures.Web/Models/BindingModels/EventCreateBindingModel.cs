using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventures.Web.Models.BindingModels
{
    public class EventCreateBindingModel
    {
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name = "End")]
        public DateTime End { get; set; }

        [Required]
        [Display(Name = "Total Tickets")]
        [Range(0.00, double.MaxValue, ErrorMessage = "Price per ticket must be a positive number.")]
        public int TotalTickets { get; set; }

        [Required]
        [Display(Name = "Price Per Ticket")]
        [Range(0.00, double.MaxValue,ErrorMessage ="Price per ticket must be a positive number.")]
        public decimal PricePerTicket { get; set; }
    }
}
