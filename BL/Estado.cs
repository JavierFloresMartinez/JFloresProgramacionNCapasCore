using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result EstadoGetByIdPais(int idPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JfloresProgramacionNcapasContext contex = new DL.JfloresProgramacionNcapasContext())
                {
                    var RowsAfected = contex.Estados.FromSqlRaw($"EstadoGetByIdPais {idPais}").ToList();//EstadoGetByIdPais(idPais).ToList();

                    result.Objects = new List<object>();


                    foreach (var obj in RowsAfected)
                    {
                        ML.Estado estado = new ML.Estado();
                        estado.IdEstado = obj.IdEstado;
                        estado.Nombre = obj.Nombre;
                        estado.Pais = new ML.Pais();
                        estado.Pais.IdPais = obj.IdPais.Value;
                        result.Objects.Add(estado);
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
