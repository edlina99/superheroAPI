using System;
namespace SuperHeroAPI.Data
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Place { get; set; }
		public string Username { get; set; } = string.Empty;
		public byte[] Password { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public DateTime Created_At { get; set; }
		public string Created_By { get; set; }
		public DateTime? Updated_At { get; set; }
		public string? Updated_By { get; set; }
	}
}

