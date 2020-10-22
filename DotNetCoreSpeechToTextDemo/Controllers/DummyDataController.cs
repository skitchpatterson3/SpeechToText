using DotNetCoreSpeechToTextDemo.Contexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreSpeechToTextDemo.Controllers
{
    [ApiController]
    [Route("api/testdatabase")]
    public class DummyDataController : ControllerBase
    {
        private readonly AudioFileInfoContext _ctx;

        public DummyDataController(AudioFileInfoContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        [HttpGet]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}
