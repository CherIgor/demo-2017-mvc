using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Services.Models;

namespace Demo.Services.IServices
{
    public interface IUsersService
    {
        Task<IEnumerable<UserTextModel>> GetUserTexts();
        Task AddUserTextAsync(UserTextModel model, bool checkUniqueUserName);
    }
}