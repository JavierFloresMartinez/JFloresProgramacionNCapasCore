using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        [Route("Api/Usuario/GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost]
        [Route("Api/Usuario/GetAll")]
        public IActionResult GetAll([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAll(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost]
        [Route("Api/Usuario/Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpGet]
        [Route("Api/Usuario/Delete/{idUsuario}")]
        public IActionResult Delete(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = idUsuario;
            ML.Result result = BL.Usuario.Delete(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpGet]
        [Route("Api/Usuario/GetbyId/{id}")]
        public IActionResult GetById(int id)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = id;
            var result = BL.Usuario.GetById(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPut]
        [Route("api/Usuario/update/")]
        public IActionResult Put([FromBody] ML.Usuario usuario)
        {
            //usuario.IdUsuario = idUsuario;
            var result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("api/Usuario/Login/{Username}/{Password}")]
        public IActionResult Login(string Username, string Password)
        {
            ML.Result result = BL.Usuario.GetByUsername(Username);
            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                if(usuario.Password == Password)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Contraseña Incorrecta");
                }
                
            }
            else
            {
                return BadRequest("Credenciales Incorrectas");
            }
        }

    }
}
