using My_Aanchal_NoteBook.Models;

namespace My_Aanchal_NoteBook.Repository.Interface
{
    public interface IRegisteration
    {
        Task SignUpUser(User model);
        Task<User> GetUserByPhoneNumber(string phoneNumber);

        Task<IEnumerable<User>> Users(User model, int id);
        Task<User> GetById(int id);
        Task Update(User model);    
        Task DeleteUserById(int id);    

    }
}
