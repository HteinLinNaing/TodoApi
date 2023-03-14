using System.Data;
using System.Linq;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Repositories
{
    public class TodoItemRepository : RepositoryBase<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(TodoContext repositoryContext) : base(repositoryContext) { }

        public async Task<IEnumerable<TodoItem>> SearchTodoItem(string searchTerm)
        {
            return await RepositoryContext.TodoItems
                        .Where(s => s.Name.Contains(searchTerm))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> SearchTodoItemMultiple(TodoItemSearchPayload SearchObj)
        {
            return await RepositoryContext.TodoItems
                        .Where(s => s.Name.Contains(SearchObj.Name ?? ""))
                        .OrderBy(s => s.Id).ToListAsync();
        }

        public bool IsExists(long id)
        {
            return RepositoryContext.TodoItems.Any(e => e.Id == id);
        }
    }

}