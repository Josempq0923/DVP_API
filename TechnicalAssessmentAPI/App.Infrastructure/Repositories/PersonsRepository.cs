using App.Config.Classes.Base;
using App.Infrastructure.Database.Context;
using App.Infrastructure.Database.Entities;
using App.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repositories
{
    public class PersonsRepository : BaseRepository<Persons>, IPersonsRepository
    {

        public ApplicationDbContext Context
        {
            get
            {
                return (ApplicationDbContext)_Database;
            }
        }
        public PersonsRepository(ApplicationDbContext context) : base(context)
        {

        }

        public async Task<List<Persons>> GetAllPersonsByStoreProcedure()
        {
            List<Persons> listPersons = await Context.Persons.FromSqlRaw("EXEC [dbo].[sp_GetPersons]").ToListAsync();

            if (listPersons == null || !listPersons.Any())
                return new List<Persons>();

            return listPersons;

        }
    }
}
