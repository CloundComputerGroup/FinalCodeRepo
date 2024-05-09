using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSoftwareModels
{
    public class ResponeModel<T> where T : class
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public string Status { get; set; }
        public Guid PkGuid { get; set; }
        public  T Model {get;set;}
    }
}
