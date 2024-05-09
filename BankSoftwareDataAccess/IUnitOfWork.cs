using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareDataAccess
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves all changes made to the IWorkflowDbContext
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Returns the IBPPDbContext used by this unit of work
        /// </summary>
        IBankContext Context { get; }
    }
}
