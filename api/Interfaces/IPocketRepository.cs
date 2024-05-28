using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPocketRepository
    {
         Task<List<Phone>> GetBuyerPocket(Buyer buyer);

         Task<Pocket> CreateAsync(Pocket pocket);

         Task<Pocket> DeletePocket(Buyer buyer, string symbol);
    }
}