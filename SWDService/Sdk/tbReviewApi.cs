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
    public partial class tbReviewApi
    {
        public bool CreateReview(tbReviewViewModel model)
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
    }
}
