﻿using SuperHeroAPI.Entities.User;

namespace SuperHeroAPI.DataAccess
{
    public class UserRepository
    {
        private DataContext _dbContext;
        public UserRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public UserDto InsertUser(User user)
        {
            var result = _dbContext.Users.Add(user);
            var db = _dbContext.SaveChanges();
            if( db > 0)
            {
                UserDto userResult = new UserDto()
                {
                    Name = user.Name,
                    Id = user.Id,
                    Username = user.Username,
                    Place = user.Place
                };
                return userResult;
            }
            else
            {
                return new UserDto();
            }
        }
    }
}
