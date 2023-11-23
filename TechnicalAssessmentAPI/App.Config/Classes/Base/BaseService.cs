using AutoMapper;

namespace App.Config.Classes.Base
{
    //Clase de servicio genérica
    public interface IBaseService<TEntityDTO, TEntity> 
        where TEntityDTO : class
        where TEntity : class
    {
        Task<List<TEntityDTO>> GetAllAsync();
        Task<TEntityDTO> GetByIdAsync(int id);
        Task<int> DeleteAsync(int id);
        Task<int> CreateAsync(TEntityDTO dto);
        Task<int> UpdateAsync(TEntityDTO dto);
        Task<int> AddRangeAsync(List<TEntityDTO> entities);
    }
    public class BaseService<TEntityDTO, TEntity> : IBaseService<TEntityDTO, TEntity>
        where TEntityDTO : class
        where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;
        private readonly IMapper _mappper;
        public BaseService(IBaseRepository<TEntity> repository, IMapper mappper)
        {
            _repository= repository;
            _mappper= mappper;
        }
        public async Task<List<TEntityDTO>> GetAllAsync()
        {
            List<TEntity> result = await _repository.GetAllAsync();
            if (result == null || result.Count == 0)
                return new List<TEntityDTO>();

            List<TEntityDTO> entityDTO = _mappper.Map<List<TEntityDTO>>(result);
            return entityDTO;
        }    
        public async Task<TEntityDTO> GetByIdAsync(int id)
        {
            TEntity result = await _repository.GetByIdAsync(id);
            TEntityDTO entityDTO = _mappper.Map<TEntityDTO>(result);
            return entityDTO;
        }
        public async Task<int> CreateAsync(TEntityDTO dto)
        {
            int result = 0;

            if(dto == null)
                return result;

            TEntity entity = _mappper.Map<TEntity>(dto);
            result = await _repository.CreateAsync(entity);
            return result;
        }

        public async Task<int> AddRangeAsync(List<TEntityDTO> entities)
        {
            int result = 0;

            if (entities == null || !entities.Any())
                return result;

            List<TEntity> T = _mappper.Map<List<TEntity>>(entities);
            result = await _repository.AddRangeAsync(T);
            return result;
        }

        public async Task<int> UpdateAsync(TEntityDTO dto)
        {
            int result = 0;
            if (dto == null)
                return result;

            TEntity entity = _mappper.Map<TEntity>(dto);
            result = await _repository.UpdateAsync(entity);
            return result;
        }
        public async Task<int> DeleteAsync(int id)
        {
            int result = 0;
            if (id == 0)
                return result;

            result = await _repository.DeleteAsync(id);
            return result;
        }
    }
}
