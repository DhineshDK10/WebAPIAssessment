using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CRUDwebapi.Models;

namespace CRUDwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly MovieDBContext _context;



        public TokenController(IConfiguration config, MovieDBContext context)
        {
            _configuration = config;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserInformation userData)
        {



            if (userData != null && userData.Email != null && userData.Password != null)
            {
                var user = await GetUser(userData.Email, userData.Password);



                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("FirstName", user.Firstname),
                    new Claim("LastName", user.Lastname),
                    new Claim("Email", user.Email),
                    };



                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));



                    var LogIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: LogIn);



                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        private async Task<UserInformation> GetUser(string email, string password)
        {
            UserInformation userInfo = null;
            var result = await _context.userinformation.Where(u => u.Email == email && u.Password == password).ToListAsync();
            if (result.Count > 0)
                userInfo = result[0];
            return userInfo;



        }
    }
}
