using Microsoft.EntityFrameworkCore;

namespace Notes.Db.Interfaces
{
    /// <summary>
    ///  Specific Configirator for Specific Db Provider
    /// </summary>
    public interface IDbProvider
    {
        void ConfigureDb(DbContextOptionsBuilder optionsBuilder);
    }
}
