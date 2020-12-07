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
    public class ItensRepository: IItensRepository
    {
        private readonly DataContext _context;

        public ItensRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Itens>> ObterTodos()
        {
            return await _context.Itens
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task CriarItem(Itens item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
        }
    }
}
