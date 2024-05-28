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
    [Route("api/pocket")]
    [ApiController]
   
        public class PocketController : ControllerBase
    {
        private readonly UserManager<Buyer> _userManager;
        private readonly IPhoneRepository _phoneRepo;
        private readonly IPocketRepository _pocketRepo;
        public PocketController(UserManager<Buyer> userManager,
        IPhoneRepository phoneRepo,IPocketRepository pocketRepo)

        {
            _userManager = userManager;
            _phoneRepo = phoneRepo;
            _pocketRepo=pocketRepo;
        }


        [HttpGet]
        [Authorize]
         public async Task<IActionResult> GetUserPocket()
        {
            var username = User.GetUsername();
            var buyer = await _userManager.FindByNameAsync(username);
            var userPocket = await _pocketRepo.GetBuyerPocket(buyer);
            return Ok(userPocket);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPocket(string symbol)
        {
            var username = User.GetUsername();
            var buyer = await _userManager.FindByNameAsync(username);
            var phone = await _phoneRepo.GetBySymbolAsync(symbol);

            if (phone == null) return BadRequest("Stock not found");

            var userPocket = await _pocketRepo.GetBuyerPocket(buyer);

            if (userPocket.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same phone to Portfolio");

              var pocketModel = new Pocket
            {
                PhoneId = phone.Id,
                BuyerId = buyer.Id
            };

            await _pocketRepo.CreateAsync(pocketModel);

                        if (pocketModel == null)
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
        public async Task<IActionResult> DeletePocket(string symbol)
        {
            var username = User.GetUsername();
            var buyer = await _userManager.FindByNameAsync(username);

            var userPocket = await _pocketRepo.GetBuyerPocket(buyer);

            var filteredPhone = userPocket.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredPhone.Count() == 1)
            {
                await _pocketRepo.DeletePocket(buyer, symbol);
            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }

            return Ok();
        }
    }
}
