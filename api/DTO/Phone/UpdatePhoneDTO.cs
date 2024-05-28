using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Phone
{
    public class UpdatePhoneDTO
    {
        [Required]
        [MaxLength(3,ErrorMessage ="The length of symbol should not be  than 3")]

        public string Symbol {get;set;}=string.Empty;

        [Required]
        [MinLength(5,ErrorMessage ="The length of Model should not be less than 5")]
        public string Model { get; set; } = string.Empty;

        [Required]
        [Range(100000,999999,ErrorMessage ="Please Put the numbers between 100000 and 999999 inclusively")]


        public int Serialnumber {get;set;}

    }
}