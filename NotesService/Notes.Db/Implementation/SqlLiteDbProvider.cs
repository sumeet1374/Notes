using Microsoft.EntityFrameworkCore;
using Notes.Db.Interfaces;

namespace Notes.Db.Implementation
{
    public class SqlLiteDbProvider : IDbProvider
    {
        private readonly string connectionString;

        public void ConfigureDb(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }

        public SqlLiteDbProvider(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}
