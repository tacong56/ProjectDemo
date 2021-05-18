using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Entities;

namespace WebApi.Backend.Models
{
    public class UserModel : AppUser
    {

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
    #endregion

    #region: Response
    public class UserResponseData
    {

    }
    #endregion
}
