using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareManager.IManager
{
    public interface IUserManager
    {
        ResponeModel<User> SaveUpdateUser(User model);
        List<User> GetAllUser();
        User GetUserById(Guid userId);
        ResponeModel<User> LoginUser(string email, string password);
        User GetUserByResetPasswordId(Guid resetPasswordId);
        User GetUserByEmailId(string email);
    }
}
