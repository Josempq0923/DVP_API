using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Config.Classes.Base
{
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<int> DeleteAsync(int id);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> CreateAsync(TEntity entity);
        Task<int> AddRangeAsync(List<TEntity> T);
    }

    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        protected DbContext _Database;
        protected DbSet<TEntity> _table;

        public BaseRepository(DbContext context)
        {
            _Database = context;
            _table = _Database.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            List<TEntity> result = await _table.ToListAsync() ?? new List<TEntity>();
            return result;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            TEntity? entity = await _table.FindAsync(id);
            return entity;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int result = 0;
            TEntity? entity = await GetByIdAsync(id);

            if (entity == null)
                return result;

            _table.Remove(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            await _table.AddAsync(entity);
            return await SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            int id = GetValueIdFromEntity(entity);
            var oldEntity = await GetByIdAsync(id);

            if (oldEntity == null)
                return 0;

            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (var item in properties)
            {
                var newValue = item.GetValue(entity);
                var oldValue = item.GetValue(oldEntity);

                if ((newValue != null) && (!object.Equals(newValue, oldValue)))
                    item.SetValue(oldEntity, newValue);
            }

            _Database.Update(oldEntity);
            return await SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(List<TEntity> T)
        {
            await _table.AddRangeAsync(T);
            return await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _Database.SaveChangesAsync();
        }

        private int GetValueIdFromEntity(TEntity entity)
        {
            Type entityType = typeof(TEntity);
            var property = entity
                .GetType()
                .GetProperties()
                .AsEnumerable<PropertyInfo>()
                .FirstOrDefault(t => t.Name.ToLower() == "id");
            if (property == null)
                return 0;

            int idValue = Convert.ToInt32(property.GetValue(entity, null));
            return idValue;
        }
    }
}
