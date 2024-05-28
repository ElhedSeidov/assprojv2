using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTO.Phone;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/phone")]
    [ApiController]
    public class PhoneController:ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IPhoneRepository _phoneRepo;
        public PhoneController(ApplicationDBContext context,IPhoneRepository phoneRepo)
        {
            _phoneRepo=phoneRepo;
            _context=context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var phones = await _phoneRepo.GetAllAsync();

            var PhoneDTO = phones.Select(s => s.ToPhoneDTO());

            return Ok(PhoneDTO);
        }

        [HttpGet ("{id:int}")]
       
        public async Task< IActionResult > GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock =  await  _phoneRepo.GetByIdAsync(id);


            if (stock==null)
            {
                return NotFound();
            }

            return Ok(stock.ToPhoneDTO());

        }



        [HttpPost]
        public async Task < IActionResult > Create([FromBody] CreatePhoneDTO phoneDTO)
        {

            if(await _context.Phones.AnyAsync(x=>x.Symbol==phoneDTO.Symbol)==true )
            {
                 return BadRequest("Phone With This Symbol exists ,Please Write Unique one");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var  phoneModel =phoneDTO.ToPhoneFromCreateDTO();
            await _phoneRepo.CreateAsync(phoneModel);
            return CreatedAtAction(nameof(GetById), new { id = phoneModel.Id }, phoneModel.ToPhoneDTO());


         }

        [HttpPut]
        [Route("{id:int}")]

        
        public async Task < IActionResult> Update([FromRoute] int id,[FromBody] UpdatePhoneDTO updateDto ) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel =  await _phoneRepo.UpdateAsync(id , updateDto);

            if (stockModel==null)
            {
                return NotFound();
            }
            

            return Ok(stockModel.ToPhoneDTO());

        }
        
        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles="Admin")] 
        public async Task <IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var phoneModel = await _phoneRepo.DeleteAsync(id);

             if (phoneModel==null)
            {
                return NotFound();
            }

            return NoContent();
        }
}
}