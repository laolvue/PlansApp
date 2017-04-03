using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlansApp.Models
{
    public class Recipient
    {
        [Key]
        public int recipientId { get; set; }
        public string nickName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public string UserEmail { get; set; }

        public string emailAddress { get; set; }        //TODO email address type

        [ForeignKey("RecipientCategory")]
        public int recipientCategoryId { get; set; }
        public RecipientCategory RecipientCategory { get; set; }
    }
}