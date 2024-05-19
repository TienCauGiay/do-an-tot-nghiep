using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.ClassRegistration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace BE.DATN.DL.Repository
{
    public class ClassRegistrationDetailDL : BaseDL<class_registration_detail>, IClassRegistrationDetailDL
    {
        public ClassRegistrationDetailDL(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<List<class_registration_detail>?> GetListByIdMasterAsync(Guid id)
        {
            var selectQuery = $"SELECT * FROM public.class_registration_detail WHERE class_registration_id = @Id";

            var parameters = new { Id = id };

            var result = await _unitOfWork.Connection.QueryAsync<class_registration_detail>(selectQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return (List<class_registration_detail>?)result;
        }

        public async Task<int> DebeteByIdMaster(Guid id)
        {
            var deleteQuery = $"DELETE FROM public.class_registration_detail WHERE class_registration_id = @Id";
            var parameters = new { Id = id };
            var result = await _unitOfWork.Connection.ExecuteAsync(deleteQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result;
        }

        public async Task<int> DebeteMultipleByListIdMaster(List<Guid> ids)
        {
            var deleteQuery = "DELETE FROM public.class_registration_detail WHERE class_registration_id = ANY(@Ids::uuid[])";
            var parameters = new { Ids = ids.ToArray() };
            var result = await _unitOfWork.Connection.ExecuteAsync(deleteQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result;
        } 
    }
}
