//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HmsService.Models.Entities.Services
{
    using System;
    using System.Collections.Generic;
    
    
    public partial interface ItbReviewService : SkyWeb.DatVM.Data.IBaseService<tbReview>
    {
    }
    
    public partial class tbReviewService : SkyWeb.DatVM.Data.BaseService<tbReview>, ItbReviewService
    {
        public tbReviewService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.ItbReviewRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}