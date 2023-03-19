using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyController : ControllerBase
    {
        [HttpGet]
        public int Get()
        {
            return 0;
        }
        [HttpGet("word")]
        public int Get(string word)
        {
            return word.Length;
        }
    }
}
