using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoMVC.Models
{
    //Context ira herdar DbContext
    public class Context : DbContext
    {
        //Tabela chamada Categorias, na execucao ira criar e basta referenciar o Context
        public DbSet<Categoria> Categorias{get; set;}
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //inserir a string de conexao
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=cursoMvc;Integrated Security=True");

        }
    }
}
