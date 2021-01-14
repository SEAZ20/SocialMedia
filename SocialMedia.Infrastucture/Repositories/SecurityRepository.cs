using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastucture.Data;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class SecurityRepository :  ISecurityRepository
    {
        private readonly SocialMediaContext _context;
        public SecurityRepository(SocialMediaContext context) {
            context = _context;
        }

        public async Task<Security> GetLoginByCredentials(UserLogin login)
        {
            return await _context.FirstOrDefaultAsync(x => x.User == login.User);
        }
    }
}