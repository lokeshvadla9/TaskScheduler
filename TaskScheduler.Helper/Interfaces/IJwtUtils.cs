using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskScheduler.Entities;

namespace TaskScheduler.Helper.Interfaces
{
    public interface IJwtUtils
    {
        string GenerateJwtToken(User user);
        int? ValidateToken(string token);
    }
}
