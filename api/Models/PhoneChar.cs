using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class PhoneChar
    {
        public int  Id {get;set;}
        public string Color{ get;set;}=string.Empty;

        [Column(TypeName = "decimal(5, 3)")]

        public decimal CameraQuality{get;set;}

        [Column(TypeName = "decimal(5, 3)")]
        public decimal HZ { get; set; }

        public int? PhoneId {get;set;}

        public Phone? Phone{get;set;}



        
    }
}