using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Entities;

namespace TaskScheduler.Agent.Interfaces
{
    public interface IUserAgent
    {
        public Task<ApiResponse<List<User>>> GetUsers();
        public Task<ApiResponse<User>> CreateUser(User user);

        public Task<ApiResponse<User>> GetUserById(int id);

        public Task<ApiResponse<User>> DeleteUser(int id);

        public Task<ApiResponse<User>> UpdateUser(User user);

        public Task<ApiResponse<User>> Login(UserDto user);
    }
}
