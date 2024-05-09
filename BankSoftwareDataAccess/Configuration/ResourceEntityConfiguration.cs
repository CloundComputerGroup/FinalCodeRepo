using BankSoftwareModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess.Configuration
{
    public class ResourceEntityConfiguration : EntityConfiguration<Resource>
    {
        public ResourceEntityConfiguration()
        {
            ToTable("Resource");
            HasKey(x => x.ResourcePk);
        }
    }
}
