using DL;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {

        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"  EmpresaAdd '{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}','{empresa.DireccionWeb}', '{empresa.Logo}'"); //@Nombre VARCHAR(100),  @Telefono VARCHAR(100),   @Email VARCHAR(100),@DireccionWb VARCHAR(100),   @Logo VARCHAR(MAX)



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

        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"EmpresaUpdate {empresa.IdEmpresa}, '{empresa.Nombre}', '{empresa.Telefono}', '{empresa.Email}','{empresa.DireccionWeb}', '{empresa.Logo}'");   //(, , );
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

        public static ML.Result Delete(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"EmpresaDelete {empresa.IdEmpresa}");
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
                    var RowsAfected = contex.Empresas.FromSqlRaw("EmpresaGetAll").ToList();

                    result.Objects = new List<object>();

                    if (contex != null)
                    {
                        foreach (var obj in RowsAfected)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;
                        
                            result.Objects.Add(empresa);
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

        public static ML.Result GetById(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Empresas.FromSqlRaw($"EmpresaGetById '{empresa.IdEmpresa}'").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (RowsAfected != null)
                    {

                        //ML.Empresa empresa = new ML.Empresa();
                        empresa.IdEmpresa = RowsAfected.IdEmpresa;
                        empresa.Nombre = RowsAfected.Nombre;
                        empresa.Telefono = RowsAfected.Telefono;
                        empresa.Email = RowsAfected.Email;
                        empresa.DireccionWeb = RowsAfected.DireccionWeb;
                        empresa.Logo = RowsAfected.Logo;

                        result.Objects.Add(empresa);

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Empleados";
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

        public static ML.Result AddLINQ(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext context = new DL.JfloresProgramacionNcapasContext())
                {
                    DL.Empresa empresaLin = new DL.Empresa();
                    empresaLin.Nombre = empresa.Nombre;
                    empresaLin.Telefono = empresa.Telefono;
                    empresaLin.Email = empresa.Email;
                    empresaLin.DireccionWeb = empresa.DireccionWeb;
                    empresa.Logo = empresa.Logo;

                    context.Empresas.Add(empresaLin);

                    var filasAfectadas = context.SaveChanges();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage=ex.Message;
            }
            return result;
        }

        public static ML.Result UpdateLINQ(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext context = new DL.JfloresProgramacionNcapasContext())
                {

                    var query = (from empresaLin in context.Empresas where empresaLin.IdEmpresa == empresa.IdEmpresa select empresaLin).SingleOrDefault();
                    query.Nombre = empresa.Nombre;
                    query.Telefono = empresa.Telefono;
                    query.Email =empresa.Email;
                    query.DireccionWeb = empresa.DireccionWeb;
                    query.Logo = empresa.Logo;

                    var filasAfectadas = context.SaveChanges();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result DeleteLINQ(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext Context = new DL.JfloresProgramacionNcapasContext())
                {
                    var query = (from empresaLin in Context.Empresas
                                 where empresaLin.IdEmpresa == empresa.IdEmpresa
                                 select empresaLin).First();

                    Context.Empresas.Remove(query);
                    int filasAfectadas = Context.SaveChanges();
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error");
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

        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JfloresProgramacionNcapasContext context = new DL.JfloresProgramacionNcapasContext())
                {
                    var query = (from empresa in context.Empresas

                                 select new
                                 {
                                     IdEmpresa = empresa.IdEmpresa,
                                     Nombre = empresa.Nombre,
                                     Telefono = empresa.Telefono,
                                     Email = empresa.Email,
                                     DireccionWeb = empresa.DireccionWeb,
                                     Logo = empresa.Logo,
                });

                    result.Objects = new List<object>();

                    if (query != null && query.ToList().Count > 0)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empresa empresaLin = new ML.Empresa();
                            empresaLin.IdEmpresa = obj.IdEmpresa;
                            empresaLin.Nombre = obj.Nombre;
                            empresaLin.Telefono = obj.Telefono;
                            empresaLin.Email = obj.Email;
                            empresaLin.DireccionWeb = obj.DireccionWeb;
                            empresaLin.Logo = obj.Logo;

                            result.Objects.Add(empresaLin);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros";
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



        public static ML.Result GetByIdLINQ(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var query = (from empresaLin in contex.Empresas
                                 where empresaLin.IdEmpresa == empresa.IdEmpresa
                                 select new
                                 {
                                     IdEmpresa = empresaLin.IdEmpresa,
                                     Nombre = empresaLin.Nombre,
                                     Telefono = empresaLin.Telefono,
                                     Email = empresaLin.Email,
                                     DireccionWeb = empresaLin.DireccionWeb,
                                     Logo = empresaLin.Logo,
                                 }).FirstOrDefault();


                    result.Objects = new List<object>();
                    if (query != null)
                    {

                        empresa.IdEmpresa = query.IdEmpresa;
                        empresa.Nombre = query.Nombre;
                        empresa.Telefono = query.Telefono;
                        empresa.Email = query.Email;
                        empresa.DireccionWeb = query.DireccionWeb;
                        empresa.Logo = query.Logo;
                        result.Objects.Add(query);

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
