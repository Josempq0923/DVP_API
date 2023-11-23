using App.Config.Classes.Base;
using App.Config.DTO;
using App.Domain.Interfaces;
using App.Infrastructure.Database.Entities;
using App.Infrastructure.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace App.Domain.Services
{
    public class PersonsService : BaseService<PersonsDTO, Persons>, IPersonsService
    {
        private readonly IPersonsRepository _repository;
        private readonly IMapper _mapper;

        public PersonsService(
            IPersonsRepository repository,
            IMapper mapper,
            IConfiguration configuration
        )
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PersonsDTO>> GetAllPersonsByStoreProcedure()
        {
            List<Persons> result = await _repository.GetAllPersonsByStoreProcedure();
            if (result == null || result.Count == 0)
                return new List<PersonsDTO>();

            List<PersonsDTO> listPersonsDTO = _mapper.Map<List<PersonsDTO>>(result);
            return listPersonsDTO;
        }

    }
}
