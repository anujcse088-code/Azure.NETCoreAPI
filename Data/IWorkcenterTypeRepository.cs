using Azure.NETCoreAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.NETCoreAPI.Data
{
    public interface IWorkcenterTypeRepository
    {
        Task<IEnumerable<WorkcenterType>> GetAllAsync();
        Task<WorkcenterType?> GetByIdAsync(string id);
        Task CreateAsync(WorkcenterType item);
        Task<bool> UpdateAsync(string id, WorkcenterType item);
        Task<bool> DeleteAsync(string id);
    }
}