using System;
using System.Collections.Generic;

namespace GISSA.Models
{
    public partial class TestUsuarioHabilidadesBlanda
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdHabilidad { get; set; }

        public virtual TestHabilidadesBlanda IdHabilidadNavigation { get; set; } = null!;
        public virtual TestUsuario IdUsuarioNavigation { get; set; } = null!;
    }
}
