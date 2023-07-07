using Microsoft.AspNetCore.Mvc;
using dotnetAuthApp.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using dotnetAuthApp.Data;
using BCrypt.Net;
namespace dotnetAuthApp.Controllers {
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
    
        public IConfiguration _config;
        public UserController(DataContext context, IConfiguration config) {
            _context = context;
            _config = config;
        }
    
        private readonly DataContext _context;
        [HttpGet("simpleget")]
        [AllowAnonymous]
        public IActionResult simpleGet() {
            return Ok("");
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin) {
            Console.WriteLine("entered Login Route");
            var currentUser = await Authenticate(userLogin);
            //Exit if Authenticate returns null
            if(currentUser == null) {
                return BadRequest("User Not Found");
            }


            //Comparing the http given Password with the DB stored Password
            bool passwordMatch = BCrypt.Net.BCrypt.Verify(userLogin.Password, currentUser.Password);
        

            if(passwordMatch) {
                var token = Generate(currentUser);
                return Ok(token);
            } else {
                return NotFound();
            }
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegister userRegister) {
            Console.WriteLine("Entered REgister");

            if(userRegister == null) {
                return BadRequest("null user");
            } 
            var mailExists = await _context.UserSet.FirstOrDefaultAsync(u => u.Email == userRegister.Email);
            if(mailExists != null) {
                return BadRequest("E-Mail is taken");
            }
            var userExists = await _context.UserSet.FirstOrDefaultAsync(u => u.Username == userRegister.Username);
            if(userExists != null) {
                return BadRequest("Username is taken");
            }
            var userModel = new UserModel();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password, workFactor: 13);
            userModel.Email = userRegister.Email;
            userModel.Username = userRegister.Username;
            userModel.Password = hashedPassword;

            _context.UserSet.Add(userModel);
            await _context.SaveChangesAsync();
            return Ok(await _context.UserSet.ToListAsync());
        }
        [HttpPost]
        public IActionResult Add([FromBody] int userId, int exericseId) {
            
            
            return Ok("");
        }
        
        [HttpGet("personal")]
        [Authorize]
        public IActionResult Personal() {
            var user = HttpContext.User;

            Console.WriteLine(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            return Ok("LoggedIn Only");
        }

        public string Generate(UserModel userModel) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userModel.Username),
                new Claim(ClaimTypes.Email, userModel.Email),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims, 
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        //Possibly NUll
        public async Task<UserModel> Authenticate(UserLogin userLogin) {
            return await _context.UserSet.FirstOrDefaultAsync(u => u.Username == userLogin.Username);
            
        }
    }

    
}