using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using TaskScheduler.Entities;
using TaskScheduler.Repository.Interfaces;

namespace TaskScheduler.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TasksDbContext _dbContext;
        public  TaskRepository(TasksDbContext tasksDbContext) 
        {
            _dbContext = tasksDbContext;
        }
        public async Task<TodoItem> CreateTodoItem(TodoItem item)
        {
            var result = await _dbContext.TodoItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TodoItem> DeleteTodoItem(int id)
        {
            var todoItem= await _dbContext.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                _dbContext.TodoItems.Remove(todoItem);
                await _dbContext.SaveChangesAsync();
                return todoItem;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<TodoItem>> GetTodoItemsByUser(int userId)
        {
            var items=await _dbContext.TodoItems.Where(i=>i.UserId == userId).ToListAsync();
            return items;
        }
        public async Task<TodoItem> GetTodoItem(int id)
        {
            var todoItem =await _dbContext.TodoItems.FindAsync(id);
            return todoItem;
        }

        public async Task<List<TodoItem>> GetTodoItems()
        {
            return await _dbContext.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> UpdateTodoItem(TodoItem item)
        {
            var itemInDb =await _dbContext.TodoItems.FirstOrDefaultAsync(i => i.TodoItemId == item.UserId);

            if(itemInDb != null)
            {
                await _dbContext.TodoItemHistories.AddAsync(new TodoItemHistory()
                {
                    Status=item.Status,
                    TodoItemId = item.TodoItemId

                });
                itemInDb.Title= itemInDb.Title?? item.Title;
                itemInDb.StartDate= itemInDb.StartDate?? item.StartDate;
                itemInDb.EndDate = itemInDb.EndDate ?? item.EndDate;
                itemInDb.Status=itemInDb.Status??item.Status;
                itemInDb.CreatedAt=itemInDb?.CreatedAt?? item.CreatedAt;
                await _dbContext.SaveChangesAsync();
                return itemInDb;

            }
            else
            {
                return null;
            }
        }
    }
}