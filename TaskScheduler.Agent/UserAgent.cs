using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Agent.Interfaces;
using TaskScheduler.Entities;
using TaskScheduler.Repository.Interfaces;

namespace TaskScheduler.Agent
{
    
    public class UserAgent:IUserAgent
    {
        private readonly IUserRepository _userRepository;
        public UserAgent(IUserRepository userRepository) {  
            _userRepository=userRepository;
        }

        public async Task<ApiResponse<List<User>>> GetUsers()
        {
            try
            {
                List<User> users = await _userRepository.GetUsers();
                if(users.Count > 0)
                {
                    return new ApiResponse<List<User>>()
                    {
                        Status="success",
                        Message="Users retrived successfully",
                        Data= users
                    };
                }
                else
                {
                    return new ApiResponse<List<User>>()
                    {
                        Status = "failure",
                        Message = "No data available",
                        Data = new List<User>()
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<User>>()
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = new List<User>()
                };
                
            }
        }


        public async Task<ApiResponse<User>> CreateUser(User user)
        {
            try
            {
                string passwordHash = EncodePassword(user.PasswordHash);
                user.PasswordHash = passwordHash;
                User resultedUser =await _userRepository.CreateUser(user);
                if (resultedUser.UserId>0)
                {
                    return new ApiResponse<User>()
                    {
                        Status = "success",
                        Message = "User Created Succeffuly",
                        Data = resultedUser
                    };
                }
                else
                {
                    return new ApiResponse<User>()
                    {
                        Status = "failure",
                        Message = "Something went wrong",
                        Data = new User() { UserId=-1}
                    };
                }
            }
            catch(Exception ex)
            {
                return new ApiResponse<User>()
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = new User() { UserId = -1 }
                };
            }
            
            
        }
        private string EncodePassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            string base64Password = Convert.ToBase64String(passwordBytes);
            return base64Password;
        }

        public async Task<ApiResponse<User>> GetUserById(int id)
        {
            
            try
            {
                User user = await _userRepository.GetUserById(id);
                if(user == null)
                {
                    return new ApiResponse<User>()
                    {
                        Status = "failure",
                        Message = "User Not Found",
                        Data = null
                    };
                }
                else {
                    return new ApiResponse<User>()
                    {
                        Status = "success",
                        Message = "User  Found",
                        Data = user
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse<User>()
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<User>> DeleteUser(int id)
        {
            try
            {
                User user = await _userRepository.DeleteUser(id);
                if (user == null)
                {
                    return new ApiResponse<User>()
                    {
                        Status = "failure",
                        Message = "Invalid Id",
                        Data = null
                    };
                }
                else
                {
                    return new ApiResponse<User>()
                    {
                        Status = "success",
                        Message = "User deleted",
                        Data = user
                    };
                }

            }
            catch(Exception ex)
            {
                return new ApiResponse<User>()
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<User>> UpdateUser(User user)
        {
            try
            {
                user.PasswordHash = EncodePassword(user.PasswordHash);
                User userUpdated = await _userRepository.UpdateUser(user);
                if (userUpdated == null)
                {
                    return new ApiResponse<User>()
                    {
                        Status = "failure",
                        Message = "User Not Found",
                        Data = user
                    };
                }
                else
                {
                    return new ApiResponse<User>
                    {
                        Status = "success",
                        Message="User Updated",
                        Data = userUpdated
                    };
                }
                
                
            }
            catch (Exception ex)
            {
                return new ApiResponse<User>()
                {
                    Status = "error",
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<ApiResponse<User>> Login(UserDto user)
        {
            user.Password = EncodePassword(user.Password);
            User userDetais=await _userRepository.Login(user);
            if (userDetais == null)
            {
                return new ApiResponse<User>()
                {
                    Status = "failure",
                    Message = "Invalid username or password",
                    Data = null
                };

            }
            else
            {
                return new ApiResponse<User>()
                {
                    Status = "success",
                    Message = "User login successful",
                    Data = userDetais
                };
            }
        }
    }

    
}
