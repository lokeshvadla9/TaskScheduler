using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Entities;

namespace TaskScheduler.Agent.Interfaces
{
    public interface ITaskAgent
    {
        Task<ApiResponse<List<TodoItem>>> GetTodoItems();

        Task<ApiResponse<List<TodoItem>>> GetTodoItemsByUser(int userId);

        Task<ApiResponse<TodoItem>> GetTodoItem(int id);

        Task<ApiResponse<TodoItem>> CreateTodoItem(TodoItem item);

        Task<ApiResponse<TodoItem>> UpdateTodoItem(TodoItem item);

        Task<ApiResponse<TodoItem>> DeleteTodoItem(int id);
    }
}
