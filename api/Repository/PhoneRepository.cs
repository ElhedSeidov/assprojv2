using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Phone;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PhoneRepository:IPhoneRepository
    {

        private readonly ApplicationDBContext _context;
        
        public PhoneRepository(ApplicationDBContext context )
        {
            
            _context=context;
        }
         public async Task<Phone> CreateAsync(Phone phoneModel)
        {
            await _context.Phones.AddAsync(phoneModel);
            await _context.SaveChangesAsync();
            return phoneModel;

        }

        public async Task<List<Phone>> GetAllAsync()
        {
           return await _context.Phones.Include(c=>c.PhoneChar).Include(c=>c.Reviews).ThenInclude(a=>a.Buyer).ToListAsync();
        }

        

        public async Task<Phone?> GetByIdAsync(int id)
        {
            return await _context.Phones.Include(c=>c.PhoneChar).Include(c=>c.Reviews).ThenInclude(a=>a.Buyer).FirstOrDefaultAsync(i=>i.Id==id);
        }

        public async Task<Phone?> UpdateAsync(int id, UpdatePhoneDTO phoneDTO)
        {
            var existingPhone = await _context.Phones.FirstOrDefaultAsync(x => x.Id == id);
             if (existingPhone == null)
            {
                return null;
            }

            existingPhone.Symbol=phoneDTO.Symbol;
            existingPhone.Model=phoneDTO.Model;
            existingPhone.Serialnumber=phoneDTO.Serialnumber;


            await _context.SaveChangesAsync();
            return existingPhone;         
        }

        public Task<bool> PhoneExist(int id)
        {
            return _context.Phones.AnyAsync(s=>s.Id==id);
        }

         public async Task<Phone?> DeleteAsync(int id)
        {
            var phoneModel = await _context.Phones.Include(c=>c.PhoneChar).Include(b=>b.Reviews).FirstOrDefaultAsync(x => x.Id == id);

             if (phoneModel == null)
            {
                return null;
            }

            _context.Phones.Remove(phoneModel);
            await _context.SaveChangesAsync();
            return phoneModel;
        }

        public async Task<Phone?> GetBySymbolAsync(string symbol)
        {
            return await _context.Phones.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }
    }
}