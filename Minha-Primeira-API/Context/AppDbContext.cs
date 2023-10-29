


using Microsoft.EntityFrameworkCore;
using Minha_Primeira_API.Model;

namespace Minha_Primeira_API
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Produto>? Produtos { get; set; }
    }
}
