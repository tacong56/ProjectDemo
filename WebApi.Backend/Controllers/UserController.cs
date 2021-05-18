using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Backend.Interfaces;
using WebApi.Backend.Models;

namespace WebApi.Backend.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;

        public UserController(IUser user)
        {
            _user = user;
        }

        public IActionResult Index()
        {
            return Ok("abc");
        }

        //[HttpPost]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _user.Register(request);

            if (result != null && result.Error == 0)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
