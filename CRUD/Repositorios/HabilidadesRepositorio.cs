using GISSA.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GISSA.Repositorios
{
    public class HabilidadesRepositorio
    {

        private readonly string _conexion;

        public HabilidadesRepositorio(IConfiguration configuracion)
        {
            _conexion = configuracion.GetConnectionString("defaultConnection");
        }

        private TestHabilidadesBlanda Mapeo(SqlDataReader reader)
        {
            return new TestHabilidadesBlanda()
            {
                IdHabilidad = reader.GetInt32(0),
                Nombre = reader.GetString(1),
            };
        }

        public List<TestHabilidadesBlanda> ObtenerHabilidadesBlandas()
        {
            var data = new List<TestHabilidadesBlanda>();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "EXEC test_obtener_habilidades_blandas";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.CommandType = CommandType.Text;

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

        public List<TestHabilidadesBlanda> ObtenerHabilidadesBlandasUsuario(int id)
        {
            var data = new List<TestHabilidadesBlanda>();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "EXEC test_obtener_habilidades_blandas_usuario @id";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.CommandType = CommandType.Text;

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

        public bool AgregarHabilidadesBlandasUsuario(TestUsuarioHabilidadesBlanda testUsuarioHabilidadesBlanda)
        {
            var data = false;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "test_agregar_habilidades_blandas_usuario";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", testUsuarioHabilidadesBlanda.IdUsuario));
                        cmd.Parameters.Add(new SqlParameter("@idHabilidad", testUsuarioHabilidadesBlanda.IdHabilidad));
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

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

        public bool EliminarHabilidadesBlandasUsuario(int id)
        {
            var data = false;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "EXEC test_eliminar_habilidades_blandas_usuario @id";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.CommandType = CommandType.Text;
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
