using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Aanchal_NoteBook.Data;
using My_Aanchal_NoteBook.Models;
using My_Aanchal_NoteBook.Repository.Interface;

namespace My_Aanchal_NoteBook.Repository.Implementation
{
    public class RegisterationImplementation : IRegisteration
    {
        private readonly ApplicationDbContext context;

        public RegisterationImplementation(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task SignUpUser(User model)
        {
            await context.Users.AddAsync(model);
            model.CreatedOn = DateTime.Now;
            model.CreatedBy = "Gaj";
            model.Otp = "1025";
            model.OtpCreatedOn = DateTime.Now;
            model.OtpEndsOn = DateTime.Now;
            await context.SaveChangesAsync();
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            return await context.Users.FirstOrDefaultAsync(p => p.PhoneNumber == phoneNumber);
        }

        public async Task<IEnumerable<User>> Users(User model, int id)
        {
            var userData = await context.Users.Where(e => e.UserId == id).ToListAsync();
            return userData;
        }

        public async Task<User> GetById(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task Update(User model)
        {
            model.CreatedBy = "Gaj";
            model.CreatedOn = DateTime.Today;
            model.Otp = "8474";
            model.OtpCreatedOn = DateTime.Today;
            model.OtpEndsOn = DateTime.Today;
            context.Users.Update(model);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserById(int id)
        {
            var userId = await context.Users.FindAsync(id);
            if (userId != null)
            {
                context.Users.Remove(userId);
                await context.SaveChangesAsync();
            }
        }
    }
}
