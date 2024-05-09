using BankSoftwareDataAccess;
using BankSoftwareDataAccess.IRepository;
using BankSoftwareDataAccess.Repository;
using BankSoftwareManager.IManager;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareManager.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ResponeModel<User> SaveUpdateUser(User model)
        {
            return _userRepository.SaveUpdateUser(model);
        }
        public List<User> GetAllUser()
        {
            return _userRepository.GetAllUser();
        }
        public User GetUserById(Guid userId)
        {
            return _userRepository.GetUserById(userId);
        }
        public ResponeModel<User> LoginUser(string email, string password)
        {
            return _userRepository.LoginUser(email, password);
        }
        public User GetUserByResetPasswordId(Guid resetPasswordId)
        {
            return _userRepository.GetUserByResetPasswordId(resetPasswordId);
        }

        public User GetUserByEmailId(string email)
        {
            return _userRepository.GetUserByEmailId(email);
        }
    }
}
