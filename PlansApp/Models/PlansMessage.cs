using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlansApp.Models
{
    public class PlansMessage
    {
        [Key]
        public int plansMessageId { get; set; }
        [ForeignKey("RecipientCategory")]
        public int recipientCategoryId { get; set; }
        public RecipientCategory RecipientCategory { get; set; }

        [ForeignKey("Recipient")]
        public int recipientId { get; set; }
        public Recipient Recipient { get; set; }
        public string Location { get; set; }
        public string introMessage { get; set; }
        public string closingMessage { get; set; }
    }
}