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
    public class ResourceManager : IResourceManager
    {
        private readonly IResourceRepository _resourceRepository;
        public ResourceManager(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public Guid SaveUpdateResource(Resource model)
        {
            return _resourceRepository.SaveUpdateResource(model);
        }
        public List<Resource> GetAllResource()
        {
            return _resourceRepository.GetAllResource();
        }
        public List<Resource> GetAllUserChequebooks(Guid userId)
        {
            return _resourceRepository.GetAllUserChequebooks(userId);
        }
        public List<Resource> GetAllUserCards(Guid userId)
        {
            return _resourceRepository.GetAllUserCards(userId);
        }

        public Resource GetResourceById(Guid resourceId)
        {
            return _resourceRepository.GetResourceById(resourceId);
        }
    }
}
