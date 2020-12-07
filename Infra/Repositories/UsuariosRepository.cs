using Domain.Context.Entities;
using Domain.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class UsuariosRepository:IUsuariosRepository
    {
        private readonly DataContext _context;

        public UsuariosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuarios>> ObterTodos()
        {
            return await _context.Usuarios
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CriarUsuario(Usuarios usuarios)
        {
            _context.Add(usuarios);
            await _context.SaveChangesAsync();
        }
    }
}
