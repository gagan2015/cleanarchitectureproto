using CleanArchitecture.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence.Repository
{
    public abstract class SqlRepository<TEntity> : IGenericRepository<TEntity>
         where TEntity : class
    {
        private readonly IDbConnectionFactory _dbConnection;

        public SqlRepository(IDbConnectionFactory dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public abstract Task<IReadOnlyList<TEntity>> GetAllAsync();


        public abstract Task<TEntity> GetByIdAsync(int id);

        public abstract Task<int> AddAsync(TEntity entity);
        public abstract Task<int> UpdateAsync(TEntity entityToUpdate);

        public abstract Task<int> DeleteAsync(int id);
    }
}
