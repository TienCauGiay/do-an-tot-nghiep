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
        Task<ResponseService> GetAllAsync();
        Task<ResponseService> GetByIdAsync(Guid id);
        Task<ResponseService> GetByCodeAsync(string code);
        Task<ResponseService> InsertAsync(TEntity entity);
        Task<ResponseService> InsertMultipleAsync(List<TEntity> entities);
        Task<ResponseService> UpdateAsync(TEntity entity);
        Task<ResponseService> UpdateMultipleAsync(List<TEntity> entities);
        Task<ResponseService> DeleteAsync(Guid id);
        Task<ResponseService> DeleteMultipleAsync(List<Guid> ids);
        Task<ResponseService> SearchAsync(string? textSearch);
        Task<ResponseService> CheckAriseAsync(Guid id);
        Task<ResponseService> GetIdAriseMultipleAsync(List<Guid> ids);
    }
}
