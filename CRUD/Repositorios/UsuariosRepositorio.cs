using GISSA.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GISSA.Services
{
    public class UsuariosRepositorio
    {
        private readonly string _conexion;

        public UsuariosRepositorio(IConfiguration configuracion)
        {
            _conexion = configuracion.GetConnectionString("defaultConnection");
        }

        private TestUsuario Mapeo(SqlDataReader reader)
        {
            return new TestUsuario()
            {
                IdUsuario = reader.GetInt32(0),
                TipoUsuario = reader.GetString(1),
                TipoIdentificacion = reader.GetString(2),
                Cedula = reader.GetString(3),
                NombreCompleto = reader.GetString(4),
                NombreUsuario = reader.GetString(5),
                Clave = reader.GetString(6),
                Salto = reader.GetString(7),
                Correo = reader.GetString(8),
            };
        }

        public List<TestUsuario> ObtenerUsuarios()
        {
            var data = new List<TestUsuario>();
            try 
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "EXEC test_obtener_usuarios";

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
        public TestUsuario ObtenerCredencialesUsuario(int id)
        {
            var data = new TestUsuario();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "test_obtener_credenciales_usuario";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id",id));
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               data = new TestUsuario()
                                {
                                    IdUsuario = reader.GetInt32(0),
                                    NombreUsuario = reader.GetString(1),
                                    TipoUsuario = reader.GetString(2), 
                                    Clave = reader.GetString(3),
                                    Correo = reader.GetString(4),
                                    Salto = reader.GetString(5),
                                };
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

        public TestUsuario UsuarioLogin(string nombreUsuario)
        {
            var data = new TestUsuario();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "test_usuario_login";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@nombreUsuario", nombreUsuario));
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data = new TestUsuario()
                                {
                                    NombreUsuario = reader.GetString(0), 
                                    Clave = reader.GetString(1),
                                    Salto = reader.GetString(2), 
                                    TipoUsuario = reader.GetString(3), 
                                };
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

        public TestUsuario ObtenerUsuario(int id)
        {
            var data = new TestUsuario();
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "EXEC test_obtener_usuario @id";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        cmd.CommandType = CommandType.Text;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data = Mapeo(reader);
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

        public int GuardarUsuario(TestUsuario usuario)
         {
            var data = 0;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    //string sp = "EXEC test_insertar_usuario @tipoUsuario, @tipoIdentificacion, @cedula, " +
                    //    "@nombreCompleto, @nombreUsuario, @clave, @correo ";
                    string sp = "test_agregar_usuario";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@tipoUsuario", usuario.TipoUsuario));
                        cmd.Parameters.Add(new SqlParameter("@tipoIdentificacion", usuario.TipoIdentificacion));
                        cmd.Parameters.Add(new SqlParameter("@cedula", usuario.Cedula));
                        cmd.Parameters.Add(new SqlParameter("@nombreCompleto", usuario.NombreCompleto));
                        cmd.Parameters.Add(new SqlParameter("@nombreUsuario", usuario.NombreUsuario));
                        cmd.Parameters.Add(new SqlParameter("@clave", usuario.Clave));
                        cmd.Parameters.Add(new SqlParameter("@salto", usuario.Salto));
                        cmd.Parameters.Add(new SqlParameter("@correo", usuario.Correo)); 
                        
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.ExecuteNonQuery();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                data = reader.GetInt32(0);
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
        public bool ActualizarUsuario(TestUsuario usuario)
        {
            var data = false;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "test_actualizar_usuario";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@idUsuario", usuario.IdUsuario));
                        cmd.Parameters.Add(new SqlParameter("@tipoUsuario", usuario.TipoUsuario));
                        cmd.Parameters.Add(new SqlParameter("@tipoIdentificacion", usuario.TipoIdentificacion));
                        cmd.Parameters.Add(new SqlParameter("@cedula", usuario.Cedula));
                        cmd.Parameters.Add(new SqlParameter("@nombreCompleto", usuario.NombreCompleto));
                        cmd.Parameters.Add(new SqlParameter("@nombreUsuario", usuario.NombreUsuario));
                        cmd.Parameters.Add(new SqlParameter("@clave", usuario.Clave));
                        cmd.Parameters.Add(new SqlParameter("@salto", usuario.Salto));
                        cmd.Parameters.Add(new SqlParameter("@correo", usuario.Correo));

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();

                    }
                    sqlcon.Close();
                }
                data = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return data;
        }
        public bool EliminarUsuario(int id)
        {
            var data = false;
            try
            {
                using (SqlConnection sqlcon = new SqlConnection(_conexion))
                {
                    sqlcon.Open();
                    string sp = "EXEC test_eliminar_usuario @id";

                    using (SqlCommand cmd = new SqlCommand(sp, sqlcon))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));


                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    sqlcon.Close();
                }
                data = true;
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return data;
        }
    }
}
