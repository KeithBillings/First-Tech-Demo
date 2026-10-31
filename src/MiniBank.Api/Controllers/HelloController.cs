using Microsoft.AspNetCore.Mvc;

namespace MiniBank.Api.Controllers;

[ApiController]
[Route("hello")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult SayHello()
    {
        return Ok(new { message = "Hello from Mini Bank" });
    }
}