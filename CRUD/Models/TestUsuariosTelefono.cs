using System;
using System.Collections.Generic;

namespace GISSA.Models
{
    public partial class TestUsuariosTelefono
    {
        public int Id { get; set; }
        public string Telefono { get; set; }
        public int? IdUsuario { get; set; }

        public virtual TestUsuario? IdUsuarioNavigation { get; set; }
    }
}
