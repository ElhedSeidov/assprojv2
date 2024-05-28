using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.PhoneChar;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PhoneCharRepository:IPhoneCharRepository
    {
         private readonly ApplicationDBContext _context;
        public PhoneCharRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<PhoneChar> CreateAsync(PhoneChar phonecharModel)
        {
            await _context.PhoneChar.AddAsync(phonecharModel);
            await _context.SaveChangesAsync();
            return phonecharModel;
        }

        public async Task<PhoneChar?> DeleteAsync(int phoneid)
        {
            var phonecharModel = await _context.PhoneChar.FirstOrDefaultAsync(x => x.PhoneId==phoneid);

            if (phonecharModel == null)
            {
                return null;
            }

            _context.PhoneChar.Remove(phonecharModel);
            await _context.SaveChangesAsync();
            return phonecharModel;
        }

        public async Task<List<PhoneChar>> GetAsync()
        {
           return await _context.PhoneChar.ToListAsync();
        }

        public async Task<PhoneChar?> GetByIdAsync(int id)
        {
            return await _context.PhoneChar.FirstOrDefaultAsync(c => c.Id == id);
        }

        public Task<bool> PhoneCharExist(int phoneid)
        {
            return _context.PhoneChar.AnyAsync(x=>x.PhoneId==phoneid);
        }

        public async Task<PhoneChar>? UpdateAsync(int phoneid, PhoneCharUpdateDTO phonechardto)
        {
            var existingPhonechar = await _context.PhoneChar.FirstOrDefaultAsync(x=>x.PhoneId==phoneid);

            if (existingPhonechar == null)
            {
                return null;
            }

            existingPhonechar.Color=phonechardto.Color;
            existingPhonechar.CameraQuality=phonechardto.CameraQuality;
            existingPhonechar.HZ=phonechardto.HZ;


            await _context.SaveChangesAsync();

            return existingPhonechar;
        }
    }
}