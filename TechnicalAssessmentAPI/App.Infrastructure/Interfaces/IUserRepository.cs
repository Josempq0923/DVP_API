using App.Config.Classes.Base;
using App.Config.Helpers;
using App.Infrastructure.Database.Entities;

namespace App.Infrastructure.Interfaces
{
    public interface IUserRepository : IBaseRepository<Users>
    {
        Users LoginUser(string name);
    }
}
