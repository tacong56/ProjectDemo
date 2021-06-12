using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Backend.Models;
using WebApi.Backend.Objects;

namespace WebApi.Backend.Interfaces
{
    public interface IUserService
    {
        Task<ResponseData<NonResponseData>> Register(UserRegisterRequest request);
        Task<ResponseData<AuthenticationResponseData>> Authenticate(AuthenticateRequest request);
        Task<ResponseData<UserResponseData>> GetById(GetDetailRequest request);
    }
}
