using Microsoft.EntityFrameworkCore;
using Notes.Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Db.Implementation
{
    /// <summary>
    ///  Provider for Unit testing
    /// </summary>
    public class InMemoiyDbProvider : IDbProvider
    {
        private string dbName;
        public void ConfigureDb(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(dbName);
        }

        public InMemoiyDbProvider(string dbName)
        {
            this.dbName = dbName;
        }

    }
}
