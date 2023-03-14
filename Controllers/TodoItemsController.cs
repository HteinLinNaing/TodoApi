using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public TodoItemsController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/TodoItems
        [HttpGet]
        // public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        // {
        //   if (_repositoryWrapper.TodoItems == null)
        //   {
        //       return NotFound();
        //   }
        //     return await _reopsitoryWrapper.TodoItems.ToListAsync();
        // }

        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var todoItems = await _repositoryWrapper.TodoItem.FindAllAsync();
            return todoItems
                .Select(x => ItemToDTO(x))
                .ToList();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        // public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        // {
        //     if (_context.TodoItems == null)
        //     {
        //         return NotFound();
        //     }
        //     var todoItem = await _context.TodoItems.FindByIDAsync(id);

        //     if (todoItem == null)
        //     {
        //         return NotFound();
        //     }

        //     return todoItem;
        // }

        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _repositoryWrapper.TodoItem.FindByIDAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        // public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        // {
        //     if (id != todoItem.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(todoItem).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!TodoItemExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            if (id != todoItemDTO.Id)
            {
                return BadRequest();
            }

            var todoItem = await _repositoryWrapper.TodoItem.FindByIDAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            todoItem.Name = todoItemDTO.Name;
            todoItem.IsComplete = todoItemDTO.IsComplete;

            try
            {
                await _repositoryWrapper.TodoItem.UpdateAsync(todoItem);
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        // public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        // {
        //     if (_repositoryWrapper.TodoItems == null)
        //     {
        //         return Problem("Entity set 'IRepositoryWrapper.TodoItems'  is null.");
        //     }
        //     _repositoryWrapper.TodoItems.Add(todoItem);
        //     await _repositoryWrapper.SaveChangesAsync();

        //     return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        // }

        public async Task<ActionResult<TodoItemDTO>> CreateTodoItem(TodoItemDTO todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name
            };

            await _repositoryWrapper.TodoItem.CreateAsync(todoItem, true);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                ItemToDTO(todoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTodoItem(long id)
        // {
        //     if (_repositoryWrapper.TodoItems == null)
        //     {
        //         return NotFound();
        //     }
        //     var todoItem = await _repositoryWrapper.TodoItems.FindByIDAsync(id);
        //     if (todoItem == null)
        //     {
        //         return NotFound();
        //     }

        //     _repositoryWrapper.TodoItems.Remove(todoItem);
        //     await _repositoryWrapper.SaveChangesAsync();

        //     return NoContent();
        // }

        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _repositoryWrapper.TodoItem.FindByIDAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.TodoItem.DeleteAsync(todoItem, true);

            return NoContent();
        }

        // Search

        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> SearchTodoItem(string term)
        {
            var empList = await _repositoryWrapper.TodoItem.SearchTodoItem(term);
            return Ok(empList);
        }

        [HttpPost("searchTodoItem")]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> SearchTodoItemMultiple(TodoItemSearchPayload SearchObj)
        {
            var empList = await _repositoryWrapper.TodoItem.SearchTodoItemMultiple(SearchObj);
            return Ok(empList);
        }

        private bool TodoItemExists(long id)
        {
            return _repositoryWrapper.TodoItem.IsExists(id);
        }

        private static TodoItemDTO ItemToDTO(TodoItem todoItem) =>
            new TodoItemDTO
            {
                Id = todoItem.Id,
                Name = todoItem.Name,
                IsComplete = todoItem.IsComplete
            };
    }
}
