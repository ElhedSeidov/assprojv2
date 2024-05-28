using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.PhoneChar;
using api.Models;

namespace api.Interfaces
{
    public interface IPhoneCharRepository
    {

        Task<List<PhoneChar>> GetAsync();

        Task<PhoneChar>? GetByIdAsync(int id);
        Task<PhoneChar>CreateAsync(PhoneChar phonecharModel);

        Task<PhoneChar>? UpdateAsync(int phoneid,PhoneCharUpdateDTO phonecharmodel);

        Task<PhoneChar?> DeleteAsync(int id);

        Task<bool> PhoneCharExist(int phoneid);
    }
}