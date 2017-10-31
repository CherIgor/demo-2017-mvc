using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data.Models.Entity;
using Demo.Services.Models;

namespace Demo.Services.Services
{
    public static class UsersService
    {
        public static async Task<IEnumerable<UserTextModel>> GetUserTexts()
        {
            var res = await new Entities().UserTexts
                .AsNoTracking()
                .OrderByDescending(x => x.Added)
                .Select(UserTextModel.FromEntity)
                .ToListAsync();
            return res;
        }

        public static async Task AddUserTextAsync(UserTextModel model, bool checkUniqueUserName)
        {
            var context = new Entities();

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