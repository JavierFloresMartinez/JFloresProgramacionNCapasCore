using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Poliza
    {

        public static ML.Result Add(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexcionString()))
                {
                    //string query = "INSERT INTO [Poliza]([Nombre],[IdSubPoliza],[NumeroPoliza],[IdUsuario]) VALUES (@Nombre,@IdSubPoliza,@NumeroPoliza,@IdUsuario)";
                    string query = "PolizaAdd";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = poliza.Nombre;

                        collection[1] = new SqlParameter("IdSubPoliza", SqlDbType.TinyInt);
                        collection[1].Value = poliza.IdSubPoliza;

                        collection[2] = new SqlParameter("NumeroPoliza", SqlDbType.VarChar);
                        collection[2].Value = poliza.NumeroPoliza;

                        collection[3] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[3].Value = poliza.Usuario.IdUsuario;

                        cmd.Parameters.AddRange(collection);

                        context.Open();

                        int RowsAfected = cmd.ExecuteNonQuery();

                        if (RowsAfected >= 1)
                        {
                            result.Correct = true;

                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrio Un Error Al Ingresar Al Aseguradora ";
                        }

                        context.Close();
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

        public static ML.Result Update(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexcionString()))
                {
                    string query = "PolizaUpdate";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPoliza", poliza.IdPoliza);

                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = poliza.Nombre;

                        collection[1] = new SqlParameter("IdSubPoliza", SqlDbType.TinyInt);
                        collection[1].Value = poliza.IdSubPoliza;

                        collection[2] = new SqlParameter("NumeroPoliza", SqlDbType.VarChar);
                        collection[2].Value = poliza.NumeroPoliza;

                        collection[3] = new SqlParameter("IdUsuario", SqlDbType.Int);
                        collection[3].Value = poliza.Usuario.IdUsuario;

                        cmd.Parameters.AddRange(collection);

                        context.Open();

                        int RowsAfected = cmd.ExecuteNonQuery();

                        if (RowsAfected > 0)
                        {

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            Console.WriteLine("La poliza no se actualizo correctamente");
                        }
                        context.Close();
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

        public static ML.Result Delete(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexcionString()))
                {
                    string query = "PolizaDelete";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdPoliza", poliza.IdPoliza);

                        context.Open();

                        int RowsAfected = cmd.ExecuteNonQuery();

                        if (RowsAfected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            Console.WriteLine("Error al eliminar la Poliza");
                        }

                        context.Close();

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
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexcionString()))
                {
                    //var query = "SELECT [IdPoliza],[Nombre],[IdSubPoliza],[NumeroPoliza],[FechaCreacion],[FechaModificacion],[IdUsuario] FROM [Poliza]";
                    var query = "PolizaGetAll";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablePoliza = new DataTable();

                            da.Fill(tablePoliza);

                            if (tablePoliza.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                foreach (DataRow row in tablePoliza.Rows)
                                {
                                    ML.Poliza poliza = new ML.Poliza();
                                    poliza.IdPoliza = int.Parse(row[0].ToString());
                                    poliza.Nombre =  row[1].ToString();
                                    poliza.SubPoliza = new ML.SubPoliza();
                                    poliza.SubPoliza.IdSubPoliza = byte.Parse(row[2].ToString());
                                    poliza.SubPoliza.Nombre = row[3].ToString();
                                    poliza.NumeroPoliza = row[4].ToString();
                                    poliza.FechaCreacion = row[5].ToString();
                                    poliza.FechaModificacion = row[6].ToString();
                                    poliza.Usuario = new ML.Usuario();
                                    poliza.Usuario.IdUsuario = int.Parse(row[7].ToString());
                                    poliza.Usuario.Nombre = row[8].ToString();
                                    poliza.Usuario.ApellidoPaterno = row[9].ToString();
                                    poliza.Usuario.ApellidoMaterno = row[10].ToString();
                                    poliza.Usuario.NombreCompleto = poliza.Usuario.Nombre + poliza.Usuario.ApellidoPaterno + poliza.Usuario.ApellidoMaterno;

                                    result.Objects.Add(poliza);
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
                result.ErrorMessage= ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(ML.Poliza poliza)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConexcionString()))
                {
                    string query = "PolizaGetById";

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdPoliza", poliza.IdPoliza);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablePoliza = new DataTable();

                            da.Fill(tablePoliza);

                            if (tablePoliza.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                foreach (DataRow row in tablePoliza.Rows)
                                {

                                    poliza.IdPoliza = int.Parse(row[0].ToString());
                                    poliza.Nombre = row[1].ToString();
                                    poliza.SubPoliza = new ML.SubPoliza();
                                    poliza.SubPoliza.IdSubPoliza = byte.Parse(row[2].ToString());
                                    poliza.SubPoliza.Nombre = row[3].ToString();
                                    poliza.NumeroPoliza = row[4].ToString();
                                    poliza.FechaCreacion = row[5].ToString();
                                    poliza.FechaModificacion = row[6].ToString();
                                    poliza.Usuario = new ML.Usuario();
                                    poliza.Usuario.IdUsuario = int.Parse(row[7].ToString());
                                    poliza.Usuario.Nombre = row[8].ToString();
                                    poliza.Usuario.ApellidoPaterno = row[9].ToString();
                                    poliza.Usuario.ApellidoMaterno = row[10].ToString();
                                    poliza.Usuario.NombreCompleto = row[8].ToString() + row[9].ToString() + row[10].ToString();

                                    result.Objects.Add(poliza);

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
