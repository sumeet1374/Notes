using Microsoft.EntityFrameworkCore;
using Notes.Db.Interfaces;
using Notes.Db.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Db
{
    public class NotesDbContext : DbContext
    {
        private readonly IDbProvider provider;

        public DbSet<User> Users { get; set; }
        public DbSet<NoteInfo> Notes { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            provider.ConfigureDb(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("User");
            builder.Entity<NoteInfo>().ToTable("NotesInfo");


        }
        public NotesDbContext(IDbProvider provider)
        {
            this.provider = provider;
        }
        /// <summary>
        ///  Only used for testing
        /// </summary>
        public async Task ClearDb()
        {
           await this.Database.EnsureDeletedAsync();
        }
    }
}
