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
        Task<int> InsertAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(Guid id);
        Task<List<TEntity>?> SearchAsync(string textSearch);
    }
}
