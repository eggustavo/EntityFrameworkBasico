using System.Data.Entity;
using Exemplo01.Domain;

namespace Exemplo01.Context
{
    public class ExemploContext : DbContext
    {
        public ExemploContext()
            : base("ExemploContextConnectionString")
        {
        }

        public DbSet<Curso> CursoSet { get; set; }
        public DbSet<CursoDisciplina> CursoDisciplinaSet { get; set; }
    }
}