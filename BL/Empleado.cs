using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTable = System.Data.DataTable;

namespace BL
{
    public class Empleado
    {

        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}', '{empleado.Rfc}', '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}','{empleado.Nss}', '{empleado.Foto}', {empleado.Empresa.IdEmpresa}"); //AseguradoraAdd(,);

                    if (RowsAfected > 0)
                    {
                        result.Correct = true; 
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error al ingresar el empleado");
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

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}', '{empleado.Rfc}', '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}','{empleado.Nss}', '{empleado.Foto}', {empleado.Empresa.IdEmpresa}");

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

        public static ML.Result Delete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"EmpleadoDelete '{empleado.NumeroEmpleado}'");
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

        public static ML.Result GetAll(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Empleados.FromSqlRaw($"EmpleadoGetAll '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();

                    if (contex != null)
                    {
                        foreach (var obj in RowsAfected)
                        {
                            empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.Rfc = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                            empleado.FechaIngreso = obj.FechaIngreso.ToString("dd-MM-yyyy");
                            empleado.Nss = obj.Nss;
                            empleado.Foto = obj.Foto;
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.NombreEmpresa;
                            result.Objects.Add(empleado);
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

        public static ML.Result GetById(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Empleados.FromSqlRaw($"EmpleadoGetById '{numeroEmpleado}'").AsEnumerable().FirstOrDefault();
                    result.Object = new object();
                    if (RowsAfected != null)
                    {

                        ML.Empleado empleado = new ML.Empleado();
                        empleado.NumeroEmpleado = RowsAfected.NumeroEmpleado;
                        empleado.Rfc = RowsAfected.Rfc;
                        empleado.Nombre = RowsAfected.Nombre;
                        empleado.ApellidoPaterno = RowsAfected.ApellidoPaterno;
                        empleado.ApellidoMaterno = RowsAfected.ApellidoMaterno;
                        empleado.Email = RowsAfected.Email;
                        empleado.Telefono = RowsAfected.Telefono;
                        empleado.FechaNacimiento = RowsAfected.FechaNacimiento.ToString("dd-MM-yyyy");
                        empleado.FechaIngreso = RowsAfected.FechaIngreso.ToString("dd-MM-yyyy");
                        empleado.Nss = RowsAfected.Nss;
                        empleado.Foto = RowsAfected.Foto;
                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = RowsAfected.IdEmpresa.Value;

                        result.Object = empleado;

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







        public static ML.Result ConvertExcellToDt(string conString)
        {
            ML.Result result = new ML.Result();
            try
            {
                DataTable dt = new DataTable();
                using (OleDbConnection contex = new OleDbConnection(conString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = contex;

                        using (OleDbDataAdapter da = new OleDbDataAdapter())
                        {
                            da.SelectCommand = cmd;

                            DataTable tableEmpleado = new DataTable();

                            da.Fill(tableEmpleado);

                            if (tableEmpleado.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();

                                foreach (DataRow row in tableEmpleado.Rows)
                                {
                                    ML.Empleado empleado = new ML.Empleado();
                                    empleado.NumeroEmpleado = row[0].ToString();
                                    empleado.Rfc = row[1].ToString();
                                    empleado.Nombre = row[2].ToString();
                                    empleado.ApellidoPaterno = row[3].ToString();
                                    empleado.ApellidoMaterno = row[4].ToString();
                                    empleado.Email = row[5].ToString();
                                    empleado.Telefono = row[6].ToString();
                                    empleado.FechaNacimiento = row[7].ToString();
                                    empleado.Nss = row[8].ToString();
                                    empleado.Empresa = new ML.Empresa();                     //NumeroEmpleado	RFC	Nombre	Apaterno	Amaterno	Email	Telefono	FechaNacimiento	NSS	IdEmpresa
                                    empleado.Empresa.IdEmpresa = int.Parse(row[9].ToString());

                                    result.Objects.Add(empleado);
                                }

                                //result.Correct = true;

                            }

                            result.Object = tableEmpleado;

                            if (tableEmpleado.Rows.Count >= 1)
                            {
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No existen registros en el excel";
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Correct = false;

            }
            return result;
        }

        public static ML.Result ValidarExcel(List<object> Objects)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Empleado empleado in Objects)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    empleado.NumeroEmpleado = (empleado.NumeroEmpleado == "") ? error.Mensaje +="Ingresar el numero de empleado" : empleado.NumeroEmpleado;
                    empleado.Rfc = (empleado.Rfc == "") ? error.Mensaje += "Ingresar el RFC  " : empleado.Rfc;
                    empleado.Nombre = (empleado.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : empleado.Nombre;
                    empleado.Nombre = (empleado.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : empleado.Nombre;
                    empleado.ApellidoPaterno = (empleado.ApellidoPaterno == "") ? error.Mensaje += "Ingresar el Apellido Paterno  " : empleado.ApellidoPaterno;
                    empleado.ApellidoMaterno = (empleado.ApellidoMaterno == "") ? error.Mensaje += "Ingresar el Apellido Materno  " : empleado.ApellidoMaterno;
                    empleado.Email = (empleado.Email == "") ? error.Mensaje += "Ingresar el Email  " : empleado.Email;
                    empleado.Telefono = (empleado.Telefono == "") ? error.Mensaje += "Ingresar el Telefono  " : empleado.Telefono;
                    empleado.FechaNacimiento = (empleado.FechaNacimiento == "") ? error.Mensaje += "Ingresar el nombre  " : empleado.FechaNacimiento;
                    empleado.Nss = (empleado.Nss == "") ? error.Mensaje += "Ingresar el NSS  " : empleado.Nss;
                    empleado.Empresa = new ML.Empresa();
                    if (empleado.Empresa.IdEmpresa.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el Id  ";
                    }
        

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }

                }
                result.Correct = true;
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

    