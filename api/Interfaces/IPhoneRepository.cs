using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Phone;
using api.Models;

namespace api.Interfaces
{
    public interface IPhoneRepository
    {
        Task<Phone> CreateAsync(Phone phoneModel);
        Task<List<Phone>> GetAllAsync();

        Task<Phone?> GetBySymbolAsync(string symbol);

        Task<Phone?> GetByIdAsync(int id);

        Task<Phone?> DeleteAsync(int id);

        Task<Phone?> UpdateAsync(int id, UpdatePhoneDTO phoneDTO);

         Task<bool> PhoneExist(int id);


       
    }
}