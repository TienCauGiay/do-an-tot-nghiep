﻿using BE.DATN.BL.Enums;
using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.Services;
using BE.DATN.BL.Interfaces.UnitOfWork;
using BE.DATN.BL.Models.Response;
using BE.DATN.BL.Models.Score;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Services
{
    public class ScoreBL : BaseBL<score>, IScoreBL
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IScoreDL _scoreDL; 

        private readonly IStudentDL _studentDL;

        private readonly ITeacherDL _teacherDL;

        public ScoreBL(IScoreDL scoreDL, IUnitOfWork unitOfWork, IStudentDL studentDL, ITeacherDL teacherDL) : base(scoreDL, unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _scoreDL = scoreDL;
            _studentDL = studentDL;
            _teacherDL = teacherDL;
        } 

        protected override void ValidateBusiness(score entity, ModelState state)
        {
            
        }

        protected override void ValidateBusinessMultiple(List<score> entities, ModelState statte)
        {
            
        }

        protected override void ValidateBeforeDelete(Guid id)
        {

        }

        protected override void ValidateBeforeDeleteMultiple(List<Guid> ids)
        {

        }

        public async Task<ReponseService> GetAllScoreViewAsync()
        {
            try
            { 
                var res = await _scoreDL.GetAllScoreViewAsync();
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

        public async Task<ReponseService> GetByStudentIdScoreViewAsync(Guid student_id)
        {
            try
            { 
                var res = await _scoreDL.GetByStudentIdScoreViewAsync(student_id);
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
    }
}