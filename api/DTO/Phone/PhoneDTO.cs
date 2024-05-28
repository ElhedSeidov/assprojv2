using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class PhoneDTO
    {
        public int Id { get; set; }
        
        public string Symbol {get;set;}=string.Empty;
        public string Model { get; set; } = string.Empty;


        public int Serialnumber {get;set;}

        public List<PhoneCharDTO>  PhoneChar {get;set;}
        public List<ReviewDTO> Reviews { get; set; } 

        public int Likes { get; set; }
    }
}