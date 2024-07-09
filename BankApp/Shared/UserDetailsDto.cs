using System;
namespace BankApp.Shared
{
	public class UserDetailsDto
	{
        public string Id { get; set; }
        public string Username { get; set; }
        public string[] Roles { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
    }
}

