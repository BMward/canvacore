using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CanvacoreLib;

namespace Canvacore.Controllers
{
    [Route("api/canvas")]
    public class CanvasController : Controller
    {
        [Route("")]
        [HttpGet]
        public ToEncode Get()
        {
            return new ToEncode();
        }

        [Route("submit/{input}")]
        [HttpGet]
        public IActionResult GetInput(string input) 
        {
            //make this configurable...
            string alphabet = "abcdefghijklmnopqrstuvwxyz ,.";
            EncoderMethods methods = new EncoderMethods();
            var can = methods.EncodeObject(input, alphabet);
            return Ok(can);
        }

    }
}
