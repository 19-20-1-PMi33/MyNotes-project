using System.Data.Entity;

namespace MyNotes
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {
        }
        public DbSet<Note> Notes { get; set; }
    }
}