using BE.DATN.BL.Interfaces.Repository;
using BE.DATN.BL.Interfaces.UnitOfWork;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

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
            var selectQuery = $"SELECT * FROM {tableName}";
            var result = await _unitOfWork.Connection.QueryAsync<TEntity>(selectQuery, null, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);
            return (List<TEntity>?)result;
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var selectQuery = $"SELECT * FROM {tableName} WHERE {tableName}_id = @Id";

            var parameters = new { Id = id };

            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(selectQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result;
        }

        public async Task<int> InsertAsync(TEntity entity)
        {
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

            var result = await _unitOfWork.Connection.ExecuteAsync(insertQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result;
        }

        public async Task<int> InsertMultipleAsync(List<TEntity> entities)
        {
            var parameters = new DynamicParameters();
            var query = "";
            var index = 0;
            // Tạo câu truy vấn
            foreach (var entity in entities)
            {
                var notNullProps = entity.GetType().GetProperties().Where(prop => prop.GetValue(entity) != null);
                query += $"INSERT INTO {tableName} (";
                query += string.Join(", ", notNullProps.Select(prop => prop.Name));
                query += ") Values (";
                query += string.Join(", ", notNullProps.Select(prop => $"@{prop.Name}_{index}"));
                query += ");";

                foreach (var prop in notNullProps)
                {
                    parameters.Add($"{prop.Name}_{index}", prop.GetValue(entity));
                }

                parameters.Add($"{tableName}_id_{index}", Guid.NewGuid());
                index++;
            }

            var result = await _unitOfWork.Connection.ExecuteAsync(query, parameters, transaction: _unitOfWork.Transaction);
            return result;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
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

            var result = await _unitOfWork.Connection.ExecuteAsync(updateQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result;
        }

        public async Task<int> UpdateMultipleAsync(List<TEntity> entities)
        {
            var parameters = new DynamicParameters();
            var queryBuilder = new StringBuilder();

            var index = 0;
            foreach (var entity in entities)
            {
                var notNullProps = entity.GetType().GetProperties().Where(prop => prop.GetValue(entity) != null);

                queryBuilder.Append($"UPDATE {tableName} SET ");
                queryBuilder.Append(string.Join(", ", notNullProps.Select(prop => $"{prop.Name} = @{prop.Name}_{index}")));

                // Assuming you have a key property named "Id"
                queryBuilder.Append($" WHERE {tableName}_id = @id_{index};");

                foreach (var prop in notNullProps)
                {
                    parameters.Add($"{prop.Name}_{index}", prop.GetValue(entity));
                }

                // Assuming you have a key property named "Id"
                parameters.Add($"id_{index}", entity.GetType().GetProperty($"{tableName}_id")?.GetValue(entity));

                index++;
            }

            var result = await _unitOfWork.Connection.ExecuteAsync(queryBuilder.ToString(), parameters, transaction: _unitOfWork.Transaction);
            return result;
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            var deleteQuery = $"DELETE FROM {tableName} WHERE {tableName}_id = @Id";
            var parameters = new { Id = id };
            var result = await _unitOfWork.Connection.ExecuteAsync(deleteQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result;
        }

        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var parameters = new DynamicParameters();
            for (int i = 0; i < ids.Count; i++)
            {
                parameters.Add($"@id{i}", ids[i]);
            }

            string idParameters = string.Join(", ", parameters.ParameterNames.Select(p => $"@{p}"));
            string query = $"DELETE FROM {tableName} WHERE {tableName}_id IN ({idParameters})";

            var result = await _unitOfWork.Connection.ExecuteAsync(query, parameters, _unitOfWork.Transaction);
            return result;
        }

        public virtual async Task<List<TEntity>?> SearchAsync(string textSearch)
        {
            var selectQuery = $"SELECT * FROM {tableName} WHERE {tableName}_code like concat('%', @TextSearch, '%') or {tableName}_name like concat('%', @TextSearch, '%')";

            var parameters = new { TextSearch = textSearch };

            var result = await _unitOfWork.Connection.QueryAsync<TEntity>(selectQuery, parameters, commandType: CommandType.Text, transaction: _unitOfWork.Transaction);

            return result.ToList();
        }
    }
}
