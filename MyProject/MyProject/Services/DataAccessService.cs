using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class DataAccessService : DbContext
    {
        #region TABLES
        public DbSet<Titan> Titans { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        #region CONSTRUCTOR    
        public DataAccessService(DbContextOptions<DataAccessService> options) : base(options)
        {

            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Titan>()
                .HasKey(x => x.IdTitan);

            modelBuilder.Entity<User>()
                .HasKey(x => x.IdUser);

            modelBuilder.Entity<Titan>()
                //Indique que chaque instance de Titan a une relation avec une instance de User via la propriété de navigation User.
                .HasOne(e => e.User)
                //Indique que chaque User peut avoir plusieurs Titans via la propriété de navigation MyCollection.
                .WithMany(e => e.MyCollection)
                //Spécifie que la clé étrangère dans la table Titan qui fait référence à la table User est UserId.
                .HasForeignKey(e => e.IdUser)
                //Indique que la clé principale dans la table User est IdUser, ce qui correspond à la clé étrangère dans la table Titan.
                .HasPrincipalKey(e => e.IdUser);
        }
    }
}
