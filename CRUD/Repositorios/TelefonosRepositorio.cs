using GISSA.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GISSA.Repositorios
{
    public class TelefonosRepositorio
    {
        private readonly string _conexion;

        public TelefonosRepositorio(IConfiguration configuracion)
        {
            _conexion = configuracion.GetConnectionString("defaultConnection");
        }


        private TestUsuariosTelefono Mapeo(SqlDataReader reader)
        {
            return new TestUsuariosTelefono()
            {
                Id = reader.GetInt32(0),
                Telefono = reader.GetString(1), 
            };
        }

        public List<TestUsuariosTelefono> ObtenerTelefonosUsuario(int id)
        {
            var data = new List<TestUsuariosTelefono>();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "test_obtener_telefono_usuario";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data.Add(Mapeo(reader));
                            }
                        }
                    }
                    sqlcon.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return data;
        }

        public bool AgregarTelefonosUsuario(TestUsuariosTelefono telefono)
        {
            var data = false;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "test_agregar_telefono_usuario";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", telefono.IdUsuario));
                        cmd.Parameters.Add(new SqlParameter("@telefono", telefono.Telefono));
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                    }
                    sqlcon.Close();
                    data = true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return data;
        }

        public bool EliminarTelefonosUsuario(int idUsuario)
        {
            var data = false;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "test_eliminar_telefonos_usuario";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", idUsuario));
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                    }
                    sqlcon.Close();
                    data = false;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return data;
        }
    }
}
