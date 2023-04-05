using SuperHeroAPI.AppService.User;
using SuperHeroAPI.DataAccess;
using SuperHeroAPI.Entities.User;

namespace SuperHeroAPI.AppService
{
    public class UserAppService: IUserAppservice
    {
        private UserRepository _userRepository;
        public UserAppService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserDto RegisterUser(Data.User user)
        {
            user.Created_At = DateTime.UtcNow;
            user.Created_By = "System";
            var result = _userRepository.InsertUser(user);
            return result;
        }
    }
}
