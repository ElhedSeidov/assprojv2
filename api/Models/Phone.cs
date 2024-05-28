using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
   
    public class Phone
    {
        public int Id { get; set; }

        public string Symbol {get;set;}=string.Empty;
        public string Model { get; set; } = string.Empty;

        public int Serialnumber {get;set;}

        public List<PhoneChar> PhoneChar { get; set; } = new List<PhoneChar>();

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Pocket> Pockets {get;set;} = new List<Pocket>();

        public List<UserLikes> UserLikes {get;set;} = new List<UserLikes>();

        public int Likes { get; set; }



    
        
        
    }
}