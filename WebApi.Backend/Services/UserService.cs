using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Backend.Interfaces;
using WebApi.Backend.Models;
using WebApi.Backend.Objects;
using WebApi.Data.Entities;

namespace WebApi.Backend.Services
{
    public class UserService : IUser
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ResponseData<UserResponseData>> Register(UserRegisterRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var email = await _userManager.FindByNameAsync(request.Email);
            if (user != null)
                return new ErrorResponseData<UserResponseData>("Username already exists!");

            if (email != null)
                return new ErrorResponseData<UserResponseData>("Email already exists!");

            user = new AppUser()
            {
                UserName = request.UserName,
                Email = request.Email,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PhoneNumber = request.PhoneNumber,
                Dob = request.Dob,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
                return new SuccessResponseData<UserResponseData>("Register success!");

            return new ErrorResponseData<UserResponseData>("Register fail!");
        }
    }
}
