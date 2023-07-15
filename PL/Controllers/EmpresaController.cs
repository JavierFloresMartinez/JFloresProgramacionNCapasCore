using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAllLINQ();
      
            if (result.Correct)
            {
                empresa.Empresas = result.Objects;

            }
            else
            {
                ViewBag.Message = "Ocurrio un error al hacer la consulta" + result.ErrorMessage;
                return View("Modal");
            }

            return View(empresa);//Muestra una vista
        }


        [HttpGet]
        public ActionResult Form(int? idEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
           
            if (idEmpresa == null)
            {
                return View(empresa);
            }
            else
            {
                empresa.IdEmpresa = idEmpresa.Value;
                ML.Result result = BL.Empresa.GetByIdLINQ(empresa);
                empresa.Empresas = result.Objects;
               
                return View(empresa);
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            IFormFile file = Request.Form.Files["inpImagen"];
            if (file != null)
            {
                empresa.Logo = Convert.ToBase64String(ConvertToBytes(file));

            }

            ML.Result result = new ML.Result();
            //add o update
            if (empresa.IdEmpresa == 0)
            {
                //add
                result = BL.Empresa.AddLINQ(empresa);
                if (result.Correct)
                {
                    ViewBag.Message = "La empresa se agrego correctamente";

                }
                else
                {
                    ViewBag.Message = "Hubo un error al agregar la empresa" + result.ErrorMessage;
                }

            }
            else
            {

                //update
                result = BL.Empresa.UpdateLINQ(empresa);
                if (result.Correct)
                {
                    ViewBag.Message = "La empresa se actualizo correctamente";

                }
                else
                {
                    ViewBag.Message = "Hubo un error al actualizar la empresa" + result.ErrorMessage;
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


        public ActionResult Delete(int idEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            empresa.IdEmpresa = idEmpresa;
            ML.Result result = BL.Empresa.DeleteLINQ(empresa);
            if (result.Correct)
            {
                ViewBag.Message = "La empresa se elimino correctamente";

            }
            else
            {
                ViewBag.Message = "Hubo un error al eliminar la empresa" + result.ErrorMessage;
            }

            return View("Modal");
        }

    }
}

