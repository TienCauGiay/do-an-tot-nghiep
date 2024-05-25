using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.ClassRegistration;
using BE.DATN.BL.Models.Response;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class ClassRegistrationBL : BaseBL<class_registration>, IClassRegistrationBL
    {
        private readonly IClassRegistrationDL _classRegistrationDL;

        private readonly IClassRegistrationDetailDL _classRegistrationDetailDL;

        public ClassRegistrationBL(IClassRegistrationDL classRegistrationDL, IUnitOfWork unitOfWork, IClassRegistrationDetailDL classRegistrationDetailDL) : base(classRegistrationDL, unitOfWork)
        {
            _classRegistrationDL = classRegistrationDL;
            _classRegistrationDetailDL = classRegistrationDetailDL;
        }

        public async Task<ResponseServiceClassRegistration> GetFilterPagingAsync(int limit, int offset, string? textSearch)
        {
            try
            {
                if (textSearch == null)
                {
                    textSearch = string.Empty;
                }

                List<class_registration_view>? data = null;
                int? totalRecord = null;

                var res = await _classRegistrationDL.GetFilterPagingAsync(limit, offset, textSearch);
                data = res.Item1;
                totalRecord = res.Item2;

                return new ResponseServiceClassRegistration()
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Lấy dữ liệu thành công",
                    Data = data,
                    TotalPage = (int)Math.Ceiling((decimal)(totalRecord > 0 ? totalRecord : 0) / limit),
                    TotalRecord = totalRecord,
                    CurrentPage = offset,
                    CurrentPageRecords = data?.Count()
                };
            }
            catch (Exception ex)
            {
                return new ResponseServiceClassRegistration()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    Data = new List<class_registration_view>()
                };
            }
        }

        public async Task<List<class_registration_detail>?> GetListDetailAsync(Guid id)
        {
            try
            {
                var res = await _classRegistrationDetailDL.GetListByIdMasterAsync(id);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<class_registration_view>?> GetMultipleByCodeAsync(string code)
        {
            try
            {
                var res = await _classRegistrationDL.GetMultipleByCodeAsync(code);
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResponseService> InsertMasterDetailAsync(class_registration_entity model)
        {
            try
            {
                // insert master
                class_registration masterSave = new class_registration()
                {
                    class_registration_id = Guid.NewGuid(),
                    class_registration_code = model.class_registration_code,
                    class_registration_name = model.class_registration_name,
                    subject_id = model.subject_id,
                    teacher_id = model.teacher_id
                };
                var res = await _classRegistrationDL.InsertAsync(masterSave); 
                // Gọi để lấy id
                var masterSaveSuccess = await _classRegistrationDL.GetByCodeAsync(masterSave.class_registration_code);

                // insert detail
                if(model.ListDetail != null && model.ListDetail.Count() > 0)
                {
                    foreach(var itemDetail in model.ListDetail)
                    {
                        itemDetail.class_registration_detail_id = Guid.NewGuid();
                        itemDetail.class_registration_id = masterSaveSuccess.class_registration_id;
                    }
                    var res2 = await _classRegistrationDetailDL.InsertMultipleAsync(model.ListDetail);
                }

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

        public async Task<ResponseService> UpdateMasterDetailAsync(class_registration_entity model)
        {
            try
            {
                // update master
                class_registration masterSave = new class_registration()
                {
                    class_registration_id = model.class_registration_id,
                    class_registration_code = model.class_registration_code,
                    class_registration_name = model.class_registration_name,
                    subject_id = model.subject_id,
                    teacher_id = model.teacher_id
                };
                var res = await _classRegistrationDL.UpdateAsync(masterSave);

                // update detail
                if (model.ListDetail != null && model.ListDetail.Count() > 0)
                {
                    // Xóa đi để thêm lại
                    var resDelete = await _classRegistrationDetailDL.DebeteByIdMaster(model.class_registration_id);

                    foreach (var itemDetail in model.ListDetail)
                    {
                        itemDetail.class_registration_detail_id = Guid.NewGuid();
                        itemDetail.class_registration_id = model.class_registration_id;
                    }
                    var res2 = await _classRegistrationDetailDL.InsertMultipleAsync(model.ListDetail);
                }
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

        protected override async Task AfterInsertAsync(class_registration entity)
        {
            
        }

        protected override void ValidateBeforeDelete(Guid id)
        {
            
        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {
            
        }

        protected override void ValidateBusiness(class_registration entity, ModelState state)
        {
            
        }

        public override async Task<ResponseService> DeleteAsync(Guid id)
        {
            try
            {
                // Xóa detail trước 
                var res2 = await _classRegistrationDetailDL.DebeteByIdMaster(id);

                var res = await base.DeleteAsync(id);

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

        public override async Task<ResponseService> DeleteMultipleAsync(List<Guid> ids)
        {
            try
            {
                // Xóa detail trước
                var res2 = await _classRegistrationDetailDL.DebeteMultipleByListIdMaster(ids);

                var res = await base.DeleteMultipleAsync(ids);
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
    }
}
