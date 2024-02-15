using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.DL.Repository
{
    public abstract class BaseDL<TEntity> : IBaseDL<TEntity>
    {
        protected readonly IUnitOfWork _unitOfWork;

        private string tableName = typeof(TEntity).Name.ToLower();

        public BaseDL(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TEntity>?> GetAllAsync()
        {
            using (var connection = _unitOfWork.Connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                var selectQuery = $"SELECT * FROM {tableName}";

                var result = await connection.QueryAsync<TEntity>(selectQuery);

                return result.ToList();
            }
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            using (var connection = _unitOfWork.Connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                var selectQuery = $"SELECT * FROM {tableName} WHERE {tableName}_id = @Id";

                var parameters = new { Id = id };

                var result = await connection.QueryFirstOrDefaultAsync<TEntity>(selectQuery, parameters);

                return result;
            }
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
            using (var connection = _unitOfWork.Connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                var parameters = new DynamicParameters();
                var propertyNames = string.Empty;
                var parameterNames = string.Empty;

                foreach (var prop in entity.GetType().GetProperties())
                {
                    var propName = prop.Name;
                    var paramName = "@" + propName;

                    if (propName.Contains($"{tableName}_id"))
                    {
                        parameters.Add(paramName, Guid.NewGuid());
                    }
                    else if (propName.Contains("created_date"))
                    {
                        parameters.Add(paramName, DateTime.Now);
                    }
                    else
                    {
                        parameters.Add(paramName, prop.GetValue(entity));
                    }

                    propertyNames += $"{propName}, ";
                    parameterNames += $"{paramName}, ";
                }

                propertyNames = propertyNames.TrimEnd(',', ' ');
                parameterNames = parameterNames.TrimEnd(',', ' ');

                var insertQuery = $"INSERT INTO {tableName} ({propertyNames}) VALUES ({parameterNames})";

                var result = await connection.ExecuteAsync(insertQuery, parameters);

                return result;
            }
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            using (var connection = _unitOfWork.Connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                var parameters = new DynamicParameters();
                var updateQuery = $"UPDATE {tableName} SET ";

                foreach (var prop in entity.GetType().GetProperties())
                {
                    var propName = prop.Name;
                    var paramName = "@" + propName;

                    if (propName.Contains("modified_date"))
                    {
                        parameters.Add(paramName, DateTime.Now);
                        updateQuery += $"{propName} = {paramName}, ";
                    }
                    else if (propName == $"{tableName}_id")
                    {
                        parameters.Add(paramName, prop.GetValue(entity));
                    }
                    else
                    {
                        parameters.Add(paramName, prop.GetValue(entity));
                        updateQuery += $"{propName} = {paramName}, ";
                    }
                }

                updateQuery = updateQuery.TrimEnd(',', ' ');

                updateQuery += $" WHERE {tableName}_id = @{tableName}_id";

                var result = await connection.ExecuteAsync(updateQuery, parameters);

                return result;
            }
        } 

        public async Task<int> DeleteAsync(Guid id)
        {
            using (var connection = _unitOfWork.Connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                var deleteQuery = $"DELETE FROM {tableName} WHERE {tableName}_id = @Id";

                var parameters = new { Id = id };

                var result = await connection.ExecuteAsync(deleteQuery, parameters);

                return result;
            }
        }

        public virtual async Task<List<TEntity>?> SearchAsync(string textSearch)
        {
            using (var connection = _unitOfWork.Connection)
            {
                if (connection.State != ConnectionState.Open)
                {
                    await connection.OpenAsync();
                }

                var selectQuery = $"SELECT * FROM {tableName} WHERE {tableName}_code like concat('%', @TextSearch, '%') or {tableName}_name like concat('%', @TextSearch, '%')";

                var parameters = new { TextSearch = textSearch };

                var result = await connection.QueryAsync<TEntity>(selectQuery, parameters);

                return result.ToList();
            }
        }
    }
}
