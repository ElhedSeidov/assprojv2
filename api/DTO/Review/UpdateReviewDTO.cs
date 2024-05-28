using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTO.Review
{
    public class UpdateReviewDTO
    {
        [Required]
        [MinLength(3,ErrorMessage ="The min amount of letters in title is 15")]
        [MaxLength(255, ErrorMessage ="The max amount of Letters in Title is 255" )]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(3,ErrorMessage ="The min amount of letters in content is 15")]
        [MaxLength(255, ErrorMessage ="The max amount of Letters in content is 511" )]
        public string Content { get; set; } = string.Empty;
    }
}