using Microsoft.EntityFrameworkCore;
using WMS.Application.Interfaces;
using WMS.Domain.Entities;
using WMS.Infrastructure.Data;

namespace WMS.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserLogin?> GetUserByUsernameAsync(string username)
        {
            return await _context.UserLogins
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUserAsync(UserLogin user)
        {
            await _context.UserLogins.AddAsync(user);

            await _context.SaveChangesAsync();
        }
    }
}