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
    public class UserLikesRepository:IUserLikesRepository
    {
        private readonly ApplicationDBContext _context;
        public UserLikesRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<UserLikes> CreateAsync(UserLikes userlike ,string symbol)
        {
            var phone=_context.Phones.FirstOrDefault(s=>s.Symbol==symbol);
            await _context.UserLikes.AddAsync(userlike);
            phone.Likes++;

            await _context.SaveChangesAsync();
            return userlike;
        }

        public async Task<List<Phone>> GetUserLikes(Buyer buyer)
        {

            return await _context.UserLikes.Where(u => u.BuyerId == buyer.Id)
            .Select(phone=> new Phone
            {
                Id = phone.PhoneId,
                Symbol = phone.Phone.Symbol,
                Model = phone.Phone.Model,
                Serialnumber = phone.Phone.Serialnumber,
                Likes=phone.Phone.Likes
                
                
            }).ToListAsync();
        }

         public async Task<UserLikes> DeleteUserLike(Buyer buyer, string symbol)
        {
             var phone=_context.Phones.FirstOrDefault(s=>s.Symbol==symbol);
            var userlikeModel = await _context.UserLikes.FirstOrDefaultAsync(x => x.BuyerId == buyer.Id && x.Phone.Symbol.ToLower() == symbol.ToLower());

            if (userlikeModel == null)
            {
                return null;
            }

            _context.UserLikes.Remove(userlikeModel);
            phone.Likes--;

            await _context.SaveChangesAsync();
            return userlikeModel;
        }

    }
}