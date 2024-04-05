using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Security.Cryptography;
using webApiPractica.Models;
namespace PracticaMVC00.Models
{
    public class EquiposDbContext : DbContext
    {
        public EquiposDbContext(DbContextOptions options) : base(options) 
        { 
        
        }
        
        public DbSet<equipos> equipos { get; set; }
        public DbSet<estados_equipo> estados_equipo { get; set; }
        public DbSet<marcas> marcas { get; set; }
        public DbSet<tipo_equipo> tipo_equipo { get; set; }



    }
}
