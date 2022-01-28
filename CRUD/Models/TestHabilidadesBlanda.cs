using System;
using System.Collections.Generic;

namespace GISSA.Models
{
    public partial class TestHabilidadesBlanda
    {
        public TestHabilidadesBlanda()
        {
            TestUsuarioHabilidadesBlanda = new HashSet<TestUsuarioHabilidadesBlanda>();
        }

        public int IdHabilidad { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<TestUsuarioHabilidadesBlanda> TestUsuarioHabilidadesBlanda { get; set; }
    }
}
