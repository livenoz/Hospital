using HMS.Entities.Models;
using System.Collections.Generic;

namespace HMS.Business.Interfaces
{
    public interface IUserBusiness
    {
        TUser Add(TUser user);
        bool Update(TUser user);
        bool Delete(int userId);
        IEnumerable<TUser> GetAll();
        TUser GetById(int userId);
    }
}
