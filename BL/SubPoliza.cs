using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubPoliza
    {


        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexcionString()))
                {
                    //var query = "SELECT [IdPoliza],[Nombre],[IdSubPoliza],[NumeroPoliza],[FechaCreacion],[FechaModificacion],[IdUsuario] FROM [Poliza]";
                    var query = "SubPolizaGetAll";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableSubPoliza = new DataTable();

                            da.Fill(tableSubPoliza);

                            if (tableSubPoliza.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                foreach (DataRow row in tableSubPoliza.Rows)
                                {
                                    ML.SubPoliza subPoliza = new ML.SubPoliza();
                                    subPoliza.IdSubPoliza = byte.Parse(row[0].ToString());
                                    subPoliza.Nombre = row[1].ToString();
                                   

                                    result.Objects.Add(subPoliza);
                                }

                                result.Correct = true;

                            }
                            else
                            {
                                result.Correct = false;
                                Console.WriteLine("Ocurrio un error, No se puede mostrar la tabla");

                            }
                        }
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

        public static ML.Result GetById(ML.SubPoliza subPoliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexcionString()))
                {
                    string query = "SubPolizaGetById";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdSubPoliza", subPoliza.IdSubPoliza);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tableUsuario = new DataTable();

                            da.Fill(tableUsuario);

                            if (tableUsuario.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                foreach (DataRow row in tableUsuario.Rows)
                                {

                                    subPoliza.IdSubPoliza = Byte.Parse(row[0].ToString());
                                    subPoliza.Nombre = row[1].ToString();
                                   

                                    result.Objects.Add(subPoliza);

                                }
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                Console.WriteLine("Ocurrio un error");
                            }

                        }
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
