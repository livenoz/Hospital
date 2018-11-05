using HMS.Entities.Models;
using System.Collections.Generic;

namespace HMS.Business.Interfaces
{
    public interface IUserBusiness
    {
        Users AddUser(Users user);
        bool UpdateUser(Users user);
        bool DeleteUser(int userId);
        IEnumerable<Users> GetAllUser();
        Users GetUserById(int userId);
    }
}
