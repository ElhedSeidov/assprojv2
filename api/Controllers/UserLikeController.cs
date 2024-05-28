using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/userlike")]
    [ApiController]

    public class UserLikeController:ControllerBase
    {
        private readonly UserManager<Buyer> _userManager;
        private readonly IPhoneRepository _phoneRepo;
        private readonly IUserLikesRepository _userlikeRepo; 
        public UserLikeController(UserManager<Buyer> userManager,
        IPhoneRepository phoneRepo,IUserLikesRepository userlikeRepo)

        {
            _userManager = userManager;
            _phoneRepo = phoneRepo;
            _userlikeRepo=userlikeRepo;
        }

        [HttpGet]
        [Authorize]
         public async Task<IActionResult> GetUserLiked()
        {
            var username = User.GetUsername();
            var buyer = await _userManager.FindByNameAsync(username);
            var userlikes = await _userlikeRepo.GetUserLikes(buyer);
            return Ok(userlikes);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikeStock(string symbol)
        {
            var username = User.GetUsername();
            var buyer = await _userManager.FindByNameAsync(username);
            var stock = await _phoneRepo.GetBySymbolAsync(symbol);

            if (stock == null) return BadRequest("Phone not found");

            var userLike = await _userlikeRepo.GetUserLikes(buyer);

            if (userLike.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot Like Same Phone");

              var userlikeModel = new UserLikes
            {
                PhoneId = stock.Id,
                BuyerId = buyer.Id
            };

            await _userlikeRepo.CreateAsync(userlikeModel,symbol);
            

            if (userlikeModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                
                return Created();
            }

         }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var buyer = await _userManager.FindByNameAsync(username);

            var phone = await _phoneRepo.GetBySymbolAsync(symbol);

            var userLike = await _userlikeRepo.GetUserLikes(buyer);

            var filteredPhone = userLike.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredPhone.Count() == 1)
            {
                await _userlikeRepo.DeleteUserLike(buyer, symbol);
                
                
            }
            else
            {
                return BadRequest("You did not like it");
            }

            return Ok();
        }



    }
}