using Domain.Context.Entities;
using Domain.Context.Enums;
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
    public class ItensRepository: IItensRepository
    {
        private readonly DataContext _context;

        public ItensRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Itens>> ObterTodos()
        {
            return await _context.Itens
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<Itens>> ObterTodosItensAtrasados()
        {
            return await _context.Itens
                .Where(p => p.Status == EStatusItens.Atrasado)
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<List<Itens>> ObterTodosPorUsuarioId(string usuarioId)
        {
            return await _context.Itens
                .AsNoTracking()
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<List<Itens>> ObterTodosItensAtrasadosPorUsuarioId(string usuarioId)
        {
            return await _context.Itens
                .AsNoTracking()
                .Where(p => p.UsuarioId == usuarioId && p.Status == EStatusItens.Atrasado)
                .ToListAsync();
        }

        public async Task<Itens> ObterPorId(string id)
        {
            return await _context.Itens
                .AsNoTracking()
                .Where(p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task CriarItem(Itens item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarItem(Itens item)
        {
            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirItem(Itens item)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
