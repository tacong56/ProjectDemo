using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Backend.Controllers
{
    [Route("api/[controller]")]
    public class MyController<T> : Controller where T : MyController<T>
    {

    }
}
