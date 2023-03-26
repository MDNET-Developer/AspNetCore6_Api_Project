using System.Linq.Expressions;

namespace NLayer.Core.Service
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        //IQueryable<T> GetAll(Expression<Func<T, bool>> exception);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> exception);
        Task<bool> AnyAsync(Expression<Func<T, bool>> exception);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);

        //Burada biz savechangeaasync dan istifade edeceyimiz ucun Update ve remove ucun async sozunden istifade etdik
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveRangAsync(IEnumerable<T> entity);
    }
}
