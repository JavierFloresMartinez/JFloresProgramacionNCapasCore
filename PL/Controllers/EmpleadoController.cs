using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll(empleado);
            //empleado.Em = result.Objects;

            if (result.Correct)
            {
                empleado.Empleados = result.Objects;

            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta" + result.ErrorMessage;
                return View("Modal");
            }

            return View(empleado);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Empleado empleado)
        {
           // ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAll(empleado);
            //empleado.Em = result.Objects;

            if (result.Correct)
            {
                empleado.Empleados = result.Objects;

            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta" + result.ErrorMessage;
                return View("Modal");
            }

            return View(empleado);
        }

        [HttpGet]
        public ActionResult Form(string? numeroEmpleado)      
        {
            ML.Result resultEmpresas = BL.Empresa.GetAll();
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            if (resultEmpresas.Correct)
            {
                empleado.Empresa.Empresas = resultEmpresas.Objects;

            }
            if (numeroEmpleado == null)
            {
                empleado.Action = "Add";
                return View(empleado);
            }
            else
            {
                //empleado.NumeroEmpleado = numeroEmpleado;
                ML.Result result = BL.Empleado.GetById(numeroEmpleado);
                empleado.Empleados = result.Objects;
                empleado.Empresa.Empresas = resultEmpresas.Objects;
                empleado.Action = "Update";

                return View(empleado);
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
        
                IFormFile file = Request.Form.Files["inpImagen"];
                if (file != null)
                {
                    empleado.Foto = Convert.ToBase64String(ConvertToBytes(file));

                }
            
                ML.Result result = new ML.Result();
            //add o update
               if (empleado.Action == "Add")
               {
                //add
                   result = BL.Empleado.Add(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = "El empleado se agrego correctamente";

                }
                else
                {
                    ViewBag.Message = "Hubo un error al agregar al empleado" + result.ErrorMessage;
                }

                }
                else
                {

                //update
                result = BL.Empleado.Update(empleado);
                if (result.Correct)
                {
                    ViewBag.Message = "El Empleado se actualizo correctamente";

                }
                else
                {
                    ViewBag.Message = "Hubo un error al actualizar al empleado" + result.ErrorMessage;
                }

            }

            return View("Modal");
        }

        public static byte[] ConvertToBytes(IFormFile foto) //Covierte a bytes la imagen
        {

            using var fileStream = foto.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }


        public ActionResult Delete(string? numeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.NumeroEmpleado = numeroEmpleado;
            ML.Result result = BL.Empleado.Delete(empleado);
            if (result.Correct)
            {
                ViewBag.Message = "El usuario se elimino correctamente";

            }
            else
            {
                ViewBag.Message = "Hubo un error al eliminar al usuario" + result.ErrorMessage;
            }

            return View("Modal");
        }


    }
}
