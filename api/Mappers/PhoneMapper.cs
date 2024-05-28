using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.DTO.Phone;
using api.Models;

namespace api.Mappers
{
    public static class PhoneMapper
    {
        public static PhoneDTO ToPhoneDTO(this Phone phoneModel)
        {
            return new PhoneDTO
            {
                Id=phoneModel.Id,
                Symbol=phoneModel.Symbol,
                Model=phoneModel.Model,
                Serialnumber=phoneModel.Serialnumber,
                Likes=phoneModel.Likes,
                Reviews=phoneModel.Reviews.Select(c=>c.ToReviewDTO()).ToList(),
                PhoneChar=phoneModel.PhoneChar.Select(p=>p.ToPhoneCharDTO()).ToList()              
            };
        }


            public static Phone ToPhoneFromCreateDTO(this CreatePhoneDTO phoneModel)
        {
            return new Phone
            {
                Symbol=phoneModel.Symbol,
                Model=phoneModel.Model,
                Serialnumber=phoneModel.Serialnumber,
                
            };
        }
    }
}