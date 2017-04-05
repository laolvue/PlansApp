using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Postal;
using System.ComponentModel.DataAnnotations;

namespace PlansApp.Models
{
    public class CheckInEmailViewModel 
    {
        [Key]
        public int plansEmailId { get; set; }

        public string Location { get; set; }
        public string introMessage { get; set; }
        public string closingMessage { get; set; }
        public DateTime planDate { get; set; }

        public List<Recipient> recipients { get; set; }

        public string sendingUserName { get; set; }

        public string sendingUserEmailAddress { get; set; }

        public DateTime deadline { get; set; }
    }
}