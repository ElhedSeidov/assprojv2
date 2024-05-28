using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO;
using api.DTO.PhoneChar;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/phonecharRepo")]
    [ApiController]
    public class PhoneCharController:ControllerBase
    {

        private readonly IPhoneCharRepository _phonecharRepo;
        private readonly IPhoneRepository _phoneRepo;

        public PhoneCharController(IPhoneCharRepository phonecharRepo,IPhoneRepository phoneRepo)
        {
            _phonecharRepo=phonecharRepo;
            _phoneRepo=phoneRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var phonechar = await _phonecharRepo.GetAsync();

            var phonecharDto = phonechar.Select(s => s.ToPhoneCharDTO());

            return Ok(phonecharDto);
        }

        [HttpGet ("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var phonechar = await _phonecharRepo.GetByIdAsync(id);

            if(phonechar==null)
            {
                return NotFound();
            }

            return Ok(phonechar.ToPhoneCharDTO());
        }

        
        [HttpPost ("{phoneId:int}")]
        public async Task<IActionResult> Create([FromRoute] int phoneId ,PhoneCharCreateDTO phonecharDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(await _phonecharRepo.PhoneCharExist(phoneId))
            {
                return BadRequest("You cannot add more than one phone char in phone");
            }


            if(!await _phoneRepo.PhoneExist(phoneId))
            {
                return BadRequest("Phone Does Not Exist");
            }


             var phonecharModel=phonecharDto.ToPhoneCharFromCreateDTO(phoneId);

             await _phonecharRepo.CreateAsync(phonecharModel);

              return CreatedAtAction(nameof(GetById),new {id=phonecharModel.Id} ,phonecharModel.ToPhoneCharDTO() ) ;
        }

       
        [HttpPut("{phoneId:int}")]
        public async Task<IActionResult> Update([FromRoute] int phoneId, [FromBody] PhoneCharUpdateDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

             var phonechar= await _phonecharRepo.UpdateAsync(phoneId,updateDTO);

            if (phonechar == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(phonechar.ToPhoneCharDTO());
        }

        [HttpDelete("{phoneId:int}")]
       
        public async Task<IActionResult> Delete([FromRoute] int phoneId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentModel = await _phonecharRepo.DeleteAsync(phoneId);

            if (commentModel == null)
            {
                return NotFound("Comment does not exist");
            }

            return Ok(commentModel);
        }


    }

}