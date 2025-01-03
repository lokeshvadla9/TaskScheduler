using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Repository.Interfaces;
using TaskScheduler.Entities;
using Microsoft.EntityFrameworkCore;

namespace TaskScheduler.Repository
{
    
    public class UserRepository:IUserRepository
    {
        private readonly TasksDbContext tasksDbContext;
        public UserRepository(TasksDbContext tasksDbContext) {
            this.tasksDbContext = tasksDbContext;
        }

        public async Task<List<User>> GetUsers()
        {
            return await tasksDbContext.Users.ToListAsync();
        }

        
        public async Task<User> CreateUser(User user)
        {
            var result = await tasksDbContext.Users.AddAsync(user);
            await tasksDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> GetUserById(int id)
        {
            var user= await tasksDbContext.Users.FindAsync(id);
            return user;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await tasksDbContext.Users.FindAsync(id);
            if(user != null)
            {
                tasksDbContext.Users.Remove(user);
                await tasksDbContext.SaveChangesAsync();
                return user;

            }
            else{
                return null;
            }
        }

        public async Task<User> UpdateUser(User user)
        {
            var userInDb = await tasksDbContext.Users.FirstOrDefaultAsync(u=>u.Email==user.Email);

            if(userInDb != null)
            {
                userInDb.Username = user.Username ?? userInDb.Username;
                userInDb.PasswordHash=user.PasswordHash??userInDb.PasswordHash;
                await tasksDbContext.SaveChangesAsync();
                return userInDb;
            }
            else
            {
                return null;
            }
        }

        public async Task<User> Login(UserDto user)
        {
            var userDetails=await tasksDbContext.Users.FirstOrDefaultAsync(u=>u.Username==user.Username && u.PasswordHash==user.Password);
            return userDetails;
        }

        
    }
}
