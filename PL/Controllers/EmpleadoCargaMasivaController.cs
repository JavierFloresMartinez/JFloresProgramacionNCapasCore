using Microsoft.AspNetCore.Mvc;
using System.IO;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class EmpleadoCargaMasivaController : Controller
    {
        private IHostingEnvironment environment;
        private IConfiguration configuration;
        public EmpleadoCargaMasivaController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            environment = _environment;
            configuration = _configuration;
        }

        [HttpGet]
        public IActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }



        [HttpPost]
        public IActionResult CargaMasiva(ML.Empleado empleado)
        {
            IFormFile file = Request.Form.Files["FileExcel"];

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (file != null)
                {
                    //.xsls , .xls, .csv
                    //obtener el nombre de nuestro archivo
                    string fileName = Path.GetFileName(file.FileName);

                    string folderPath = configuration["PathFolder"];
                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extensionAppsettings = configuration["TipoExcel"];
                    if (extensionArchivo == extensionAppsettings)
                    {

                        string filePath = Path.Combine(environment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName) + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx");
                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }
                            string connString = configuration["ExcelConString"] + filePath;  //cadena de conexion y ruta especifica del archivo

                            ML.Result resultExcell = BL.Empleado.ConvertExcellToDt(connString);
                            if (resultExcell.Correct)
                            {
                                ML.Result resultValidacion = BL.Empleado.ValidarExcel(resultExcell.Objects);

                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }

                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El excel no contiene registros";
                            }
                        }

                    }
                    else
                    {
                        ViewBag.Message = "El archivo que se intenta procesar no es un excel";
                    }

                }
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = configuration["ExcelConString"] + rutaArchivoExcel;

                ML.Result resultData = BL.Empleado.ConvertExcellToDt(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Empleado empleadoItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Empleado.Add(empleadoItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Empleado con nombre: " + empleadoItem.Nombre + " Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        string fileError = Path.Combine(environment.WebRootPath, @"C:\Users\digis\OneDrive\Documents\Javier Flores Martinez\MensajeError.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los Empleados No han sido registrados correctamente";
                    }
                    else
                    {
                        //borrar session
                        HttpContext.Session.Clear();
                        ViewBag.Message = "Los Empleados han sido registrados correctamente";
                    }

                }

            }
            return PartialView("Modal");
        
        }



        public IActionResult ReadFile(ML.Empleado empleado)
        {
            IFormFile file = Request.Form.Files["Filetext"];


            if (file != null)
            {
                string fileName = Path.GetFileName(file.FileName);
                StreamReader Textfile = new StreamReader(fileName);
                int n = 1;
                string line;
                line = Textfile.ReadLine();
                while ((line = Textfile.ReadLine()) != null)
                {
                    string[] lines = line.Split(",");

                    //ML.Empleado empleado = new ML.Empleado();

                    empleado.NumeroEmpleado = lines[0];
                    empleado.Rfc = lines[1];
                    empleado.Nombre = lines[2];
                    empleado.ApellidoPaterno = lines[3];
                    empleado.ApellidoMaterno = lines[4];
                    empleado.Email = lines[5];
                    empleado.Telefono = lines[6];
                    empleado.FechaNacimiento = lines[7];
                    empleado.Nss = lines[8];
                    empleado.Empresa = new ML.Empresa();
                    empleado.Empresa.IdEmpresa = int.Parse(lines[9]);
                    empleado.Foto = "";

                    ML.Result result = BL.Empleado.Add(empleado);

                    if (result.Correct)
                    {
                        Console.WriteLine("Se inserto el registro" + n);
                        Console.ReadKey();
                    }
                    else
                    {
                        string fileError = @"C:\Users\digis\OneDrive\Documents\Javier Flores Martinez\MensajeError.txt";
                        StreamWriter errorFile = new StreamWriter(fileError);
                        errorFile.WriteLine("Ocurrio un error al insertar el texto" + result.ErrorMessage);

                        errorFile.Close();

                    }
                    n++;
                }
            }
            return View();
        }
    }
}
