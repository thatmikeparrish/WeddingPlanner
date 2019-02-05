using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingPlanner.Models
{
    public class LineItem
    {
        [Key]
        public int LineItemId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime? Date { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Cost")]
        public double Cost { get; set; }
    }
}
