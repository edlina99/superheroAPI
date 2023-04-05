using SuperHeroAPI.Entities.User;

namespace SuperHeroAPI.AppService.User
{
    public interface IUserAppservice
    {
        UserDto RegisterUser(Data.User user);
        bool LoginUser(Data.User user);
    }
}
