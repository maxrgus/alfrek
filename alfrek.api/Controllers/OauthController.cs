using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace alfrek.api.Controllers
{
    [Route("[controller]")]
    public class OauthController : Controller
    {
        [HttpGet("callback")]
        public void Callback()
        {
            string code = HttpContext.Request.Query["code"].ToString();
            string state = HttpContext.Request.Query["state"].ToString();
            Debug.WriteLine("CODE: " + code);
            Debug.WriteLine("STATE: " + state);
        }
    }
}