using App.Config.Classes.Base;
using App.Config.DTO;
using App.Infrastructure.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces
{
    public interface IPersonsService : IBaseService<PersonsDTO, Persons> {
        Task<List<PersonsDTO>> GetAllPersonsByStoreProcedure();
    }

}
