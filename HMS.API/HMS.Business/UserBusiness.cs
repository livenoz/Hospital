using System.Collections.Generic;
using HMS.Business.Interfaces;
using HMS.Entities.Models;
using HMS.Repository.Interfaces;

namespace HMS.Business
{
    public class UserBusiness : IUserBusiness
    {
        IUserRepository _userRepository;
        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Users AddUser(Users user)
        {
            return _userRepository.Add(user);
        }

        public bool DeleteUser(int userId)
        {
            return _userRepository.Delete(userId);
        }

        public IEnumerable<Users> GetAllUser()
        {
            return _userRepository.Get();
        }

        public Users GetUserById(int userId)
        {
            return _userRepository.Get(userId);
        }

        public bool UpdateUser(Users user)
        {
            return _userRepository.Update(user);
        }
    }
}
