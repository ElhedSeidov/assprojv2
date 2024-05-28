using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.DTO.PhoneChar;
using api.Models;

namespace api.Mappers
{
    public static class PhoneCharMapper
    {
        public static PhoneCharDTO ToPhoneCharDTO(this PhoneChar phonecharModel)
        {
            return new PhoneCharDTO
            {
                Id=phonecharModel.Id,
                Color=phonecharModel.Color,
                CameraQuality=phonecharModel.CameraQuality,
                HZ=phonecharModel.HZ,
                PhoneId=phonecharModel.PhoneId,
                
                
                
                
            };
        }

          public static PhoneChar ToPhoneCharFromCreateDTO(this PhoneCharCreateDTO phonecharModel ,int phoneId)
        {
            return new PhoneChar
            {      
                Color=phonecharModel.Color,
                CameraQuality=phonecharModel.CameraQuality,
                HZ=phonecharModel.HZ,
                PhoneId=phoneId
                
            };
        }

        public static PhoneChar ToPhoneCharFromUpdateDTO(this PhoneCharUpdateDTO phonecharModel ,int phoneId)
        {
            return new PhoneChar
            {      
                Color=phonecharModel.Color,
                CameraQuality=phonecharModel.CameraQuality,
                HZ=phonecharModel.HZ,
                PhoneId=phoneId
                
            };
        }


    }
}