using App.Config.Classes.Base;
using App.Infrastructure.Database.Entities;

namespace App.Infrastructure.Interfaces
{
    public interface IPersonsRepository : IBaseRepository<Persons>
    {
        Task<List<Persons>> GetAllPersonsByStoreProcedure();
    }
}
