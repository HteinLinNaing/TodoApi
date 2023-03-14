using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoItemRepository : IRepositoryBase<TodoItem>
    {
        Task<IEnumerable<TodoItem>> SearchTodoItem(string searchName);
        Task<IEnumerable<TodoItem>> SearchTodoItemMultiple(TodoItemSearchPayload SearchObj);
        bool IsExists(long id);
    }
}