using GISSA.Services;

namespace GISSA.Repositorios
{
    public class RepositorioCentral
    {
        private UsuariosRepositorio _usuariorepo;
        private TelefonosRepositorio _usuariotelefonosrepo;
        private HabilidadesRepositorio _habilidadesrepo;
        public RepositorioCentral(UsuariosRepositorio usuariorepo, TelefonosRepositorio usuariotelefonosrepo, HabilidadesRepositorio habilidadesrepo)
        {
            _usuariorepo = usuariorepo;
            _usuariotelefonosrepo = usuariotelefonosrepo;
            _habilidadesrepo = habilidadesrepo;
        }
        public UsuariosRepositorio getUsuariosRepositorio()
        {
            return _usuariorepo;
        }
        public TelefonosRepositorio getTelefonosRepositorio()
        {
            return _usuariotelefonosrepo;
        }
        public HabilidadesRepositorio getHabilidadesRepositorio()
        {
            return _habilidadesrepo;
        }


    }
}
