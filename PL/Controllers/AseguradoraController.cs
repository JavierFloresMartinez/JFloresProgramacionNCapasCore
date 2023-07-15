using Microsoft.AspNetCore.Mvc;
using System.IO;
using ML;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        // GET: Aseguradora
        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Aseguradora aseguradora = new ML.Aseguradora();
        //    ML.Result result = BL.Aseguradora.GetAll();
        //    aseguradora.Aseguradoras = result.Objects;
        //    if (result.Correct)
        //    {
        //        aseguradora.Aseguradoras = result.Objects;

        //    }
        //    else
        //    {
        //        ViewBag.Message = "Ocurrio un error al hacer la consulta" + result.ErrorMessage;
        //        return View("Modal");
        //    }

        //    return View(aseguradora);//Muestra una vista
        //}

        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public AseguradoraController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Aseguradoras = new List<Object>();
            string urlAPI = configuration["WebApi"];

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("Aseguradora/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Aseguradora resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(resultItem.ToString());
                        aseguradora.Aseguradoras.Add(resultItemList);
                    }
                }
            }
            return View(aseguradora);
        }


        //[HttpGet]
        //public ActionResult Form(int? idAseguradora)
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    ML.Result resultUsuario = BL.Usuario.GetAll(usuario);
        //    ML.Aseguradora aseguradora = new ML.Aseguradora();
        //    aseguradora.Usuario = new ML.Usuario();


        //    if (resultUsuario.Correct)
        //    {
        //        aseguradora.Usuario.Usuarios = resultUsuario.Objects;
        //    }
        //    if (idAseguradora == null)
        //    {
        //        return View(aseguradora);
        //    }
        //    else
        //    {
        //        aseguradora.IdAseguradora = idAseguradora.Value;
        //        ML.Result result = BL.Aseguradora.GetById(aseguradora);
        //        aseguradora.Aseguradoras = result.Objects;
        //        aseguradora.Usuario.Usuarios = resultUsuario.Objects;

        //        return View(aseguradora);
        //    }
        //}
        //[HttpPost]
        //public ActionResult Form(ML.Aseguradora aseguradora)
        //{
        //    ML.Result result = new ML.Result();
        //    //add o update
        //    if (aseguradora.IdAseguradora == 0)
        //    {
        //        //add
        //        result = BL.Aseguradora.Add(aseguradora);
        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "La aseguradora se agrego correctamente";

        //        }
        //        else
        //        {
        //            ViewBag.Message = "Hubo un error al agregar la aseguradora" + result.ErrorMessage;
        //        }

        //    }
        //    else
        //    {

        //        //update
        //        result = BL.Aseguradora.Update(aseguradora);
        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "La Aseguradora se actualizo correctamente";

        //        }
        //        else
        //        {
        //            ViewBag.Message = "Hubo un error al actualizar la Aseguradora" + result.ErrorMessage;
        //        }

        //    }

        //    return View("Modal");
        //}

        [HttpGet]
        public ActionResult Form(int? idAseguradora)
        {
            ML.Result resultUsu = new ML.Result();
            resultUsu.Objects = new List<object>();
            string urlAPI = configuration["WebApi"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                var responseTask = client.GetAsync("Usuario/GetAll");
                responseTask.Wait();

                var resultUsuario = responseTask.Result;

                if (resultUsuario.IsSuccessStatusCode)
                {
                    var readTask = resultUsuario.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultUsu.Objects.Add(resultItemList);
                    }
                }
            }
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            
            aseguradora.Usuario = new ML.Usuario();
             aseguradora.Usuario.Usuarios = resultUsu.Objects;
            
            if (idAseguradora == null)
            {
                return View(aseguradora);
            }
            else
            {
               
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    var responseTask = client.GetAsync("Aseguradora/GetbyId/" + idAseguradora);
                        responseTask.Wait();
                        var resultAPI = responseTask.Result;
                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                 
                            aseguradora = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(readTask.Result.Object.ToString());
                         
                        }
                    }
                aseguradora.Usuario.Usuarios = resultUsu.Objects;

                return View(aseguradora);
            }
           
        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            //add o update
            if (aseguradora.IdAseguradora == 0)
            {
                string urlAPI = configuration["WebApi"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);
                    var postTask = client.PostAsJsonAsync<ML.Aseguradora>("Aseguradora/Add", aseguradora);
                    postTask.Wait();

                    var resultAseguradora = postTask.Result;
                    if (resultAseguradora.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "La aseguradora se agrego correctamente";

                    }
                    else
                    {
                        ViewBag.Message = "Hubo un error al agregar la aseguradora" + result.ErrorMessage;
                    }
                }
            }
            else
            {

                string urlAPI = configuration["WebApi"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    //HTTP POST
                    var postTask = client.PutAsJsonAsync<ML.Aseguradora>("Aseguradora/Update" , aseguradora);
                    postTask.Wait();

                    var resultUpdate = postTask.Result;
                    if (resultUpdate.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "La Aseguradora se actualizo correctamente";

                    }
                    else
                    {
                        ViewBag.Message = "Hubo un error al actualizar la Aseguradora" + result.ErrorMessage;
                    }

                }
            }
            return View("Modal");
        }

        //[HttpDelete]
        //public ActionResult Delete(int idAseguradora)
        //{
        //    ML.Aseguradora aseguradora = new ML.Aseguradora();
        //    aseguradora.IdAseguradora = idAseguradora;
        //    ML.Result result = BL.Aseguradora.Delete(aseguradora);
        //    if (result.Correct)
        //    {
        //        ViewBag.Message = "La aseguradora se elimino correctamente";

        //    }
        //    else
        //    {
        //        ViewBag.Message = "Hubo un error al eliminar la aseguradora" + result.ErrorMessage;
        //    }

        //    return View("Modal");
        //}

    
        public ActionResult Delete(int idAseguradora)
        {
            string urlAPI = configuration["WebApi"];
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlAPI);

                //HTTP POST
                var postTask = client.DeleteAsync("Aseguradora/Delete/" + idAseguradora);
                postTask.Wait();

                var resultDelete = postTask.Result;
                if (resultDelete.IsSuccessStatusCode)
                {
                    ViewBag.Message = "La aseguradora se elimino correctamente";

                }
                else
                {
                    ViewBag.Message = "Hubo un error al eliminar la aseguradora";
                }
            }
                return View("Modal");
        }

    }
}

