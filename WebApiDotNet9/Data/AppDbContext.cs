using Microsoft.EntityFrameworkCore;
using WebApiDotNet9.Models;

namespace WebApiDotNet9.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
            
        }

        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<AuditoriaModel> Auditorias { get; set; }
    }
}
