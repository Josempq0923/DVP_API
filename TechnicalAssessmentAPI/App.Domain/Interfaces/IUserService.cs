using App.Config.Classes.Base;
using App.Config.DTO;
using App.Config.Helpers;
using App.Infrastructure.Database.Entities;

namespace App.Domain.Interfaces
{
    public interface IUsersService : IBaseService<UsersDTO, Users>
    {
        Task<List<UsersDTO>> GetAllUsers();
        UsersDTO LoginUser(LoginDTO login);
        Task<int> CreateUser(UsersDTO user);

    }
}
