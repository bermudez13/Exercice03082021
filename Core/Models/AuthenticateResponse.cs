namespace Exercice03082021.Core.Models
{
    public class AuthenticateResponse
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }
        public string Email {get; set;}
        public string Role { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.ID;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Password = user.Password;
            Token = token;
            Email = user.Email;
            Role = user.Role;
        }
    }
}