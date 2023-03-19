using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using signalr.demo.Data;

namespace signalr.demo.Controllers
{
    [Route("[controller]")]
    public class TokenController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IConfiguration config;
        
        public TokenController(SignInManager<ApplicationUser> signInManager, IConfiguration config)
        {
            this.signInManager = signInManager;
            this.config = config;
        }
        
        [HttpGet("api/token")]
        [Authorize]
        public IActionResult GetToken()
        {
            return Ok(GenerateToken(User.Identity.Name));
        }
        
        private string GenerateToken(string userId)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(config["JwtKey"]));
            
            var claims = new[]
            { 
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("signalrdemo", "signalrdemo", claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    
    public class LoginRequest
    { 
        public string Username { get; set; }
        public string Password { get; set; }
    }
}