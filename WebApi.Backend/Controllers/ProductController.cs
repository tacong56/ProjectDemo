using Microsoft.AspNetCore.Authorization;
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
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index([FromBody] GetProductRequest request)
        {
            var result = await _productService.Get(request);
            if (result != null)
                return Ok(result);

            return BadRequest();
        }


        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        //[Authorize(Roles = "Admin,Customer")]
        [Authorize]
        public async Task<IActionResult> Create([FromForm] CreateProductRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.Create(request);
            if (result != null && result.Error == 0)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
