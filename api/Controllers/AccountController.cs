using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        

        private readonly UserManager<Buyer> _userManager;
        private readonly ITokenService _tokenService;

        private readonly SignInManager<Buyer> _signinManager;

        public AccountController(UserManager<Buyer> userManager ,ITokenService tokenService,SignInManager<Buyer> signInManager)
        {
                _userManager=userManager;
                _tokenService=tokenService;
                _signinManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try 
            {
                if(!ModelState.IsValid)
                 return BadRequest(ModelState);

                 var buyer=new Buyer
                 {
                    UserName=registerDto.UserName,
                    Email=registerDto.Email   
                 };

                 var createdUser=await _userManager.CreateAsync(buyer,registerDto.Password);
                 var roles=await _userManager.GetRolesAsync(buyer);

                 if (createdUser.Succeeded)
                 {
                    var roleResult = await _userManager.AddToRoleAsync(buyer,"Buyer");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName=buyer.UserName,
                                Email=buyer.Email,
                                Role = string.Join("," ,roles),
                                Token=_tokenService.CreateToken(buyer,roles)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500 ,roleResult.Errors);
                    }
                 }
                 else
                 {
                    return StatusCode(500,createdUser.Errors);
                 }

            }
            catch(Exception e)
            {
                return StatusCode(500,e);
            }

        }



        [HttpPost("registeradmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto registerDto)
        {
            try 
            {
                if(!ModelState.IsValid)
                 return BadRequest(ModelState);

                 var buyer=new Buyer
                 {
                    UserName=registerDto.UserName,
                    Email=registerDto.Email   
                 };

                 var createdUser=await _userManager.CreateAsync(buyer,registerDto.Password);
                 var roles=await _userManager.GetRolesAsync(buyer);

                 if (createdUser.Succeeded)
                 {
                    var roleResult = await _userManager.AddToRoleAsync(buyer,"Admin");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName=buyer.UserName,
                                Email=buyer.Email,
                                Role = string.Join("," ,roles),
                                Token=_tokenService.CreateToken(buyer,roles)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500 ,roleResult.Errors);
                    }
                 }
                 else
                 {
                    return StatusCode(500,createdUser.Errors);
                 }

            }
            catch(Exception e)
            {
                return StatusCode(500,e);
            }

        }


        [HttpPost("registerreviewer")]
        public async Task<IActionResult> RegisterReviewer([FromBody] RegisterDto registerDto)
        {
            try 
            {
                if(!ModelState.IsValid)
                 return BadRequest(ModelState);

                 var buyer=new Buyer
                 {
                    UserName=registerDto.UserName,
                    Email=registerDto.Email   
                 };

                 var createdUser=await _userManager.CreateAsync(buyer,registerDto.Password);
                 var roles=await _userManager.GetRolesAsync(buyer);

                 if (createdUser.Succeeded)
                 {
                    var roleResult = await _userManager.AddToRoleAsync(buyer,"Reviewer");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName=buyer.UserName,
                                Email=buyer.Email,
                                Role = string.Join("," ,roles),
                                Token=_tokenService.CreateToken(buyer,roles)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500 ,roleResult.Errors);
                    }
                 }
                 else
                 {
                    return StatusCode(500,createdUser.Errors);
                 }

            }
            catch(Exception e)
            {
                return StatusCode(500,e);
            }

        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {


               try 
               {
                if(!ModelState.IsValid)
                 return BadRequest(ModelState);

                var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.UserName==loginDto.UserName.ToLower());

                if (user==null ) return Unauthorized("Invalid User name");

                var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

                 if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

                 var roles = await _userManager.GetRolesAsync(user);

                 

                return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Role = string.Join("," ,roles),
                    Token = _tokenService.CreateToken(user,roles)
                }
                );

               }
               catch(Exception e)
               {
                return StatusCode(500,e);
                
                
               }
            
        }


    }
}