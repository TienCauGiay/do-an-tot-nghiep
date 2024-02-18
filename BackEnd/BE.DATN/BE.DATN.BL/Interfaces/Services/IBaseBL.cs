using BE.DATN.BL.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.Services
{
    public interface IBaseBL<TEntity>
    {
        Task<ReponseService> GetAllAsync();
        Task<ReponseService> GetByIdAsync(Guid id);
        Task<ReponseService> InsertAsync(TEntity entity);
        Task<ReponseService> InsertMultipleAsync(List<TEntity> entities);
        Task<ReponseService> UpdateAsync(TEntity entity);
        Task<ReponseService> UpdateMultipleAsync(List<TEntity> entities);
        Task<ReponseService> DeleteAsync(Guid id);
        Task<ReponseService> DeleteMultipleAsync(List<Guid> ids);
        Task<ReponseService> SearchAsync(string? textSearch);
    }
}
