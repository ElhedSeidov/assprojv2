using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IUserLikesRepository
    {
         Task<List<Phone>> GetUserLikes(Buyer buyer);

         Task<UserLikes> CreateAsync(UserLikes userlikes,string symbol);

         Task<UserLikes> DeleteUserLike(Buyer buyer, string symbol);
    }
}