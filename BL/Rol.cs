using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Rols.FromSqlRaw("RolGetAll").ToList(); //contex.RolGetAll().ToList();

                    result.Objects = new List<object>();

                    if (contex != null)
                    {
                        foreach (var obj in RowsAfected)
                        {
                            ML.Rol rol = new ML.Rol();
                            rol.IdRol = obj.IdRol;
                            rol.Nombre = obj.Nombre;

                            result.Objects.Add(rol);
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

        public static ML.Result GetById(ML.Rol rol)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Rols.FromSqlRaw($"RolGetById {rol.IdRol}").AsEnumerable().FirstOrDefault();   //RolGetById(rol.IdRol).FirstOrDefault();

                    result.Object = new object();
                    if (RowsAfected != null)
                    {

                        rol.IdRol = RowsAfected.IdRol;
                        rol.Nombre = RowsAfected.Nombre;
                        result.Object = rol;

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
