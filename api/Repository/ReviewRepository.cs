using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Review;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ReviewRepository:IReviewRepository
    {
        private readonly ApplicationDBContext _context;
        public ReviewRepository(ApplicationDBContext context)
        {
            _context=context;
        }

        public async Task<Review> CreateAsync(Review reviewModel)
        {
            await _context.Reviews.AddAsync(reviewModel);
            await _context.SaveChangesAsync();
            return reviewModel;
        }

        public async Task<Review?> DeleteAsync(int id)
        {
            var commentModel = await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
            {
                return null;
            }

            _context.Reviews.Remove(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.Include(b=>b.Buyer).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.Include(b=>b.Buyer).FirstOrDefaultAsync(c => c.Id == id);

        }

        public async Task<Review?> UpdateAsync(int id, UpdateReviewDTO reviewDTO)
        {
            var existingReview = await _context.Reviews.FindAsync(id);

            if (existingReview == null)
            {
                return null;
            }

            existingReview.Title = reviewDTO.Title;
            existingReview.Content = reviewDTO.Content;

            await _context.SaveChangesAsync();

            return existingReview;
        }
    }
}