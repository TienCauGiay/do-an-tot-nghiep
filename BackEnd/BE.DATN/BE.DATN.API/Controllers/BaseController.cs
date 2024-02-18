using BE.DATN.BL.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE.DATN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity> : ControllerBase
    {
        protected readonly IBaseBL<TEntity> _baseBL;

        public BaseController(IBaseBL<TEntity> baseBL)
        {
            _baseBL = baseBL;
        }

        [HttpGet("get_all")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _baseBL.GetAllAsync();
            return Ok(res);
        }

        [HttpGet("get_by_id")]
        public async Task<IActionResult> GetByID(Guid id)
        {
            var res = await _baseBL.GetByIdAsync(id);
            return Ok(res);
        } 

        [HttpPost("insert")]
        public async Task<IActionResult> Post(TEntity entity)
        {
            var res = await _baseBL.InsertAsync(entity);
            return Ok(res);
        }

        [HttpPost("insert_multiple")]
        public async Task<IActionResult> PostMultiple(List<TEntity> entities)
        {
            var res = await _baseBL.InsertMultipleAsync(entities);
            return Ok(res);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Put(TEntity entity)
        {
            var res = await _baseBL.UpdateAsync(entity);
            return Ok(res);
        }

        [HttpPut("update_multiple")]
        public async Task<IActionResult> PutMultiple(List<TEntity> entities)
        {
            var res = await _baseBL.UpdateMultipleAsync(entities);
            return Ok(res);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _baseBL.DeleteAsync(id);
            return Ok(res);
        }

        [HttpDelete("delete_multiple")]
        public async Task<IActionResult> DeleteMultiple(List<Guid> ids)
        {
            var res = await _baseBL.DeleteMultipleAsync(ids);
            return Ok(res);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string? textSearch)
        {
            var res = await _baseBL.SearchAsync(textSearch);
            return Ok(res);
        }
    }
}
