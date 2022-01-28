using GISSA.Models;
using GISSA.Repositorios;
using GISSA.Services;
using Microsoft.AspNetCore.Mvc;
using SimpleLogin.Helper;

namespace GISSA.Controllers
{

    public class UsuarioController : Controller
    {
        private UsuariosRepositorio _usuariorepo;
        private TelefonosRepositorio _usuariotelefonosrepo;
        private HabilidadesRepositorio _habilidadesrepo;
        //private RepositorioCentral _repositorioCentral;
        public UsuarioController(UsuariosRepositorio usuariorepo, TelefonosRepositorio usuariotelefonosrepo, HabilidadesRepositorio habilidadesrepo)
        {
            _usuariorepo = usuariorepo;
            _usuariotelefonosrepo = usuariotelefonosrepo;
            _habilidadesrepo = habilidadesrepo;
        }

        public IActionResult Registrar(TestUsuario usuario, string[] telefonos, int[] habilidades)
        {
            var hash = HashHelper.Hash(usuario.Clave);
            usuario.Clave = hash.Password;
            usuario.Salto = hash.Salt;
            var idUsuario = _usuariorepo.GuardarUsuario(usuario);

            if (idUsuario > 0)
            {
                foreach (var telefono in telefonos)
                {
                    _usuariotelefonosrepo.AgregarTelefonosUsuario(new TestUsuariosTelefono()
                    {
                        IdUsuario = idUsuario,
                        Telefono = telefono
                    });
                }

                foreach (var habilidad in habilidades)
                {
                    _habilidadesrepo.AgregarHabilidadesBlandasUsuario(new TestUsuarioHabilidadesBlanda()
                    {
                        IdHabilidad = habilidad,
                        IdUsuario = idUsuario
                    });
                }
            } 

            return RedirectToAction("Usuarios", "Dashboard");

        }
        public IActionResult Actualizar(TestUsuario usuario, int[] telefonos, int[] habilidades)
        {
            if (usuario.Clave is not null)
            {
                var hash = HashHelper.Hash(usuario.Clave);
                usuario.Clave = hash.Password;
                usuario.Salto = hash.Salt;
            }
            else
            {
                var credenciales = _usuariorepo.ObtenerCredencialesUsuario(usuario.IdUsuario);
                usuario.Clave = credenciales.Clave;
                usuario.Salto = credenciales.Salto;
            }

            var idUsuario = _usuariorepo.ActualizarUsuario(usuario);

            if (idUsuario)
            {
                _usuariotelefonosrepo.EliminarTelefonosUsuario(usuario.IdUsuario);
                foreach (var telefono in telefonos)
                {
                    _usuariotelefonosrepo.AgregarTelefonosUsuario(new TestUsuariosTelefono()
                    {
                        IdUsuario = usuario.IdUsuario,
                        Telefono = telefono+""
                    });
                }
                _habilidadesrepo.EliminarHabilidadesBlandasUsuario(usuario.IdUsuario);
                foreach (var habilidad in habilidades)
                {
                    _habilidadesrepo.AgregarHabilidadesBlandasUsuario(new TestUsuarioHabilidadesBlanda()
                    {
                        IdHabilidad = habilidad,
                        IdUsuario = usuario.IdUsuario
                    });
                }
            }

            return RedirectToAction("Usuarios", "Dashboard");
        }

        public IActionResult Eliminar(int id)
        {
           _usuariorepo.EliminarUsuario(id);
 
            return RedirectToAction("Usuarios", "Dashboard");
        }


    }
}
