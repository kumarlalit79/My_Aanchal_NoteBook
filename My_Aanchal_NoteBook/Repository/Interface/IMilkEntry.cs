using My_Aanchal_NoteBook.Models;

namespace My_Aanchal_NoteBook.Repository.Interface
{
    public interface IMilkEntry
    {
        Task<IEnumerable<MilkEntry>> GetAllRecord(User model , int id);
        Task<MilkEntry> GetMilkEntryById(int id);
        Task CreateMilkEntry(MilkEntry model , int userId);

        Task UpdateMilkEntry(MilkEntry model);
        Task DeleteMilkEntryById(int id);

        

    }
}
