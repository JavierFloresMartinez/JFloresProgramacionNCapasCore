using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
            // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5201/Api/");

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
                        usuario.Usuarios.Add(resultItemList);
                    }
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta";
                    return View("Modal");
                }

            }
            return View(usuario);//Muestra una vista

        }

        //[HttpPost]
        //public ActionResult GetAll(ML.Usuario usuario)
        //{

        //    ML.Result result = BL.Usuario.GetAll(usuario);
        //    usuario.Usuarios = result.Objects;
        //    if (result.Correct)
        //    {
        //        usuario.Usuarios = result.Objects;

        //    }
        //    else
        //    {
        //        ViewBag.Message = "Ocurrio un error al hacer la consulta" + result.ErrorMessage;
        //        return View("Modal");
        //    }

        //    return View(usuario);//Muestra una vista
        //}

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Usuario usuarioResult = new ML.Usuario();
            usuarioResult.Usuarios = new List<object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5201/Api/");

                var responseTask = client.PostAsJsonAsync("Usuario/GetAll", usuario);
                responseTask.Wait();

                var resultUsuario = responseTask.Result;

                if (resultUsuario.IsSuccessStatusCode)
                {
                    var readTask = resultUsuario.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        usuarioResult.Usuarios.Add(resultItemList);
                    }
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta";
                    return View("Modal");
                }

            }
                return View(usuarioResult);//Muestra una vista
        }


        //[HttpGet]
        //    public ActionResult Form(int? idUsuario)
        //    {
        //        ML.Result resultRol = BL.Rol.GetAll();
        //        ML.Result resultPais = BL.Pais.GetAll();

        //        ML.Usuario usuario = new ML.Usuario();
        //        usuario.Rol = new ML.Rol();

        //        usuario.Direccion = new ML.Direccion();
        //        usuario.Direccion.Colonia = new ML.Colonia();
        //        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
        //        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
        //        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();


        //        if (resultRol.Correct)
        //        {
        //            usuario.Rol.Rols = resultRol.Objects;
        //            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

        //        }
        //        if (idUsuario == null)
        //        {
        //            return View(usuario);
        //        }
        //        else
        //         {
        //            usuario.IdUsuario = idUsuario.Value;
        //            ML.Result result = BL.Usuario.GetById(usuario);
        //            usuario.Usuarios = result.Objects;
        //            usuario.Rol.Rols = resultRol.Objects;
        //            ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
        //            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
        //            ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
        //            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
        //            ML.Result resultColonia = BL.Colonia.ColoniaGetByIMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
        //            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
        //            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

        //            return View(usuario);
        //        }
        //    }
        //    [HttpPost]
        //    public ActionResult Form(ML.Usuario usuario)
        //    {

        //    if (ModelState.IsValid)//validar si se cumplieron todas las data annotations
        //    {
        //        IFormFile file = Request.Form.Files["inpImagen"];
        //        if (file != null)
        //        {
        //            usuario.Imagen = Convert.ToBase64String(ConvertToBytes(file));

        //        }
        //        ML.Result result = new ML.Result();
        //        //add o update
        //        if (usuario.IdUsuario == 0)
        //        {
        //            //add
        //            result = BL.Usuario.Add(usuario);
        //            if (result.Correct)
        //            {
        //                ViewBag.Message = "El usuario se agrego correctamente";

        //            }
        //            else
        //            {
        //                ViewBag.Message = "Hubo un error al agregar al usuario" + result.ErrorMessage;
        //            }

        //        }
        //        else
        //        {

        //            //update
        //            result = BL.Usuario.Update(usuario);
        //            if (result.Correct)
        //            {
        //                ViewBag.Message = "El usuario se actualizo correctamente";

        //            }
        //            else
        //            {
        //                ViewBag.Message = "Hubo un error al actualizar al usuario" + result.ErrorMessage;
        //            }

        //        }

        //    }
        //    else
        //    {
        //        ML.Result resultRol = BL.Rol.GetAll();
        //        ML.Result resultPais = BL.Pais.GetAll();

        //        //ML.Usuario usuario = new ML.Usuario();
        //        usuario.Rol = new ML.Rol();

        //        usuario.Direccion = new ML.Direccion();
        //        usuario.Direccion.Colonia = new ML.Colonia();
        //        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
        //        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
        //        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
        //        usuario.Rol.Rols = resultRol.Objects;
        //        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

        //        return View(usuario);

        //    }

        //        return View("Modal");
        //    }

        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {
            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();


            if (resultRol.Correct)
            {
                usuario.Rol.Rols = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

            }
            if (idUsuario == null)
            {
                return View(usuario);
            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5201/Api/");
                    var responseTask = client.GetAsync("Usuario/GetbyId/" + idUsuario);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());

                    }
                }
                usuario.Rol.Rols = resultRol.Objects;
                ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                ML.Result resultColonia = BL.Colonia.ColoniaGetByIMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                return View(usuario);
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {

            if (ModelState.IsValid)//validar si se cumplieron todas las data annotations
            {
                IFormFile file = Request.Form.Files["inpImagen"];
                if (file != null)
                {
                    usuario.Imagen = Convert.ToBase64String(ConvertToBytes(file));

                }
                ML.Result result = new ML.Result();
                //add o update
                if (usuario.IdUsuario == 0)
                {
                    //add
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5201/Api/");
                        var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
                        postTask.Wait();

                        var resulUsuario = postTask.Result;
                        if (resulUsuario.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "El usuario se agrego correctamente";

                        }
                        else
                        {
                            ViewBag.Message = "Hubo un error al agregar el usuario" + result.ErrorMessage;
                        }
                    }

                }
                else
                {

                    //update
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:5201/Api/");
                        var postTask = client.PutAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);
                        postTask.Wait();

                        var resultUpdate = postTask.Result;
                        if (resultUpdate.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "El usuario se actualizo correctamente";

                        }
                        else
                        {
                            ViewBag.Message = "Hubo un error al actualizar el usuario" + result.ErrorMessage;
                        }

                    }

                }

            }
            else
            {
                ML.Result resultRol = BL.Rol.GetAll();
                ML.Result resultPais = BL.Pais.GetAll();

                //ML.Usuario usuario = new ML.Usuario();
                usuario.Rol = new ML.Rol();

                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                usuario.Rol.Rols = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                return View(usuario);

            }

            return View("Modal");
        }

        public ActionResult Delete(int idusuario)
            {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5201/Api/");

                //HTTP POST
                var postTask = client.GetAsync("Usuario/Delete/" + idusuario);
                postTask.Wait();

                var resultDelete = postTask.Result;
                if (resultDelete.IsSuccessStatusCode)
                {
                    ViewBag.Message = "El usuario se elimino correctamente";

                }
                else
                {
                    ViewBag.Message = "Hubo un error al eliminar al usuario";
                }
            }
            return View("Modal");
        }

            [HttpPost]
            public JsonResult GetEstado(int idPais)
            {
                var result = BL.Estado.EstadoGetByIdPais(idPais);



                return Json(result.Objects);
            }

            [HttpPost]
            public JsonResult GetMunicipio(int idEstado)
            {
                var result = BL.Municipio.MunicipioGetByIdEstado(idEstado);



                return Json(result.Objects);
            }


            [HttpPost]
            public JsonResult GetColonia(int idMunicipio)
            {
                var result = BL.Colonia.ColoniaGetByIMunicipio(idMunicipio);

                return Json(result.Objects);
            }

        public static byte[] ConvertToBytes(IFormFile imagen) //Covierte a bytes la imagen
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        [HttpPost]
        public JsonResult CambiarStatus(int idAlumno, bool status) //Actualiza el estado del status 0 o 1
        {

            ML.Result result = BL.Usuario.CambiarEstatus(idAlumno, status);

            return Json(result);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Login(string Username, string Password)
        //{
        //    ML.Result result = BL.Usuario.GetByUsername(Username);
        //    if (result.Correct)
        //    {
        //        ML.Usuario usuario = (ML.Usuario)result.Object;
        //        if(usuario.Password == Password)
        //        {
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Error Contraseña Incorrecta";
        //            return PartialView("ModalLogin");
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Error Credenciales Incorrecta";
        //        return PartialView("ModalLogin");
        //    }
        //}
        [HttpPost]
        public IActionResult Login(string Username, string Password)
        {
            ML.Usuario usuario = new ML.Usuario();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5201/Api/");
                var responseTask = client.GetAsync("Usuario/Login/" + Username+ "/" + Password);
                responseTask.Wait();
                var resultAPI = responseTask.Result;
                if (resultAPI.IsSuccessStatusCode)
                {
                    var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());

                }
            }
            if(usuario.Username == Username)
            { 
                if (usuario.Password == Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "Error Contraseña Incorrecta";
                    return PartialView("ModalLogin");
                }
            }
            else
            {
                ViewBag.Message = "Error Credenciales Incorrecta";
                return PartialView("ModalLogin");
            }
        }
    }
}
