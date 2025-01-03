using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using TaskScheduler.Agent;
using TaskScheduler.Agent.Interfaces;
using TaskScheduler.Entities;

namespace TaskScheduler.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskAgent _taskAgent;
        public TaskController(ITaskAgent taskAgent)
        {
            _taskAgent = taskAgent;
        }

        [HttpGet]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<IActionResult> GetAllTodoItems()
        {
            var response=await _taskAgent.GetTodoItems();
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else if (response.Status =="failure")
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("userId={userId}")]
        [Authorize("AdminAndUser")]
        public async Task<IActionResult> GetTodoItemsByUser(int userId=0)
        {
            if(userId == 0)
            {
                userId = (int)HttpContext.Items["UserId"];
            }
            var response = await _taskAgent.GetTodoItemsByUser(userId);
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else if (response.Status == "failure")
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("itemId={id}")]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<IActionResult> GetTodoItemsById(int id) 
        {
            var response = await _taskAgent.GetTodoItem(id);
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else if (response.Status == "failure")
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<IActionResult> CreateTodoItem([FromBody]TodoItemDto todoItem)
        {
            TodoItem todo=new TodoItem()
            {
                Title = todoItem.Title,
                Description = todoItem.Description,
                Status = todoItem.Status,
                StartDate = todoItem.StartDate,
                EndDate = todoItem.EndDate,
                UserId = (int)HttpContext.Items["UserId"]
                
            };
            var response=await _taskAgent.CreateTodoItem(todo);
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPut]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<IActionResult> UpdateTotoItem([FromBody]TodoItemDto todoItem)
        {
            TodoItem todo = new TodoItem()
            {
                TodoItemId= todoItem.TodoItemId,
                Title = todoItem.Title,
                Description = todoItem.Description,
                Status = todoItem.Status,
                StartDate = todoItem.StartDate,
                EndDate = todoItem.EndDate,
                UserId = (int)HttpContext.Items["UserId"]
            };
            var response = await _taskAgent.UpdateTodoItem(todo);
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else if (response.Status == "failure")
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpDelete]
        [Authorize(Policy = "AdminAndUser")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            var response = await _taskAgent.DeleteTodoItem(id);
            if (response.Status == "success")
            {
                return Ok(response);
            }
            else if (response.Status == "failure")
            {
                return NotFound(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
