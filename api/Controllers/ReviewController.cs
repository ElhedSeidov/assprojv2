using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Review;
using api.Extensions;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/review")]
    [ApiController]
    public class ReviewController:ControllerBase
    {
        private readonly IReviewRepository _reviewRepo;
        private readonly IPhoneRepository _phoneRepo;

        

        private readonly UserManager<Buyer> _userManager;
        public ReviewController(IReviewRepository reviewRepo ,IPhoneRepository phoneRepo ,UserManager<Buyer> userManager)
        {
            _reviewRepo=reviewRepo;
            _phoneRepo=phoneRepo;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviews= await _reviewRepo.GetAllAsync();

            var reviewDto = reviews.Select(s => s.ToReviewDTO());

            return Ok(reviewDto);
        }
        [HttpGet ("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var review = await _reviewRepo.GetByIdAsync(id);

            if(review==null)
            {
                return NotFound();
            }

            return Ok(review.ToReviewDTO());
        }

        [HttpPost ("{phoneId:int}")]
        public async Task<IActionResult> Create([FromRoute] int phoneId ,CreateReviewDTO reviewDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(!await _phoneRepo.PhoneExist(phoneId))
            {
                return BadRequest("Phone Does Not Exist");
            }

           var username = User.GetUsername();
           var buyer = await _userManager.FindByNameAsync(username);

            var reviewModel=reviewDTO.ToReviewFromCreate(phoneId);
            
            reviewModel.BuyerId = buyer.Id;
            await _reviewRepo.CreateAsync(reviewModel);
            return CreatedAtAction(nameof(GetById),new {id=reviewModel.Id} ,reviewModel.ToReviewDTO() ) ;
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateReviewDTO updateDto)
        {
             var username = User.GetUsername();          
             var reviewget = await _reviewRepo.GetByIdAsync(id);
             var reviewusname=  reviewget.Buyer.UserName;

             if(username!=reviewusname)
             {
                return  BadRequest("You cannot change another peoples review");
             }
            


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

             var review = await _reviewRepo.UpdateAsync(id, updateDto);

            if (review == null)
            {
                return NotFound("Review not found");
            }

            return Ok(review.ToReviewDTO());
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewModel = await _reviewRepo.DeleteAsync(id);

            if (reviewModel == null)
            {
                return NotFound("Review does not exist");
            }

            return Ok(reviewModel);
        }

    }
}