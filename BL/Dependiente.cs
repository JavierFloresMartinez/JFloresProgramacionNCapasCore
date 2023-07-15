using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        //GetAll
        public static ML.Result GetAll(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Dependientes.FromSqlRaw($"DependienteGetByIdEmpleado '{numeroEmpleado}'").ToList();
                    //var RowsAfected = contex.Database.ExecuteSqlRaw($"AseguradoraGetById {aseguradora.IdAseguradora}").FirstOrDefault();
                    result.Objects = new List<object>();
               
                        foreach (var obj in RowsAfected)
                        {
                        ML.Dependiente dependiente = new ML.Dependiente(); 
                            dependiente.IdDependiente = obj.IdDependiente;
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            dependiente.Empleado.Nombre = obj.EmpleadoNombre;
                            dependiente.Empleado.ApellidoPaterno = obj.EmpleadoApellidoPaterno;
                            dependiente.Empleado.ApellidoMaterno = obj.EmpleadoApellidoMaterno;
                            dependiente.Empleado.NombreCompleto = obj.EmpleadoNombre + obj.EmpleadoApellidoPaterno + obj.EmpleadoApellidoMaterno;
                            dependiente.Nombre = obj.Nombre;
                            dependiente.ApellidoPaterno = obj.ApellidoPaterno;
                            dependiente.ApellidoMaterno = obj.ApellidoMaterno;
                            dependiente.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                            dependiente.EstadoCivil = obj.EstadoCivil;
                            dependiente.Genero = obj.Genero;
                            dependiente.Telefono = obj.Telefono;
                            dependiente.RFC = obj.Rfc;
                            dependiente.DependienteTipo = new ML.DependienteTipo();
                            dependiente.DependienteTipo.Nombre = obj.NombreDependencia;
                           
                            result.Objects.Add(dependiente);
                        }
                        result.Correct = true;
                }
            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Add(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"DependienteAdd '{dependiente.Empleado.NumeroEmpleado}','{dependiente.Nombre}',  '{dependiente.ApellidoPaterno}', '{dependiente.ApellidoMaterno}',  '{dependiente.FechaNacimiento}', '{dependiente.EstadoCivil}',  '{dependiente.Genero}', '{dependiente.Telefono}',  '{dependiente.RFC}', {dependiente.DependienteTipo.IdDependienteTipo}");   //(, , );

                    if (RowsAfected > 0)
                    {
                        result.Correct = true; ;
                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error al ingresar el dependiente");
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

        public static ML.Result Update(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"DependienteUpdate {dependiente.IdDependiente},'{dependiente.Empleado.NumeroEmpleado}','{dependiente.Nombre}',  '{dependiente.ApellidoPaterno}', '{dependiente.ApellidoMaterno}',  '{dependiente.FechaNacimiento}', '{dependiente.EstadoCivil}',  '{dependiente.Genero}', '{dependiente.Telefono}',  '{dependiente.RFC}', {dependiente.DependienteTipo.IdDependienteTipo}");   //(, , );
                    if (RowsAfected > 0)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        Console.WriteLine("Ocurrio un error al actualizar el Dependiente");
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
