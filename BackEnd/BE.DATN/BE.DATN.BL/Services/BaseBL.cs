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
    public class BaseBL<TEntity> : IBaseBL<TEntity>
    {
        private readonly IBaseDL<TEntity> _baseDL;

        private readonly IUnitOfWork _unitOfWork;

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
            catch
            {
                return new ReponseService(StatusCodes.Status500InternalServerError, "Có lỗi xảy ra, vui lòng liên hệ Bùi Ngọc Tiến", new Object());
            }
        }

        public async Task<ReponseService> GetByIdAsync(Guid id)
        {
            try
            {
                var res = await _baseDL.GetByIdAsync(id);
                return new ReponseService(StatusCodes.Status200OK, "Tìm kiếm dữ liệu thành công", res);
            }
            catch
            {
                return new ReponseService(StatusCodes.Status500InternalServerError, "Có lỗi xảy ra, vui lòng liên hệ Bùi Ngọc Tiến", new Object());
            }
        }

        public async Task<ReponseService> InsertAsync(TEntity entity)
        {
            try
            {
                var res = await _baseDL.InsertAsync(entity);
                return new ReponseService(StatusCodes.Status200OK, "Thêm mới dữ liệu thành công", res);
            }
            catch
            {
                return new ReponseService(StatusCodes.Status500InternalServerError, "Có lỗi xảy ra, vui lòng liên hệ Bùi Ngọc Tiến", new Object());
            }
        } 

        public async Task<ReponseService> UpdateAsync(TEntity entity)
        {
            try
            {
                var res = await _baseDL.UpdateAsync(entity);
                return new ReponseService(StatusCodes.Status200OK, "Cập nhật dữ liệu thành công", res);
            }
            catch
            {
                return new ReponseService(StatusCodes.Status500InternalServerError, "Có lỗi xảy ra, vui lòng liên hệ Bùi Ngọc Tiến", new Object());
            }
        }

        public async Task<ReponseService> DeleteAsync(Guid id)
        {
            try
            {
                var res = await _baseDL.DeleteAsync(id);
                return new ReponseService(StatusCodes.Status200OK, "Xóa dữ liệu thành công", res);
            }
            catch
            {
                return new ReponseService(StatusCodes.Status500InternalServerError, "Có lỗi xảy ra, vui lòng liên hệ Bùi Ngọc Tiến", new Object());
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
            catch
            {
                return new ReponseService(StatusCodes.Status500InternalServerError, "Có lỗi xảy ra, vui lòng liên hệ Bùi Ngọc Tiến", new Object());
            }
        }
    }
}
