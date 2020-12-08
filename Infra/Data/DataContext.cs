using Domain.Context.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        //Tabelas
        public DbSet<Itens> Itens { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Funcoes> Funcoes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Itens>().HasKey(m => m.Id);
            builder.Entity<Usuarios>().HasKey(m => m.Id);
            builder.Entity<Funcoes>().HasKey(m => m.Id);

            base.OnModelCreating(builder);
        }
    }
}
