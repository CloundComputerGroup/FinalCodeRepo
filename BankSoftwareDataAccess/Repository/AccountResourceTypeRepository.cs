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
    public class AccountResourceTypeRepository : EntityRepositoryBase<AccountResourceType>, IAccountResourceTypeRepository
    {
        private IUnitOfWork unitOfWork;

        public AccountResourceTypeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdateAccountResourceType(AccountResourceType model)
        {
            var accountResourceType = DataContext.AccountResourceTypes.Count(u => u.AccountResourceTypePk == model.AccountResourceTypePk) > 0;
            if (!accountResourceType)
            {
                model.AccountResourceTypePk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
            unitOfWork.SaveChanges();
            return model.AccountResourceTypePk;
        }


        public List<AccountResourceType> GetAllAccountResourceType()
        {
            return DataContext.AccountResourceTypes.ToList();
        }
        
        
        public AccountResourceType GetAccountResourceTypeById(Guid accountResourceTypeId)
        {
            return DataContext.AccountResourceTypes.Where(x => x.AccountResourceTypePk == accountResourceTypeId).FirstOrDefault();
        }
        


        
    }
}
