using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'").ToList();

                    result.Objects = new List<object>();

                    if (contex != null)
                    {
                        foreach (var obj in RowsAfected)
                        {
                            usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.NombreCompleto = obj.Nombre + obj.ApellidoPaterno + obj.ApellidoMaterno;
                            usuario.CorreoElectronico = obj.CorreoElectronico;
                            usuario.Matricula = (int)obj.Matricula;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.NombreRol;
                            usuario.Username = obj.Username;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.Curp = obj.Curp;
                            usuario.Imagen = obj.Imagen;
                            usuario.Estatus = obj.Estatus.Value;
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Nombre = obj.Colonia;
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.Municipio;
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.Estado;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.Pais;

                            result.Objects.Add(usuario);
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

        public static ML.Result GetById(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Usuarios.FromSqlRaw($"UsuarioGetById {usuario.IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Object = new object();
                    
                    if (RowsAfected != null)
                    {

                       
                        usuario.IdUsuario = RowsAfected.IdUsuario;
                        usuario.Nombre = RowsAfected.Nombre;
                        usuario.ApellidoPaterno = RowsAfected.ApellidoPaterno;
                        usuario.ApellidoMaterno = RowsAfected.ApellidoMaterno;
                        usuario.CorreoElectronico = RowsAfected.CorreoElectronico;
                        usuario.Matricula = (int)RowsAfected.Matricula;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = (byte)RowsAfected.IdRol;
                        usuario.Username = RowsAfected.Username;
                        usuario.Password = RowsAfected.Password;
                        usuario.FechaNacimiento = RowsAfected.FechaNacimiento.ToString("dd-MM-yyyy");
                        usuario.Sexo = RowsAfected.Sexo;
                        usuario.Telefono = RowsAfected.Telefono;
                        usuario.Celular = RowsAfected.Celular;
                        usuario.Curp = RowsAfected.Curp;
                        usuario.Imagen = RowsAfected.Imagen;
                        usuario.Estatus = RowsAfected.Estatus.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = RowsAfected.IdDireccion.Value;
                        usuario.Direccion.Calle = RowsAfected.Calle;
                        usuario.Direccion.NumeroInterior = RowsAfected.NumeroInterior;
                        usuario.Direccion.NumeroExterior = RowsAfected.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = RowsAfected.IdColonia.Value;
                        usuario.Direccion.Colonia.Nombre = RowsAfected.Colonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = RowsAfected.IdMunicipio.Value;
                        usuario.Direccion.Colonia.Municipio.Nombre = RowsAfected.Municipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = RowsAfected.IdEstado.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = RowsAfected.Estado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = RowsAfected.IdPais.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = RowsAfected.Pais;
                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Departamento";
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

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.CorreoElectronico}', {usuario.Matricula}, {usuario.Rol.IdRol}, '{usuario.Username}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.Curp}', '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', '{usuario.Direccion.Colonia.IdColonia}' ");     
                                                                                //(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.CorreoElectronico, usuario.Matricula, usuario.Rol.IdRol, usuario.Username, usuario.Password, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Curp, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                    if (RowsAfected >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio Un Error Al Ingresar Al Usuario ";
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


        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.CorreoElectronico}', {usuario.Matricula}, {usuario.Rol.IdRol},'{usuario.Username}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.Curp}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', '{usuario.Direccion.Colonia.IdColonia}', '{usuario.Imagen}'");
                                                     //UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.CorreoElectronico, usuario.Matricula, usuario.Rol.IdRol, usuario.Username, usuario.Password, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Curp, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia, usuario.Imagen);

                    if (RowsAfected >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio Un Error Al Actualizar El Usuario ";
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


        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}"); //UsuarioDelete(usuario.IdUsuario);


                    if (RowsAfected >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio Un Error Al Eliminar El Usuario ";
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


        public static ML.Result CambiarEstatus(int idUsuario, bool status)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    int RowsAfected = contex.Database.ExecuteSqlRaw($"EstatusUpdate {idUsuario}, {status} ");
                   
                    if (RowsAfected >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio Un Error Al Actualizar El Estatus ";
                    }
                }

            }
            catch(Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }


        public static ML.Result GetByUsername(string Username)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Usuarios.FromSqlRaw($"UsuarioGetByUsername '{Username}'").AsEnumerable().FirstOrDefault();
                    result.Object = new object();

                    if (RowsAfected != null)
                    {

                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = RowsAfected.IdUsuario;
                        usuario.Nombre = RowsAfected.Nombre;
                        usuario.ApellidoPaterno = RowsAfected.ApellidoPaterno;
                        usuario.ApellidoMaterno = RowsAfected.ApellidoMaterno;
                        usuario.CorreoElectronico = RowsAfected.CorreoElectronico;
                        usuario.Matricula = (int)RowsAfected.Matricula;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = (byte)RowsAfected.IdRol;
                        usuario.Username = RowsAfected.Username;
                        usuario.Password = RowsAfected.Password;
                        usuario.FechaNacimiento = RowsAfected.FechaNacimiento.ToString("dd-MM-yyyy");
                        usuario.Sexo = RowsAfected.Sexo;
                        usuario.Telefono = RowsAfected.Telefono;
                        usuario.Celular = RowsAfected.Celular;
                        usuario.Curp = RowsAfected.Curp;
                        usuario.Imagen = RowsAfected.Imagen;
                        usuario.Estatus = RowsAfected.Estatus.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = RowsAfected.IdDireccion.Value;
                        usuario.Direccion.Calle = RowsAfected.Calle;
                        usuario.Direccion.NumeroInterior = RowsAfected.NumeroInterior;
                        usuario.Direccion.NumeroExterior = RowsAfected.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = RowsAfected.IdColonia.Value;
                        usuario.Direccion.Colonia.Nombre = RowsAfected.Colonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = RowsAfected.IdMunicipio.Value;
                        usuario.Direccion.Colonia.Municipio.Nombre = RowsAfected.Municipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = RowsAfected.IdEstado.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = RowsAfected.Estado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = RowsAfected.IdPais.Value;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = RowsAfected.Pais;
                        result.Object =usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Departamento";
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
