using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Entities;

namespace WebApi.Backend.Models
{
    public class AuthenticationModel
    {
        public Authentication UserInfo { get; set; }
        public string Token { get; set; }
    }

    public class Authentication
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
    }

    #region: Request
    public class UserRegisterRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
    }

    public class AuthenticateRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    #endregion

    #region: Response
    public class UserResponseData : AppUser
    {

    }

    public class AuthenticationResponseData : AuthenticationModel
    {

    }
    #endregion
}
