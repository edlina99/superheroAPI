using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using SuperHeroAPI.AppService.User;
using SuperHeroAPI.DataAccess;
using SuperHeroAPI.Entities.User;

namespace SuperHeroAPI.AppService
{
    public class UserAppService: IUserAppservice
    {
        SqlConnection conn;
        private UserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserAppService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string LoginUser(Data.User user)
        {
            UserDto userDto = new UserDto();
            string token = "";
                //string pass = Convert.ToString(user.Password);

            //kat sini tak check password null ke tak
                if (user != null && !string.IsNullOrWhiteSpace(user.Username))
                {
                    var result = _userRepository.GetUser(user.Username, user.Password);
                    if (result != null)
                        {
                             token = CreateToken(result);
                        }
                   
                   
                }
            return token;

            
        }

        public UserDto RegisterUser(Data.User user)
        {
            user.Created_At = DateTime.UtcNow;
            user.Created_By = "System";
            var result = _userRepository.InsertUser(user);
            return result;
        }

        private string CreateToken(Data.User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim (ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));
            //TODO: handle exception

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
