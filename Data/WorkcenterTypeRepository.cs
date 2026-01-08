using Azure.NETCoreAPI.Models;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Azure.NETCoreAPI.Data
{
    public class WorkcenterTypeRepository : IWorkcenterTypeRepository
    {
        private readonly string _connectionString;

        public WorkcenterTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<WorkcenterType>> GetAllAsync()
        {
            var list = new List<WorkcenterType>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT workcenter_type, add_user, add_date, mod_user, mod_date FROM dbo.Workcenter_Type", conn);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                list.Add(new WorkcenterType
                {
                    WorkcenterTypeId = reader.GetString(0),
                    AddUser = reader.IsDBNull(1) ? null : reader.GetString(1),
                    AddDate = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                    ModUser = reader.IsDBNull(3) ? null : reader.GetString(3),
                    ModDate = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                });
            }
            return list;
        }

        public async Task<WorkcenterType?> GetByIdAsync(string id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT workcenter_type, add_user, add_date, mod_user, mod_date FROM dbo.Workcenter_Type WHERE workcenter_type = @id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new WorkcenterType
                {
                    WorkcenterTypeId = reader.GetString(0),
                    AddUser = reader.IsDBNull(1) ? null : reader.GetString(1),
                    AddDate = reader.IsDBNull(2) ? null : reader.GetDateTime(2),
                    ModUser = reader.IsDBNull(3) ? null : reader.GetString(3),
                    ModDate = reader.IsDBNull(4) ? null : reader.GetDateTime(4),
                };
            }
            return null;
        }

        public async Task CreateAsync(WorkcenterType item)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("INSERT INTO dbo.Workcenter_Type (workcenter_type, add_user, add_date, mod_user, mod_date) VALUES (@id, @addUser, @addDate, @modUser, @modDate)", conn);
            cmd.Parameters.AddWithValue("@id", item.WorkcenterTypeId);
            cmd.Parameters.AddWithValue("@addUser", (object?)item.AddUser ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@addDate", (object?)item.AddDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@modUser", (object?)item.ModUser ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@modDate", (object?)item.ModDate ?? DBNull.Value);
            await conn.OpenAsync();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<bool> UpdateAsync(string id, WorkcenterType item)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("UPDATE dbo.Workcenter_Type SET add_user=@addUser, add_date=@addDate, mod_user=@modUser, mod_date=@modDate WHERE workcenter_type=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@addUser", (object?)item.AddUser ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@addDate", (object?)item.AddDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@modUser", (object?)item.ModUser ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@modDate", (object?)item.ModDate ?? DBNull.Value);
            await conn.OpenAsync();
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("DELETE FROM dbo.Workcenter_Type WHERE workcenter_type=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            var rows = await cmd.ExecuteNonQueryAsync();
            return rows > 0;
        }
    }
}
