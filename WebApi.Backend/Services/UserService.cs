using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Backend.Interfaces;
using WebApi.Backend.Models;
using WebApi.Backend.Objects;
using WebApi.Data.Entities;
using WebApi.Backend.Helpers;
using WebApi.Data.EF;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApi.Backend.Common;
using Microsoft.Extensions.Configuration;

namespace WebApi.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly WebApiDbContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(IConfiguration config, WebApiDbContext dbContext,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            _config = config;
            _dbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResponseData<AuthenticationResponseData>> Authenticate(AuthenticateRequest request)
        {
            //var user = await _userManager.FindByNameAsync(request.UserName);
            //var login = await _signInManager.PasswordSignInAsync(request.UserName, MyHelper.GetMd5Hash(request.Password), true, true);

            //if (user == null)
            //    return new ErrorResponseData<AuthenticationResponseData>("Account does not exist.");

            //if (!login.Succeeded)
            //    return new ErrorResponseData<AuthenticationResponseData>("Username or password is incorect.");

            var user = _dbContext.AppUsers.FirstOrDefault(x => request.UserName == x.UserName && MyHelper.GetMd5Hash(request.Password) == x.Password);

            //if (string.IsNullOrEmpty(request.UserName))
            //    return new ErrorResponseData<AuthenticationResponseData>("Tài khoản không được để trống.");

            //if (string.IsNullOrEmpty(request.Password))
            //    return new ErrorResponseData<AuthenticationResponseData>("Mật khẩu không được để trống.");

            //if (request.UserName.Length < 4)
            //    return new ErrorResponseData<AuthenticationResponseData>("Tài khoản phải có ít nhất 4 ký tự.");

            //if (request.UserName.Length < 6)
            //    return new ErrorResponseData<AuthenticationResponseData>("Tài khoản phải có ít nhất 6 ký tự.");

            if (user == null)
                return new ErrorResponseData<AuthenticationResponseData>("Username or password incorect.");

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(Utilities.USER_ID, user.Id.ToString()),
                new Claim(Utilities.USER_EMAIL, user.Email),
                new Claim(Utilities.FIRST_NAME, user.FirstName),
                new Claim(Utilities.LAST_NAME, user.LastName),
                new Claim(Utilities.USER_ROLE, string.Join(";",roles)),
                new Claim(Utilities.USER_NAME, request.UserName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            var userInfo = new Authentication()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Dob = user.Dob,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            var resData = new AuthenticationResponseData()
            {
                UserInfo = userInfo,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
            };

            return new SuccessResponseData<AuthenticationResponseData>("Get token success.", resData);

        }

        public async Task<ResponseData<UserResponseData>> GetById(GetDetailRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            if (user == null)
                return new ErrorResponseData<UserResponseData>("Account does not exist.");

            var resData = new UserResponseData()
            {
                Id = user.Id,
                Dob = user.Dob,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber
            };

            return new SuccessResponseData<UserResponseData>("", resData);
        }

        public async Task<ResponseData<NonResponseData>> Register(UserRegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var email = await _userManager.FindByNameAsync(request.Email);

            if (user != null)
                return new ErrorResponseData<NonResponseData>("Username already exists!");

            if (email != null)
                return new ErrorResponseData<NonResponseData>("Email already exists!");

            user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                Dob = request.Dob,
                Password = MyHelper.GetMd5Hash(request.Password),
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
                return new SuccessResponseData<NonResponseData>("Register success!");

            return new ErrorResponseData<NonResponseData>("Register fail!");
        }
    }
}
