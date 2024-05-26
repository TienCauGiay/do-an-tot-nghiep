using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Repository
{
    public interface IBaseDL<TEntity>
    {
        Task<List<TEntity>?> GetAllAsync();
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<List<TEntity>?> GetByListIdAsync(List<Guid> ids);
        Task<TEntity?> GetByCodeAsync(string code);
        Task<int> InsertAsync(TEntity entity);
        Task<int> InsertMultipleAsync(List<TEntity> entities);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateMultipleAsync(List<TEntity> entities);
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteMultipleAsync(List<Guid> ids);
        Task<List<TEntity>?> SearchAsync(string textSearch);
        Task<bool> CheckAriseAsync(Guid id);
        Task<List<Guid>?> GetIdAriseMultipleAsync(List<Guid> ids);
    }
}
