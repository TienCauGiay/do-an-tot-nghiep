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

        public async Task<ResponseService> GetAllAsync()
        {
            try
            {
                var res = await _baseDL.GetAllAsync();
                return new ResponseService(StatusCodes.Status200OK, "Lấy dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError, 
                        ex.Message, 
                        new Object()
                    );
            }
        }

        public async Task<ResponseService> GetByIdAsync(Guid id)
        {
            try
            {
                var res = await _baseDL.GetByIdAsync(id);
                return new ResponseService(StatusCodes.Status200OK, "Tìm kiếm dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public async Task<ResponseService> GetByCodeAsync(string code)
        {
            try
            {
                var res = await _baseDL.GetByCodeAsync(code);
                return new ResponseService(StatusCodes.Status200OK, "Tìm kiếm dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
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
        protected abstract void ValidateBusiness(TEntity entity, ModelState state);
        /// <summary>
        /// Xử lí nghiệp vụ trước khi thêm nhiều bản ghi
        /// các model cụ thể sẽ override lại để custom riêng
        /// </summary>
        /// <param name="entity"></param>
        protected abstract void ValidateBusinessMultiple(List<TEntity> entities, ModelState statte);

        public async Task<ResponseService> InsertAsync(TEntity entity)
        {
            try
            {
                ValidateBusiness(entity, ModelState.Insert);
                var res = await _baseDL.InsertAsync(entity);
                await AfterInsertAsync(entity);
                return new ResponseService(StatusCodes.Status201Created, "Thêm mới dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        protected virtual async Task AfterInsertAsync(TEntity entity)
        {

        }

        public async Task<ResponseService> InsertMultipleAsync(List<TEntity> entities)
        {
            try
            {
                ValidateBusinessMultiple(entities, ModelState.Insert);
                var res = await _baseDL.InsertMultipleAsync(entities);
                await AfterInsertMultipleAsync(entities);
                return new ResponseService(StatusCodes.Status201Created, "Thêm mới dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        protected virtual async Task AfterInsertMultipleAsync(List<TEntity> entities)
        {

        }

        public virtual async Task<ResponseService> UpdateAsync(TEntity entity)
        {
            try
            {
                ValidateBusiness(entity, ModelState.Update);
                var res = await _baseDL.UpdateAsync(entity);
                return new ResponseService(StatusCodes.Status200OK, "Cập nhật dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public virtual async Task<ResponseService> UpdateMultipleAsync(List<TEntity> entities)
        {
            try
            {
                ValidateBusinessMultiple(entities, ModelState.Update);
                var res = await _baseDL.UpdateMultipleAsync(entities);
                return new ResponseService(StatusCodes.Status200OK, "Cập nhật dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
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

        public async Task<ResponseService> DeleteAsync(Guid id)
        {
            try
            {
                ValidateBeforeDelete(id);
                var res = await _baseDL.DeleteAsync(id);
                return new ResponseService(StatusCodes.Status200OK, "Xóa dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public async Task<ResponseService> DeleteMultipleAsync(List<Guid> ids)
        {
            try
            {
                ValidateBeforeDeleteMultiple(ids);
                var res = await _baseDL.DeleteMultipleAsync(ids);
                return new ResponseService(StatusCodes.Status200OK, "Xóa dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }

        public async Task<ResponseService> SearchAsync(string? textSearch)
        {
            try
            {
                textSearch = textSearch ?? string.Empty;
                var res = await _baseDL.SearchAsync(textSearch);
                return new ResponseService(StatusCodes.Status200OK, "Tìm kiếm dữ liệu thành công", res);
            }
            catch (Exception ex)
            {
                return new ResponseService
                    (
                        StatusCodes.Status500InternalServerError,
                        ex.Message,
                        new Object()
                    );
            }
        }
    }
}
