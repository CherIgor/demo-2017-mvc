using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data.Models.Entity;
using Demo.Services.IServices;
using Demo.Services.Models;

namespace Demo.Services.Services
{
    public class UsersService : IUsersService
    {
        public async Task<IEnumerable<UserTextModel>> GetUserTexts()
        {
            var res = await new DemoContext().UserTexts
                .AsNoTracking()
                .OrderByDescending(x => x.Added)
                .Select(UserTextModel.FromEntity)
                .ToListAsync();
            return res;
        }

        public async Task AddUserTextAsync(UserTextModel model, bool checkUniqueUserName)
        {
            var context = new DemoContext();

            if (checkUniqueUserName)
            {
                var nameLowercased = model.Name.ToLower();
                if (await context.UserTexts.AsNoTracking().AnyAsync(x => x.Name.ToLower() == nameLowercased))
                {
                    throw new UserAlreadyExistsException();
                }
            }

            context.UserTexts.Add(new UserText
            {
                Name = model.Name,
                Text = model.Text,
                Added = DateTimeOffset.UtcNow
            });
            await context.SaveChangesAsync();
        }

        public class UserAlreadyExistsException : Exception
        {
        }
    }
}