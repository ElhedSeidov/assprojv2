using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Pocket
    {
        public string BuyerId {get;set;}

        public int PhoneId {get;set;}

        public Buyer Buyer {get;set;}

        public Phone Phone {get;set;}


    }
}