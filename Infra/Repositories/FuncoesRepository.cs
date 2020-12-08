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
    public class FuncoesRepository : IFuncoesRepository
    {
        private readonly DataContext _context;

        public FuncoesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Funcoes>> ObterTodos()
        {
            return await _context.Funcoes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CriarFuncao(Funcoes funcoes)
        {
            _context.Add(funcoes);
            await _context.SaveChangesAsync();
        }

        public async Task<Funcoes> ObterPorNome(string nome)
        {
            return await _context.Funcoes
                .AsNoTracking()
                .Where(p => p.Nome == nome)
                .FirstOrDefaultAsync();
        }

        public async Task<string> ObterNomePorId(string id)
        {
            return await _context.Funcoes
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => p.Nome)
                .FirstOrDefaultAsync();
        }
    }
}
