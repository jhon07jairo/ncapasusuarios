namespace Usuarios.AplicacionWeb.Models.ViewModels
{
	public class VMUsuario
	{
		public int IdUsuario { get; set; }
		public string? Nombre { get; set; }
        //public DateTime? FechaNacimiento { get; set; }
        public string? FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
	}
}
