using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class PolizaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Poliza poliza = new ML.Poliza();
            ML.Result result = BL.Poliza.GetAll();
            //aseguradora.Aseguradoras = result.Objects;
            if (result.Correct)
            {
                poliza.Polizas = result.Objects;

            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta" + result.ErrorMessage;
                return View("Modal");
            }

            return View(poliza);//Muestra una vista
        }

        [HttpGet]
        public ActionResult Form(int? idPoliza)
        {
            ML.Poliza poliza = new ML.Poliza();
            ML.Usuario usuario = new ML.Usuario();
            ML.SubPoliza subPoliza = new ML.SubPoliza();
            ML.Result resultUsuario = BL.Usuario.GetAll(usuario);
            ML.Result resultSubPoliza = BL.SubPoliza.GetAll();
           
            poliza.Usuario = new ML.Usuario();
            poliza.SubPoliza = new ML.SubPoliza();
            if (resultUsuario.Correct)
            {
                poliza.Usuario.Usuarios = resultUsuario.Objects;
                poliza.SubPoliza.SubPolizas = resultSubPoliza.Objects;

            }
            if (idPoliza == null)
            {
                return View(poliza);
            }
            else
            {
                poliza.IdPoliza = idPoliza.Value;
                ML.Result result = BL.Poliza.GetById(poliza);
                poliza.Polizas = result.Objects;
                poliza.Usuario.Usuarios = resultUsuario.Objects;
                poliza.SubPoliza.SubPolizas = resultSubPoliza.Objects;
                return View(poliza);
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Poliza poliza)
        {
          
            ML.Result result = new ML.Result();
            //add o update
            if (poliza.IdPoliza == 0)
            {
                //add
                result = BL.Poliza.Add(poliza);
                if (result.Correct)
                {
                    ViewBag.Message = "La poliza se agrego correctamente";

                }
                else
                {
                    ViewBag.Message = "Hubo un error al agregar la poliza" + result.ErrorMessage;
                }

            }
            else
            {

                //update
                result = BL.Poliza.Update(poliza);
                if (result.Correct)
                {
                    ViewBag.Message = "La poliza se actualizo correctamente";

                }
                else
                {
                    ViewBag.Message = "Hubo un error al actualizar la poliza" + result.ErrorMessage;
                }

            }

            return View("Modal");
        }


        public ActionResult Delete(int idPoliza)
        {
            ML.Poliza poliza = new ML.Poliza();
            poliza.IdPoliza = idPoliza;
            ML.Result result = BL.Poliza.Delete(poliza);
            if (result.Correct)
            {
                ViewBag.Message = "La poliza se elimino correctamente";

            }
            else
            {
                ViewBag.Message = "Hubo un error al eliminar la poliza" + result.ErrorMessage;
            }

            return View("Modal");
        }

    }
}
