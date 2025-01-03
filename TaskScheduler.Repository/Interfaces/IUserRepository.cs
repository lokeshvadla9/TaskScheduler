using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Entities;

namespace TaskScheduler.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Login(UserDto user);
        Task<List<User>> GetUsers();

        Task<User> CreateUser(User user);

        Task<User> GetUserById(int id);

        Task<User> UpdateUser(User user);

        Task<User> DeleteUser(int id);
    }
}
