using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}', {aseguradora.Usuario.IdUsuario}"); //AseguradoraAdd(,);

                    if (RowsAfected > 0)
                    {
                        result.Correct = true; ;
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error al ingresar la aseguradora");
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static ML.Result Update(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"AseguradoraUpdate {aseguradora.IdAseguradora}, '{aseguradora.Nombre}', {aseguradora.Usuario.IdUsuario}");   //(, , );
                    if (RowsAfected > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error al actualizar la aseguradora");
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Delete(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"AseguradoraDelete {aseguradora.IdAseguradora}");
                    if (RowsAfected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error al eliminar la aseguradora");
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Aseguradoras.FromSqlRaw("AseguradoraGetAll").ToList();

                    result.Objects = new List<object>();

                    if (contex != null)
                    {
                        foreach (var obj in RowsAfected)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString();
                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = (int)obj.IdUsuario;
                            aseguradora.Usuario.NombreCompleto = obj.UsuarioNombre + obj.ApellidoPaterno + obj.ApellidoMaterno;
                            aseguradora.Usuario.Nombre = obj.UsuarioNombre;
                            aseguradora.Usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            aseguradora.Usuario.ApellidoMaterno = obj.ApellidoMaterno;

                            result.Objects.Add(aseguradora);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Aseguradoras.FromSqlRaw($"AseguradoraGetById {aseguradora.IdAseguradora}").AsEnumerable().FirstOrDefault();
                    //var RowsAfected = contex.Database.ExecuteSqlRaw($"AseguradoraGetById {aseguradora.IdAseguradora}").FirstOrDefault();
                    result.Object = new object();
                    if (RowsAfected != null)
                    {

                        aseguradora.IdAseguradora = (int)RowsAfected.IdAseguradora;
                        aseguradora.Nombre = RowsAfected.Nombre;
                        aseguradora.FechaCreacion = RowsAfected.FechaCreacion.ToString();
                        aseguradora.FechaModificacion = RowsAfected.FechaModificacion.ToString();
                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = (int)RowsAfected.IdUsuario;
                        aseguradora.Usuario.Nombre = RowsAfected.UsuarioNombre;
                        aseguradora.Usuario.ApellidoPaterno = RowsAfected.ApellidoPaterno;
                        aseguradora.Usuario.ApellidoMaterno = RowsAfected.ApellidoMaterno;
                        aseguradora.Usuario.NombreCompleto = RowsAfected.UsuarioNombre + RowsAfected.ApellidoPaterno + RowsAfected.ApellidoMaterno;

                        result.Object = aseguradora;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Aseguradora";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }




    }
}