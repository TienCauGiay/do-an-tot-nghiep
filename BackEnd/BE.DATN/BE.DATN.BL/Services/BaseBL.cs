using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public abstract class BaseBL<TEntity> : IBaseBL<TEntity>
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IBaseDL<TEntity> _baseDL;

        public BaseBL(IBaseDL<TEntity> baseDL, IUnitOfWork unitOfWork)
        {
            _baseDL = baseDL;
            _unitOfWork = unitOfWork;
        } 

        public async Task<ReponseService> GetAllAsync()
        {
            try
            {
                var res = await _baseDL.GetAllAsync();
                return new ReponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ReponseService
                    (
                        StatusCodes.Status500InternalServerError, 
                        ex.Message, 
                        new Object()
                    );
            }
        }

        public async Task<ReponseService> GetByIdAsync(Guid id)
        {
            try
            {
                var res = await _baseDL.GetByIdAsync(id);
                return new ReponseService(StatusCodes.Status200OK, "Tìm kiếm dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ReponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        /// <summary>
        /// Xử lí nghiệp vụ trước khi thêm 1 bản ghi
        /// các model cụ thể sẽ override lại để custom riêng
        /// </summary>
        /// <param name="entity"></param>
        protected abstract void ValidateBusiness(TEntity entity, ModelStatte state);
        /// <summary>
        /// Xử lí nghiệp vụ trước khi thêm nhiều bản ghi
        /// các model cụ thể sẽ override lại để custom riêng
        /// </summary>
        /// <param name="entity"></param>
        protected abstract void ValidateBusinessMultiple(List<TEntity> entities, ModelStatte statte);

        public async Task<ReponseService> InsertAsync(TEntity entity)
        {
            try
            {
                ValidateBusiness(entity, ModelStatte.Insert);
                var res = await _baseDL.InsertAsync(entity);
                return new ReponseService(StatusCodes.Status200OK, "Thêm mới dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ReponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public async Task<ReponseService> InsertMultipleAsync(List<TEntity> entities)
        {
            try
            {
                ValidateBusinessMultiple(entities, ModelStatte.Insert);
                var res = await _baseDL.InsertMultipleAsync(entities);
                return new ReponseService(StatusCodes.Status200OK, "Thêm mới dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ReponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public async Task<ReponseService> UpdateAsync(TEntity entity)
        {
            try
            {
                ValidateBusiness(entity, ModelStatte.Update);
                var res = await _baseDL.UpdateAsync(entity);
                return new ReponseService(StatusCodes.Status200OK, "Cập nhật dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ReponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public async Task<ReponseService> UpdateMultipleAsync(List<TEntity> entities)
        {
            try
            {
                ValidateBusinessMultiple(entities, ModelStatte.Update);
                var res = await _baseDL.UpdateMultipleAsync(entities);
                return new ReponseService(StatusCodes.Status200OK, "Cập nhật dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ReponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        /// <summary>
        /// Xử lí nghiệp vụ trước khi xóa bản ghi
        /// các model override lại để xử lí cụ thể
        /// </summary>
        /// <param name="id"></param>
        protected abstract void ValidateBeforeDelete(Guid id);
        /// <summary>
        /// Xử lí nghiệp vụ trước khi xóa nhiều bản ghi
        /// các model override lại để xử lí cụ thể
        /// </summary>
        /// <param name="ids"></param>
        protected abstract void ValidateBeforeDeleteMultiple(List<Guid> ids); 

        public async Task<ReponseService> DeleteAsync(Guid id)
        {
            try
            {
                ValidateBeforeDelete(id);
                var res = await _baseDL.DeleteAsync(id);
                return new ReponseService(StatusCodes.Status200OK, "Xóa dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ReponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public async Task<ReponseService> DeleteMultipleAsync(List<Guid> ids)
        {
            try
            {
                ValidateBeforeDeleteMultiple(ids);
                var res = await _baseDL.DeleteMultipleAsync(ids);
                return new ReponseService(StatusCodes.Status200OK, "Xóa dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ReponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public async Task<ReponseService> SearchAsync(string? textSearch)
        {
            try
            {
                textSearch = textSearch ?? string.Empty;
                var res = await _baseDL.SearchAsync(textSearch);
                return new ReponseService(StatusCodes.Status200OK, "Tìm kiếm dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ReponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }
    }
}
