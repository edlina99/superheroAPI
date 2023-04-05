using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SuperHeroAPI.AppService;
using SuperHeroAPI.AppService.User;
using SuperHeroAPI.Entities.User;
using SuperHeroAPI.ViewModels;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration configuration;
        private IUserAppservice _userAppService;

        public AuthController (IConfiguration configuration, IUserAppservice userAppService)
        {
            this.configuration = configuration;
            _userAppService = userAppService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResultViewModel>> Register (RegisterViewModel request)
        {
            var result = new ResultViewModel()
            {
                Success = false,
                ErrorCode = "400",
                ErrorDescription = "Something went wrong during registration.",
                Result = null                
            };
            try
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var registerUser = new User()
                {
                    Name = request.Name,
                    Username = request.Username,
                    Password = Encoding.ASCII.GetBytes(request.Password),
                    PasswordSalt = passwordSalt,
                    PasswordHash = passwordHash,
                    Place = request.Place
                };
                var user = _userAppService.RegisterUser(registerUser);
                result.ErrorCode = "200";
                result.ErrorDescription = "";
                result.Success = true;
                result.Result = user;
            }
            catch (Exception ex)
            {
                result.ErrorDescription = $"Type exception: {ex.GetType}. Error Message:{ex.Message}";
            }

            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login (LoginViewModel request)
        {
            //Check username & password in database
            if (user.Username != request.Username)
            {
                return BadRequest("User not found");
            }

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }

            string token = CreateToken(user);

            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                this.configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash (string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
