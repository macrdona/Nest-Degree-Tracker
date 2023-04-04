using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Entities
{
    //base User Model
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public bool Completed { get; set; } = false;

        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }

    //user model for registering users
    public class RegisterRequest
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }

    //user model for updating a user's information
    public class UpdateRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    //user model for authenticating a response
    public class AuthenticateResponse
    {
        [JsonIgnore]
        public int UserId { get; set; }
        [JsonIgnore]
        public string? FirstName { get; set; }
        [JsonIgnore]
        public string? LastName { get; set; }
        [JsonIgnore]
        public string? Username { get; set; }
        public string? Token { get; set; }
    }

    //user model for authenticating a request
    public class AuthenticateRequest
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
