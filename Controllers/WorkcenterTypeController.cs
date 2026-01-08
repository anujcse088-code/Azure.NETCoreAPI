using Azure.NETCoreAPI.Data;
using Azure.NETCoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Azure.NETCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkcenterTypeController : ControllerBase
    {
        private readonly IWorkcenterTypeRepository _repo;

        public WorkcenterTypeController(IWorkcenterTypeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkcenterType item)
        {
            await _repo.CreateAsync(item);
            return CreatedAtAction(nameof(Get), new { id = item.WorkcenterTypeId }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, WorkcenterType item)
        {
            var ok = await _repo.UpdateAsync(id, item);
            if (!ok) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var ok = await _repo.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
