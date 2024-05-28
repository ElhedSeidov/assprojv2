using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PocketRepository:IPocketRepository
    {
        private readonly ApplicationDBContext _context;
        public PocketRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Pocket> CreateAsync(Pocket pocket)
        {
            await _context.Pockets.AddAsync(pocket);
            await _context.SaveChangesAsync();
            return pocket;
        }

        public async Task<List<Phone>> GetBuyerPocket(Buyer buyer)
        {

            return await _context.Pockets.Where(u => u.BuyerId == buyer.Id)
            .Select(phone => new Phone
            {
                Id = phone.PhoneId,
                Symbol = phone.Phone.Symbol,
                Model = phone.Phone.Model,
                Serialnumber = phone.Phone.Serialnumber
                
            }).ToListAsync();
        }

          public async Task<Pocket> DeletePocket(Buyer buyer, string symbol)
        {
            var pocketModel = await _context.Pockets.FirstOrDefaultAsync(x => x.BuyerId == buyer.Id && x.Phone.Symbol.ToLower() == symbol.ToLower());

            if (pocketModel == null)
            {
                return null;
            }

            _context.Pockets.Remove(pocketModel);
            await _context.SaveChangesAsync();
            return pocketModel;
        }
    }
}