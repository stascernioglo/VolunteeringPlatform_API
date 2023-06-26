using System.Linq.Expressions;
using VolunteeringPlatform.Common.Models.PagedRequest;
using VolunteeringPlatform.Domain.Auth;
using VolunteeringPlatform.Domain.Entities;

namespace VolunteeringPlatform.Dal.Interfaces
{
    public interface IRepository
    {
        Task<TEntity> GetByIdAsync<TEntity>(int id, CancellationToken cancellationToken) where TEntity : class;

        Task<TEntity> GetByIdWithIncludeAsync<TEntity>(int id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity;

        Task<List<TEntity>> GetAllAsync<TEntity>(CancellationToken cancellationToken) where TEntity : class;

        Task SaveChangesAsync(CancellationToken cancellationToken);

        void Add<TEntity>(TEntity entity) where TEntity : class;
        
        public Task<TEntity> GetWithFilter<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken) where TEntity : class;

        public Task<List<TEntity>> GetAllWithFilter<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken) where TEntity : class;


        Task<TEntity> DeleteAsync<TEntity>(int id, CancellationToken cancellationToken) where TEntity : BaseEntity;

        Task<PaginatedResult<TDto>> GetPagedDataAsync<TEntity, TDto>(PagedRequest pagedRequest, CancellationToken cancellationToken) where TEntity : class
                                                                                           where TDto : class;
        Task<TEntity> GetUserByIdWithIncludeAsync<TEntity>(int id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : User;

        public Task<List<TEntity>> GetAllWithFilterAndInclude<TEntity>(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : class;
    }
}
