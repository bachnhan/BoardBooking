using HmsService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsService.Sdk
{
    public partial class tbCustomerApi
    {
        public bool CreateCustomer(tbCustomerViewModel custtomer) {
            try
            {
                var entity = custtomer.ToEntity();
                this.BaseService.Create(entity);
                this.BaseService.Save();
                custtomer.CopyFromEntity(entity);
                return true;
            }
            catch
            {
                return false;
            }
          
        }
    }
}
