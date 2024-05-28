using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO
{
    public class PhoneCharDTO
    {
        public int Id {get;set;}

        public string Color {get;set;}=string.Empty;

        public decimal CameraQuality{get;set;}
        public decimal HZ { get; set; }

        public int? PhoneId {get;set;}
    }
}