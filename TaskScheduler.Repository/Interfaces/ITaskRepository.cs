using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Entities;

namespace TaskScheduler.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TodoItem>> GetTodoItems();

        Task<List<TodoItem>> GetTodoItemsByUser(int userId);

        Task<TodoItem> GetTodoItem(int id);

        Task<TodoItem> CreateTodoItem(TodoItem item);

        Task<TodoItem> UpdateTodoItem(TodoItem item);

        Task<TodoItem> DeleteTodoItem(int id);
    }
}
