using Microsoft.EntityFrameworkCore;

namespace CPAU.RevistaNotas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Revista> Revistas { get; set; }

        public virtual DbSet<RevistaContenido> RevistaContenidos { get; set; }

        public virtual DbSet<Contenido> Contenidos { get; set; }

        public virtual DbSet<ContenidoAutor> ContenidoAutores { get; set; }

        public virtual DbSet<Autor> Autores { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<ContenidoTag> ContenidoTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
