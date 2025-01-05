using Microsoft.EntityFrameworkCore;
using My_Aanchal_NoteBook.Data;
using My_Aanchal_NoteBook.Models;
using My_Aanchal_NoteBook.Repository.Interface;

namespace My_Aanchal_NoteBook.Repository.Implementation
{
    public class MilkEntryImplementation : IMilkEntry
    {
        private readonly ApplicationDbContext context;

        public MilkEntryImplementation(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<MilkEntry>> GetAllRecord(User model , int id)
        {
            var milkRecord = await context.MilkEntries.Where(x => x.UserId == id).ToListAsync();
            return milkRecord;
        }

        public async Task<MilkEntry> GetMilkEntryById(int id)
        {
            return await context.MilkEntries.FindAsync(id);
        }
        public async Task CreateMilkEntry(MilkEntry model , int userId)
        {
            model.UserId = userId;
            model.CreatedOn = DateTime.Now;
            
            await context.MilkEntries.AddAsync(model);
            await context.SaveChangesAsync();
        }

        public async Task UpdateMilkEntry(MilkEntry model, int userId)
        {
            model.UserId = userId;
            model.CreatedOn = DateTime.Now;
            context.MilkEntries.Update(model);
            await context.SaveChangesAsync();
        }
        public async Task DeleteMilkEntryById(int id)
        {
            var milkId = await context.MilkEntries.FindAsync(id);
            if (milkId != null)
            {
                context.MilkEntries.Remove(milkId);
                await context.SaveChangesAsync();
            }
        }

        
    }
}
