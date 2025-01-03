using System.Net.Http.Headers;
using TaskScheduler.Agent.Interfaces;
using TaskScheduler.Entities;
using TaskScheduler.Repository.Interfaces;

namespace TaskScheduler.Agent
{
    public class TaskAgent : ITaskAgent
    {
        private readonly ITaskRepository _taskRepository;
        public TaskAgent(ITaskRepository taskRepository) 
        { 
            _taskRepository = taskRepository;
        }
        public async Task<ApiResponse<TodoItem>> CreateTodoItem(TodoItem item)
        {
            try
            {
                TodoItem todoItem = await _taskRepository.CreateTodoItem(item);
                if (todoItem.TodoItemId != 0)
                {
                    return new ApiResponse<TodoItem>()
                    {
                        Status = "success",
                        Message = "Task Created Successfully",
                        Data = todoItem
                    };
                }
                else
                {
                    return new ApiResponse<TodoItem>
                    {
                        Status = "failure",
                        Message = "Something went wrong, Try again",
                        Data = null
                    };
                }
            }
            catch(Exception ex)
            {
                return new ApiResponse<TodoItem>()
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<TodoItem>> DeleteTodoItem(int id)
        {
            try
            {
                TodoItem item = await _taskRepository.DeleteTodoItem(id);
                if (item != null)
                {
                    return new ApiResponse<TodoItem>()
                    {
                        Status = "success",
                        Message = "Task Deleted Successfully",
                        Data = item
                    };
                }
                else
                {
                    return new ApiResponse<TodoItem>()
                    {
                        Status = "failure",
                        Message = "Item not present",
                        Data = null

                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<TodoItem>
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<TodoItem>> GetTodoItem(int id)
        {
            try
            {
                TodoItem item = await _taskRepository.GetTodoItem(id);
                if(item != null)
                {
                    return new ApiResponse<TodoItem>()
                    {
                        Status = "success",
                        Message = "Task Retrieved Successfully",
                        Data = item
                    };
                }
                else 
                {
                    return new ApiResponse<TodoItem>()
                    {
                        Status = "failure",
                        Message = "Item not Present",
                        Data = null
                    };
                        
                }
            }
            catch(Exception ex)
            {
                return new ApiResponse<TodoItem>
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = null
                };
            }
            

        }

        public async Task<ApiResponse<List<TodoItem>>> GetTodoItemsByUser(int userId)
        {
            try
            {
                List<TodoItem> items = await _taskRepository.GetTodoItemsByUser(userId);
                if(items.Count == 0)
                {
                    return new ApiResponse<List<TodoItem>>()
                    {
                        Status = "failure",
                        Message = "No Items found",
                        Data = null
                    };
                }
                else
                {
                    return new ApiResponse<List<TodoItem>>()
                    {
                        Status = "success",
                        Message = "Items retrieved Successfully",
                        Data = items
                    };
                }
            }
            catch(Exception ex)
            {
                return new ApiResponse<List<TodoItem>>()
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<List<TodoItem>>> GetTodoItems()
        {
            try
            {
                List<TodoItem> items = await _taskRepository.GetTodoItems();
                if (items.Count != 0)
                {
                    return new ApiResponse<List<TodoItem>>()
                    {
                        Status="success",
                        Message="Items retrieved Successfully",
                        Data=items
                    };
                }
                else
                {
                    return new ApiResponse<List<TodoItem>>()
                    {
                        Status = "failure",
                        Message = "No items found",
                        Data = null
                    };
                }
            }
            catch(Exception ex)
            {
                return new ApiResponse<List<TodoItem>>()
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<TodoItem>> UpdateTodoItem(TodoItem item)
        {
            try
            {
                TodoItem todoItem =await _taskRepository.UpdateTodoItem(item);
                if (todoItem != null)
                {
                    return new ApiResponse<TodoItem>
                    {
                        Status = "success",
                        Message = "Item Updated Successfully",
                        Data = todoItem

                    };
                }
                else 
                {
                    return new ApiResponse<TodoItem>()
                    {
                        Status = "failure",
                        Message = "Item not found",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<TodoItem>
                {
                    Status = "error",
                    Message = ex.Message,
                    Data=null
                };
            }
        }
    }
}