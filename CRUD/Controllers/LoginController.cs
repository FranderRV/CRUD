using GISSA.Models;
using GISSA.Repositorios;
using GISSA.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SimpleLogin.Helper;
using System.Security.Claims;

namespace GISSA.Controllers
{
    public class LoginController : Controller
    {
        private UsuariosRepositorio _usuariorepo;
        private TelefonosRepositorio _usuariotelefonosrepo;
        private HabilidadesRepositorio _habilidadesrepo;
        //private RepositorioCentral _repositorioCentral;
        public LoginController(UsuariosRepositorio usuariorepo, TelefonosRepositorio usuariotelefonosrepo, HabilidadesRepositorio habilidadesrepo)
        {
            _usuariorepo = usuariorepo;
            _usuariotelefonosrepo = usuariotelefonosrepo;
            _habilidadesrepo = habilidadesrepo;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }
        public async Task<IActionResult> Login(string usuarioNombre, string clave)
        {
            var datos = _usuariorepo.UsuarioLogin(usuarioNombre);

            if(datos.NombreUsuario is not null)
            {
                if (HashHelper.CheckHash(clave, datos.Clave, datos.Salto)) {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.Name, datos.NombreUsuario));
                    identity.AddClaim(new Claim("Tipo", datos.TipoUsuario));

                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                           new AuthenticationProperties { ExpiresUtc = DateTime.Now.AddDays(1), IsPersistent = true });

                    if (datos.TipoUsuario.Equals("C"))
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                    return RedirectToAction("Registro", "Dashboard");
                }
            }


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Registro()
        {
            var lista = new List<TestUsuarioHabilidadesBlanda>();
            var usuario = _usuariorepo.ObtenerUsuario(5);
            ViewData["TestUsuario"] = usuario;
            lista.Add(new TestUsuarioHabilidadesBlanda() { Id = 0, IdHabilidad = 1, IdUsuario = 1 });
            ViewData["telefonosUsuarios"] = (usuario.ListaUsuariosTelefonos is null) ? new List<TestUsuariosTelefono>() : usuario.ListaUsuariosTelefonos;

            ViewData["habilidades"] = _habilidadesrepo.ObtenerHabilidadesBlandas();
            ViewData["habilidadesUsuario"] = new List<int>();



            return View("Registro");
        }


        public IActionResult Registrar(TestUsuario usuario, string[] telefonos, int[] habilidades)
        {
            var claveTemporal = usuario.Clave;
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
            else
            {
                return RedirectToAction("Registro", "Login");
            }


           // return CreatedAtAction("Login", new { NombreUsuario = usuario.NombreCompleto, Clave = claveTemporal });
            return RedirectToAction("Login", "Login", new { UsuarioNombre = usuario.NombreUsuario,Clave=claveTemporal});
           
        }


    }
}
