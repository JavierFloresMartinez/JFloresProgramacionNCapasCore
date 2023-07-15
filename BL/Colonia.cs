using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result ColoniaGetByIMunicipio(int idMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio {idMunicipio}").ToList();//ColoniaGetByIdMunicipio(idMunicipio).ToList();

                    result.Objects = new List<object>();


                    foreach (var obj in RowsAfected)
                    {
                        ML.Colonia colonia = new ML.Colonia();
                        colonia.IdColonia = obj.IdColonia;
                        colonia.Nombre = obj.Nombre;
                        colonia.Municipio = new ML.Municipio();
                        colonia.Municipio.IdMunicipio = obj.IdMunicipio.Value;
                        result.Objects.Add(colonia);
                    }
                    result.Correct = true;

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
