using GISSA.Models;
using GISSA.Repositorios;
using GISSA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GISSA.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {

        private UsuariosRepositorio _usuariorepo;
        private TelefonosRepositorio _usuariotelefonosrepo;
        private HabilidadesRepositorio _habilidadesrepo;
        //private RepositorioCentral _repositorioCentral;
        public DashboardController(UsuariosRepositorio usuariorepo, TelefonosRepositorio usuariotelefonosrepo, HabilidadesRepositorio habilidadesrepo)
        {
            _usuariorepo = usuariorepo;
            _usuariotelefonosrepo = usuariotelefonosrepo;
            _habilidadesrepo = habilidadesrepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "RoleAdmin")]
        public IActionResult Usuarios()
        {
            ViewData["usuarios"] = _usuariorepo.ObtenerUsuarios();

            return View("Usuarios");
        }

        [Authorize(Policy = "RoleAdmin")]
        public IActionResult Registro()
        { 
            var usuario = new TestUsuario();
            ViewData["TestUsuario"] = usuario;
            ViewData["telefonosUsuarios"] = (usuario.ListaUsuariosTelefonos is null) ? new List<TestUsuariosTelefono>() : usuario.ListaUsuariosTelefonos;
            ViewData["habilidades"] = _habilidadesrepo.ObtenerHabilidadesBlandas();
            ViewData["habilidadesUsuario"] = new List<int>();

            return View("Registro");
        }
        [Authorize(Policy = "RoleAdmin")]
        public IActionResult Actualizar(int id)
        {
            var usuario = _usuariorepo.ObtenerUsuario(id);
            usuario.ListaUsuariosTelefonos = _usuariotelefonosrepo.ObtenerTelefonosUsuario(id);
            ViewData["TestUsuario"] = usuario;
            ViewData["telefonosUsuarios"] = (usuario.ListaUsuariosTelefonos is null) ? new List<TestUsuariosTelefono>() : usuario.ListaUsuariosTelefonos;
            ViewData["habilidades"] = _habilidadesrepo.ObtenerHabilidadesBlandas();
            var habilidadesUsuario = _habilidadesrepo.ObtenerHabilidadesBlandasUsuario(id);
            var listaIdHabilidadaes = new List<int>();
            
            if (habilidadesUsuario is not null)
            {
                foreach (var item in habilidadesUsuario)
                {
                    listaIdHabilidadaes.Add(item.IdHabilidad);
                }
            }
            ViewData["habilidadesUsuario"] = (habilidadesUsuario is null)?new List<int>(): listaIdHabilidadaes;
            var resp = (usuario.TipoUsuario is null) ? "" : (usuario.TipoUsuario.Equals("A") ? "selected" : "");
            return View("Registro");
        }

      

    }
}
