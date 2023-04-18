using SuperHeroAPI.Entities.User;

namespace SuperHeroAPI.AppService.User
{
    public interface IUserAppservice
    {
        UserDto RegisterUser(Data.User user);
        string LoginUser(Data.User user);
    }
}
