using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Usuarios.DAL.Repository;
using Usuarios.Models;

namespace Usuarios.BLL.Service
{
	public class UsuarioService : IUsuarioService
	{
		private readonly IGenericRepository<Usuario> _usuarioRepo;

		public UsuarioService(IGenericRepository<Usuario> usuarioRepo)
        {
				_usuarioRepo = usuarioRepo;
        }
        public async Task<bool> Actualizar(Usuario modelo)
		{
			return await _usuarioRepo.Actualizar(modelo);
		}

		public async Task<bool> Eliminar(int id)
		{
			return await _usuarioRepo.Eliminar(id);
		}

		public async Task<bool> Insertar(Usuario modelo)
		{
			return await _usuarioRepo.Insertar(modelo);
		}

		public async Task<Usuario> Obtener(int id)
		{
			return await _usuarioRepo.Obtener(id);
		}

		public async Task<Usuario> ObtenerPorNombre(string nombreUsuario)
		{
			IQueryable<Usuario> queryUsuarioSQL = await _usuarioRepo.ObtenerTodos();
			Usuario usuario = queryUsuarioSQL.Where(u => u.Nombre == nombreUsuario).FirstOrDefault();
			return usuario;
		}

		public async Task<IQueryable<Usuario>> ObtenerTodos()
		{
			return await _usuarioRepo.ObtenerTodos();
		}

    }
}
