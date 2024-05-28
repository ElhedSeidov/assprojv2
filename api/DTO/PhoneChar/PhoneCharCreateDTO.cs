using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.PhoneChar
{
    public class PhoneCharCreateDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="Min length is 10")]
        [MaxLength(25,ErrorMessage ="Max Length is 25")]
        public string Color {get;set;}

        [Required]
        [Range(3,200 ,ErrorMessage ="Range between 3 and 200")]
        public decimal CameraQuality{get;set;}

        [Required]
        [Range(3,200 ,ErrorMessage ="Range between 3 and 200")]

        public decimal HZ { get; set; }
    }
}