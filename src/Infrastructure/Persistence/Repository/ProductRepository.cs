using TopupPortal.Application.Common.Interfaces;
using TopupPortal.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopupPortal.Infrastructure.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnectionFactory _dbConnection;
        private readonly IConfiguration _configuration;
        public ProductRepository(IDbConnectionFactory dbConnection, IConfiguration configuration)
        {
            _dbConnection = dbConnection;
            _configuration = configuration;
            _dbConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
       
        }
        public async Task<int> AddAsync(Product entity)
        {
            entity.AddedOn = DateTime.Now;
            var sql = "Insert into Products (Name,Description,Barcode,Rate,AddedOn) VALUES (@Name,@Description,@Barcode,@Rate,@AddedOn)";
            using (var conn = _dbConnection.Create())
            {
                var result = await conn.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = "DELETE FROM Products WHERE Id = @Id";
            using (var conn = _dbConnection.Create())
            {
                var result = await conn.ExecuteAsync(query, new { Id = id }, commandType: CommandType.Text);
                return result;
            }
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            var sql = "SELECT * FROM Products";
            using (var conn = _dbConnection.Create())
            {
                var result = await conn.QueryAsync<Product>(sql);
                return result.ToList();
            }
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Products WHERE Id = @Id";
            using (var conn = _dbConnection.Create())
            {
                var result = await conn.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Product entity)
        {
            entity.ModifiedOn = DateTime.Now;
            var sql = "UPDATE Products SET Name = @Name, Description = @Description, Barcode = @Barcode, Rate = @Rate, ModifiedOn = @ModifiedOn  WHERE Id = @Id";
            using (var conn = _dbConnection.Create())
            {
                var result = await conn.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
