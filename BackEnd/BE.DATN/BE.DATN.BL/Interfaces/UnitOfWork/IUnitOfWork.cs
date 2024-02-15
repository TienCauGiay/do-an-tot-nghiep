using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE.DATN.BL.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        NpgsqlConnection Connection { get; }
        NpgsqlTransaction Transaction { get; }
        void BeginTransaction();
        Task BeginTransactionAsync();
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
