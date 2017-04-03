using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlansApp.Models
{
    public class RecipientCategory
    {
        [Key]
        public int recipientCategoryId { get; set; }
        public string categoryName { get; set; }
    }
}