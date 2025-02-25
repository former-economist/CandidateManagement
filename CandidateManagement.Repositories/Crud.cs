using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Data.SqlClient;
using Dapper;

using System.Collections;

namespace CandidateManagement.Repositories
{
    public class Crud
    {
        public async Task<Object> GetAllRows(string connectionString, string query) 
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Object>(query);
        }

        public async Task<Object> GetSingleRowById(string connectionString, string query, Guid id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<Object>(query, new { Id = id });
        }

        public async Task<Object> UpdateRow(string connectionString, string query, Object targetObject)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<Object>(query, targetObject);
        }

        public async Task<Object> AddRow(string connectionString, string query, Object targetObject)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync<Object>(query, targetObject);
        }

        public async Task<Object> DeleteRow(string connectionString, string query, Guid id)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleOrDefaultAsync<Object>(query, new { Id = id });
        }
    }
}
