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
    public class ResourceRepository : EntityRepositoryBase<Resource>, IResourceRepository
    {
        private IUnitOfWork unitOfWork;

        public ResourceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Guid SaveUpdateResource(Resource model)
        {
            var account = DataContext.Resources.Count(u => u.ResourcePk == model.ResourcePk) > 0;
            if (!account)
            {
                model.ResourcePk = Guid.NewGuid();
                SetAdded(model);
            }
            else
            {
                SetModified(model);
            }
            unitOfWork.SaveChanges();
            return model.ResourcePk;
        }


        public List<Resource> GetAllResource()
        {
            return DataContext.Resources.ToList();
        }
        public List<Resource> GetAllUserChequebooks(Guid userId)
        {
            var resourceTypes = DataContext.AccountResourceTypes.Where(x => x.ResourceName == "Chequebook").FirstOrDefault();
            var userAccounts = DataContext.Accounts.Where(x => x.UserFk == userId && (x.IsDeleted == false )).ToList();
            List<Resource> chequebookResources = new List<Resource>();
            foreach (var item in userAccounts)
            {
                var userchequebooks = DataContext.Resources.Where(x => x.ResourceTypeFk == resourceTypes.AccountResourceTypePk && x.AccountFk==item.AccountPk && x.IsActive==true).ToList();
                Resource res;
                foreach (var book in userchequebooks)
                {
                    res = new Resource();
                    res = book;
                    res.ResourceAccount = new Account();
                    res.ResourceAccount.AccountName = item.AccountName;
                    res.ResourceAccount.AccountNumber = item.AccountNumber;
                    res.ResourceTypeName = resourceTypes.ResourceName;
                    res.ActiveStatus = book.IsActive == true ? "Active" : "Hold";
                    res.IssuedDate = book.CreateDate.ToString("MM/dd/yyyy HH:mm");
                    chequebookResources.Add(res);

                }
                
            }
            return chequebookResources;


        }

        public List<Resource> GetAllUserCards(Guid userId)
        {
            List<Guid> resoursesTypes = new List<Guid>();
            var debitCardType = DataContext.AccountResourceTypes.Where(x => x.ResourceName == "Debit Card").FirstOrDefault();
            var creditCardType = DataContext.AccountResourceTypes.Where(x =>  x.ResourceName == "Credit Card").FirstOrDefault();
            resoursesTypes.Add(debitCardType.AccountResourceTypePk);
            resoursesTypes.Add(creditCardType.AccountResourceTypePk);
            var userAccounts = DataContext.Accounts.Where(x => x.UserFk == userId && (x.IsDeleted == false)).ToList();
            List<Resource> cardsResources = new List<Resource>();
            foreach (var item in userAccounts)
            {
                var userCards = DataContext.Resources.Where(x => resoursesTypes.Contains(x.ResourceTypeFk) && x.AccountFk == item.AccountPk && x.IsActive == true).ToList();
                Resource res;
                foreach (var card in userCards)
                {
                    res = new Resource();
                    res = card;
                    res.ResourceAccount = new Account();
                    res.ResourceAccount.AccountName = item.AccountName;
                    res.CardNumber = card.CardNumber;
                    res.ExpiryDateString = card.ExpiryDate.ToString("MM/dd/yyyy HH:mm");
                    res.ResourceAccount.AccountNumber = item.AccountNumber;
                    res.ResourceTypeName = (card.ResourceTypeFk == debitCardType.AccountResourceTypePk)? "Debit Card": "Credit Card";
                    res.ActiveStatus = card.IsActive == true ? "Active" : "Hold";
                    res.IssuedDate = card.CreateDate.ToString("MM/dd/yyyy HH:mm");
                    cardsResources.Add(res);

                }

            }
            return cardsResources;


        }



        public Resource GetResourceById(Guid resourceId)
        {
            return DataContext.Resources.FirstOrDefault(x => x.ResourcePk == resourceId);
        }
        


        
    }
}
