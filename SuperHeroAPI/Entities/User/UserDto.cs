using System;
namespace SuperHeroAPI.Entities.User
{
	public class UserDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Place { get; set; }
		public string Username { get; set; } = string.Empty;

		public UserDto()
		{
		}
	}
}

