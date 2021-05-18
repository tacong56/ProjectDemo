using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Backend.Models;
using WebApi.Backend.Objects;

namespace WebApi.Backend.Interfaces
{
    public interface IUser
    {
        Task<ResponseData<UserResponseData>> Register(UserRegisterRequest request);
    }
}
