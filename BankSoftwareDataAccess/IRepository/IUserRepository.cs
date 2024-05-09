using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface IUserRepository
    {

        ResponeModel<User> SaveUpdateUser(User model);
        ResponeModel<User> LoginUser(string email,string password);
        List<User> GetAllUser();
        User GetUserById(Guid userId);
        User GetUserByResetPasswordId(Guid resetPasswordId);
        User GetUserByEmailId(string email);



    }
}
