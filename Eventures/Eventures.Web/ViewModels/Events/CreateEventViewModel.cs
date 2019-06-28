namespace Eventures.Web.ViewModels.Events
{
    using Eventures.Common;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateEventViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [MinLength(GlobalConstants.EVENT_NAME_MIN_LENGTH)]
        [MaxLength(GlobalConstants.EVENT_NAME_MAX_LENGTH)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [Display(Name = "Place")]
        public string Place { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Start")]
        public DateTime Start { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "End")]
        public DateTime End { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Total Tickets")]
        public int TotalTickets { get; set; }

        [Required]
        [Range(typeof(decimal), "0", "79228162514264337593543950335")]
        [Display(Name = "Price Per Ticket")]
        public decimal PricePerTicket { get; set; }
    }
}
