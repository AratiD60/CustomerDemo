using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CustomerDemo.API.Controllers
{
    public class HomeController : ControllerBase
    {
        public string Index()
        {
            return "API Running...";
        }
    }
}