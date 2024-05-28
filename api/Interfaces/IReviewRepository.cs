using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Review;
using api.Models;

namespace api.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(int id);
        Task<Review> CreateAsync(Review reviewModel);
        Task<Review?> UpdateAsync(int id, UpdateReviewDTO reviewDTO);
        Task<Review?> DeleteAsync(int id);
    }
}