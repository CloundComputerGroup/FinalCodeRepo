using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.IRepository
{
    public interface IResourceRepository
    {

        Guid SaveUpdateResource(Resource model);
        List<Resource> GetAllResource();
        Resource GetResourceById(Guid resourceId);
        List<Resource> GetAllUserChequebooks(Guid userId);
        List<Resource> GetAllUserCards(Guid userId);



    }
}
