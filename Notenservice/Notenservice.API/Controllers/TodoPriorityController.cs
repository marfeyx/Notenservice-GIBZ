using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.Shared.DTO;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoPriorityController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoPriorityController(TodoContext context)
        {
            _context = context;
        }

        // Get all Todo priorities
        // GET: api/TodoPriority
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoPriorityDTO>>> GetTodoPriorities()
        {
            return await _context.TodoPriority
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // Get single Todo priority
        // GET: api/TodoPriority/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoPriorityDTO>> GetTodoPriority(long id)
        {
            var todoPriority = await _context.TodoPriority.FindAsync(id);

            if (todoPriority == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoPriority);
        }

        // POST: Add Todo priority
        // POST: api/TodoPriority
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoPriorityDTO>> PostTodoPriority(TodoPriorityDTO todoPriorityDTO)
        {
            var todoPriority = new TodoPriority
            {
                Name = todoPriorityDTO.Name,
                Description = todoPriorityDTO.Description
            };

            _context.TodoPriority.Add(todoPriority);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoPriority),
                new { id = todoPriority.Id },
                ItemToDTO(todoPriority));
        }

        // Update existing TodoPriority
        // PUT: api/TodoPriority/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoPriority(long id, TodoPriorityDTO todoPriorityDTO)
        {
            if (id != todoPriorityDTO.Id)
            {
                return BadRequest();
            }

            var todoPriority = await _context.TodoPriority.FindAsync(id);
            if (todoPriority == null)
            {
                return NotFound();
            }

            todoPriority.Name = todoPriorityDTO.Name;
            todoPriority.Description = todoPriorityDTO.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoPriorityExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // Delete existing TodoPriority
        // DELETE: api/TodoPriority/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoPriority(long id)
        {
            var todoPriority = await _context.TodoPriority.FindAsync(id);

            if (todoPriority == null)
            {
                return NotFound();
            }

            _context.TodoPriority.Remove(todoPriority);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoPriorityExists(long id)
        {
            return _context.TodoPriority.Any(e => e.Id == id);
        }

        private static TodoPriorityDTO ItemToDTO(TodoPriority todoPriority) =>
            new TodoPriorityDTO
            {
                Id = todoPriority.Id,
                Name = todoPriority.Name,
                Description = todoPriority.Description
            };
    }
}