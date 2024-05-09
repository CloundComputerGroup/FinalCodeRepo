using BankSoftwareDataAccess.EntityRepository;
using BankSoftwareDataAccess.IRepository;
using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Repository
{
    public class UserRepository : EntityRepositoryBase<User>, IUserRepository
    {
        private IUnitOfWork unitOfWork ;

        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ResponeModel<User> SaveUpdateUser(User model)
        {
            ResponeModel<User> response = new ResponeModel<User>();
            try
            {
                var user = DataContext.Users.Count(u => u.UserPk == model.UserPk) > 0;
                if (!user)
                {
                    var alreadyExist = DataContext.Users.Count(u => u.Email == model.Email) > 0;
                    if (!alreadyExist)
                    {
                        model.UserPk = Guid.NewGuid();
                        SetAdded(model);
                    }
                    else
                    {
                        response.Code = -1;
                        response.Status = "Warning";
                        response.ErrorMessage = "Already Registered with same UserName";
                        response.PkGuid = Guid.Empty;
                        return response;
                    }


                }
                else
                {
                    SetModified(model);
                }
                unitOfWork.SaveChanges();
                response.Code = 1;
                response.Status = "Success";
                response.ErrorMessage = "Saved Successfully.";
                response.PkGuid = model.UserPk;
                return response;

            }
            catch (Exception ex)
            {

                response.Code = 0;
                response.Status = "Error";
                response.ErrorMessage = "Error occured: "+ ex.Message;
                response.PkGuid = Guid.Empty;
                return response;
            }
            
        }

        public ResponeModel<User> LoginUser(string email,string password)
        {
            ResponeModel<User> response = new ResponeModel<User>();
            try
            {
                var user = DataContext.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                if (user==null)
                {
                    response.Code = -1;
                    response.Status = "Warrning";
                    response.ErrorMessage = "Incorrect UserName or Password.";
                    response.PkGuid = Guid.Empty;
                    return response;



                }
                else
                {
                    response.Code = 0;
                    response.Status = "Success";
                    response.ErrorMessage = "Successfully Login";
                    response.PkGuid = user.UserPk;
                    response.Model = user;
                    return response;
                }
                unitOfWork.SaveChanges();
               
            }
            catch (Exception ex)
            {

                response.Code = 0;
                response.Status = "Error";
                response.ErrorMessage = "Error occured: " + ex.Message;
                response.PkGuid = Guid.Empty;
                return response;
            }

        }


        public List<User> GetAllUser()
        {
            return DataContext.Users.ToList();
        }
        
        
        public User GetUserById(Guid userId)
        {
            return DataContext.Users.Where(x => x.UserPk == userId).FirstOrDefault();
        }

        public User GetUserByResetPasswordId(Guid resetPasswordId)
        {
            return DataContext.Users.Where(x => x.ResetPasswordId == resetPasswordId).FirstOrDefault();
        }

        public User GetUserByEmailId(string email)
        {
            return DataContext.Users.Where(x => x.Email == email).FirstOrDefault();
        }




    }
}
