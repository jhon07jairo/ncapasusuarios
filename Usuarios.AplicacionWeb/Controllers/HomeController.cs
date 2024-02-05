using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;
using Usuarios.AplicacionWeb.Models;
using Usuarios.AplicacionWeb.Models.ViewModels;
using Usuarios.BLL.Service;
using Usuarios.Models;


namespace Usuarios.AplicacionWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly IUsuarioService _usuarioService;

		public HomeController(IUsuarioService usuarioServ)
		{
			_usuarioService = usuarioServ;
		}

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
		public async Task<IActionResult> Lista()
		{
            IQueryable<Usuario> queryUsuarioSQL = await _usuarioService.ObtenerTodos();


                List<VMUsuario> lista = queryUsuarioSQL
               .Select(u => new VMUsuario()
				{
					IdUsuario = u.IdUsuario,
					Nombre = u.Nombre,
                    FechaNacimiento = u.FechaNacimiento.Value.ToString("yyyy-MM-dd"),
                    //FechaNacimiento = u.FechaNacimiento,
                    Sexo = u.Sexo
				})
                .ToList();
                

             return StatusCode(StatusCodes.Status200OK, lista);


        }

		[HttpPost]
		public async Task<IActionResult> Insertar([FromBody] VMUsuario modelo)
		{
			Usuario NuevoModelo = new Usuario()
			{
				Nombre = modelo.Nombre,
                FechaNacimiento = DateTime.ParseExact(modelo.FechaNacimiento, "yyyy-MM-dd", CultureInfo.CreateSpecificCulture("es-CO")),
                //FechaNacimiento = modelo.FechaNacimiento,
                Sexo = modelo.Sexo
			};

			bool respuesta = await _usuarioService.Insertar(NuevoModelo);

			return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
		}

		[HttpPut]
		public async Task<IActionResult> Actualizar([FromBody] VMUsuario modelo)
		{
			Usuario NuevoModelo = new Usuario()
			{
				IdUsuario = modelo.IdUsuario,
				Nombre = modelo.Nombre,
                FechaNacimiento = DateTime.ParseExact(modelo.FechaNacimiento, "yyyy-MM-dd", CultureInfo.CreateSpecificCulture("es-CO")),
                //FechaNacimiento = modelo.FechaNacimiento,
                Sexo = modelo.Sexo
			};

			bool respuesta = await _usuarioService.Actualizar(NuevoModelo);

			return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
		}

		[HttpDelete]
		public async Task<IActionResult> Eliminar(int id)
		{
			bool respuesta = await _usuarioService.Eliminar(id);

			return StatusCode(StatusCodes.Status200OK, new { valor = respuesta });
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

        [HttpGet]
        public async Task<IActionResult> DownloadExcel()
        {
            IQueryable<Usuario> queryUsuarioSQL = await _usuarioService.ObtenerTodos();

            List<VMUsuario> lista = queryUsuarioSQL
                .Select(u => new VMUsuario()
                {
                    IdUsuario = u.IdUsuario,
                    Nombre = u.Nombre,
                    FechaNacimiento = u.FechaNacimiento.Value.ToString("yyyy-MM-dd"),
                    Sexo = u.Sexo
                })
                .ToList();

            var stream = new System.IO.MemoryStream();

            using (var package = new OfficeOpenXml.ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Usuarios");

                worksheet.Cells.LoadFromCollection(lista, true);

                package.Save();
            }

            stream.Position = 0;

            string excelName = $"Usuarios_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }

    }
}