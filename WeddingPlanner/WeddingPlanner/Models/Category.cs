using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeddingPlanner.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Display(Name = "Total")]
        public double? Total { get; set; }

        public double? LineItemsTotal { get; set; }

        public double? Balance
        {
            get
            {
                if (LineItemsTotal != 0)
                {
                    return Total - LineItemsTotal;
                }
                else if (Total != 0)
                {
                    return Total;
                } else
                {
                    return 0;
                }
            } 
        }

        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
