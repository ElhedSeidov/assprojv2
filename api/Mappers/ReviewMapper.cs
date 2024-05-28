using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.DTO.Review;
using api.Models;

namespace api.Mappers
{
    public static class ReviewMapper
    {
       public static ReviewDTO ToReviewDTO(this Review reviewModel)
       {

        return new ReviewDTO
        {
            Id=reviewModel.Id,
            Title=reviewModel.Title,
            Content=reviewModel.Content,
            CreatedOn=reviewModel.CreatedOn,
            CreatedBy=reviewModel.Buyer,
            PhoneId=reviewModel.PhoneId

        };

       }

        public static Review ToReviewFromCreate(this CreateReviewDTO  reviewDTO,int phoneId)
        {
            return new Review
            {
                Title = reviewDTO.Title,
                Content = reviewDTO.Content,
                PhoneId=phoneId
            };
        }

         public static Review ToReviewFromUodate(this UpdateReviewDTO  reviewDTO,int phoneId)
        {
            return new Review
            {
                Title = reviewDTO.Title,
                Content = reviewDTO.Content,
                PhoneId=phoneId
            };
        }
    }

}