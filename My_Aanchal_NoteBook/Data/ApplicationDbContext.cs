using Microsoft.EntityFrameworkCore;
using My_Aanchal_NoteBook.Models;

namespace My_Aanchal_NoteBook.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MilkEntry> MilkEntries { get; set; }
    }
}
