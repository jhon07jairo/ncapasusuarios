using System;
using System.Collections.Generic;

namespace Usuarios.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
    }
}
