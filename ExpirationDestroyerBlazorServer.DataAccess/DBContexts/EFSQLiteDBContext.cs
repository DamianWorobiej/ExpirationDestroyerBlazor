using ExpirationDestroyerBlazorServer.DataAccess.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ExpirationDestroyerBlazorServer.DataAccess.DBContexts
{
    public class EFSQLiteDBContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public EFSQLiteDBContext(DbContextOptions<EFSQLiteDBContext> options) : base(options)
        {
        }

        //public EFSQLiteDBContext() : base()
        //{

        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "ExpirationDestroyer.db" };
        //    var connectionString = connectionStringBuilder.ToString();
        //    var connection = new SqliteConnection(connectionString);

        //    optionsBuilder.UseSqlite(connection);
        //}
    }
}
