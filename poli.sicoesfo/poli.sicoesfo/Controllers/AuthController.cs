using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using poli.sicoesfo.JWT;
using poli.sicoesfo.Models;

namespace poli.sicoesfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            string user = "poli";
            string password = "2021";
            if (!ModelState.IsValid)
            {
                return BadRequest("User and Password are required");
            }
            if (!(user == model.UserName && model.Password == password))
                return BadRequest(new { message = "Username or password is incorrect" });
            string token = new TokenGenerator().GetToken(user);
            return Ok(token);
        }

        [HttpGet("[action]")]
        public string EchoUser()
        {
            var identity = User.Identity;
            return $" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}";
        }
    }
}
