using App.Config.Classes.Base;
using App.Config.Helpers;
using App.Infrastructure.Database.Context;
using App.Infrastructure.Database.Entities;
using App.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public ApplicationDbContext Context
        {
            get
            {
                return (ApplicationDbContext)_Database;
            }
        }
        public UserRepository(ApplicationDbContext context) : base(context)
        {

        }

        public Users LoginUser(string name)
        {
            Users result = Context.Users.Where(x=>x.UserName == name).FirstOrDefault() ?? new Users();
            return result;
        }

    
    }
}
