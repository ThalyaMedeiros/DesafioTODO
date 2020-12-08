using Domain.Context.Entities;
using Domain.Repositories;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly DataContext _context;

        public UsuariosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Usuarios>> ObterTodos()
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

        public async Task<Usuarios> LoginUsuario(string email, string senha)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.Email == email && p.Senha == senha)
                .SingleOrDefaultAsync();
        }

        public async Task<string> ObterFuncaoIdPorUsuarioId(string usuarioId)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.Id == usuarioId)
                .Select(p => p.FuncaoId)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuarios> ObterPorId(string usuarioId)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.Id == usuarioId)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuarios> ObterPorEmail(string email)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.Email == email)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> EmailExistente(string email)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Where(p => p.Email == email)
                .AnyAsync();
        }
    }
}
