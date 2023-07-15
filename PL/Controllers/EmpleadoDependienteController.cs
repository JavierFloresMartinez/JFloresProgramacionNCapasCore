using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        [HttpGet]
       public IActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();

            ML.Result result = BL.Empleado.GetAll(empleado);
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
        

        public IActionResult GetAllDependiente(string numeroEmpleado)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            ML.Result result = BL.Dependiente.GetAll(numeroEmpleado);
            
            if (result.Correct)
            {
                
                dependiente.Empleado = new ML.Empleado();
                dependiente.DependienteTipo = new ML.DependienteTipo();
                ML.Result resultDependientesTipo = BL.DependienteTipo.GetAll();
                dependiente.DependienteTipo.DependienteTipos = resultDependientesTipo.Objects;
                dependiente.Dependientes = result.Objects;
                ML.Result resultEmpleado = BL.Empleado.GetById(numeroEmpleado);
                dependiente.Empleado = (ML.Empleado)resultEmpleado.Object;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta" + result.ErrorMessage;
                return View("Modal1");
            }

            return View(dependiente);
        }

        [HttpPost]
        public IActionResult Form(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            if (dependiente.IdDependiente == 0)
            {
                //add
                result = BL.Dependiente.Add(dependiente);
                if (result.Correct)
                {
                    ViewBag.Message = "Se agrego correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error " + result.ErrorMessage;
                }
     
                return View("Modal1");
            }
            else
            {
                //update
                result = BL.Dependiente.Update(dependiente);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo Correctamente";
                }
                else
                {
                    ViewBag.Message ="Ocurrio un error : " + result.ErrorMessage;
                }

                return View("Modal1");
            }

        }



    }
}
