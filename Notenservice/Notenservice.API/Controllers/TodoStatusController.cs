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
    public class TodoStatusController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoStatusController(TodoContext context)
        {
            _context = context;
        }

        // Get all Todo statuses
        // GET: api/TodoStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoStatusDTO>>> GetTodoStatuses()
        {
            return await _context.TodoStatus
                .Select(x => ItemToDTO(x))
                .ToListAsync();
        }

        // Get single Todo status
        // GET: api/TodoStatus/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoStatusDTO>> GetTodoStatus(long id)
        {
            var todoStatus = await _context.TodoStatus.FindAsync(id);

            if (todoStatus == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoStatus);
        }

        // POST: Add Todo status
        // POST: api/TodoStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoStatusDTO>> PostTodoStatus(TodoStatusDTO todoStatusDTO)
        {
            var todoStatus = new TodoStatus
            {
                Name = todoStatusDTO.Name,
                Description = todoStatusDTO.Description
            };

            _context.TodoStatus.Add(todoStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoStatus),
                new { id = todoStatus.Id },
                ItemToDTO(todoStatus));
        }

        // Update existing TodoStatus
        // PUT: api/TodoStatus/1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoStatus(long id, TodoStatusDTO todoStatusDTO)
        {
            if (id != todoStatusDTO.Id)
            {
                return BadRequest();
            }

            var todoStatus = await _context.TodoStatus.FindAsync(id);
            if (todoStatus == null)
            {
                return NotFound();
            }

            todoStatus.Name = todoStatusDTO.Name;
            todoStatus.Description = todoStatusDTO.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoStatusExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // Delete existing TodoStatus
        // DELETE: api/TodoStatus/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoStatus(long id)
        {
            var todoStatus = await _context.TodoStatus.FindAsync(id);

            if (todoStatus == null)
            {
                return NotFound();
            }

            _context.TodoStatus.Remove(todoStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoStatusExists(long id)
        {
            return _context.TodoStatus.Any(e => e.Id == id);
        }

        private static TodoStatusDTO ItemToDTO(TodoStatus todoStatus) =>
            new TodoStatusDTO
            {
                Id = todoStatus.Id,
                Name = todoStatus.Name,
                Description = todoStatus.Description
            };
    }
}