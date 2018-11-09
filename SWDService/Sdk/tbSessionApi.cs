using HmsService.Models.Entities;
using HmsService.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsService.Sdk
{
    public partial class tbSessionApi
    {
        public bool CreateSession(tbSessionViewModel model)
        {
            try
            {
                var entity = model.ToEntity();
                this.BaseService.Create(entity);
                this.BaseService.Save();
                model.CopyFromEntity(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateSesion(tbSessionViewModel session)
        {

            try
            {
                using(SWDEntities context = new SWDEntities())
                {
                    context.Entry(session).State = EntityState.Modified;
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
