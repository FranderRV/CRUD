using System;
using System.Collections.Generic;

namespace GISSA.Models
{
    public partial class TestUsuario
    {
     
        public int IdUsuario { get; set; }
        public string TipoUsuario { get; set; } = null!;
        public string TipoIdentificacion { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string Salto { get; set; } = null!;
        public string Correo { get; set; } = null!;

        public virtual List<TestUsuarioHabilidadesBlanda> ListaUsuarioHabilidadesBlanda { get; set; }
        public virtual List<TestUsuariosTelefono> ListaUsuariosTelefonos { get; set; }
    }
}
