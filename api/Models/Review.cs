using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? PhoneId { get; set; }

        public Phone?  Phone { get; set; }

        public string BuyerId { get; set; }
        public Buyer Buyer { get; set; }



    }
}