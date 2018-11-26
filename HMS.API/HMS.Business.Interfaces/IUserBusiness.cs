using HMS.Common.Dtos.User;
using HMS.Common.Responses.User;
using HMS.Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HMS.Business.Interfaces
{
    public interface IUserBusiness
    {
        TUser Add(TUser user);
        bool Update(TUser user);
        bool Delete(int userId);
        IEnumerable<TUser> GetAll();
        TUser GetById(int userId);

        Task<LoginResponse> Login(LoginDto model);
        bool CheckAuthentication(string accessToken);
    }
}
