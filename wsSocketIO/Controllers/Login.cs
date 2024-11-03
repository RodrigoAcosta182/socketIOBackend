using Microsoft.AspNetCore.Mvc;

namespace wsSocketIO.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class Login : Controller
    {

        [HttpPost("/login/{usuario}", Name = "Login")]
        public string LoginUsuario(string usuario)
        {
            return usuario;
        }

    }
}
