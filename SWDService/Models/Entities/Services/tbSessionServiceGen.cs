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
    
    
    public partial interface ItbSessionService : SkyWeb.DatVM.Data.IBaseService<tbSession>
    {
    }
    
    public partial class tbSessionService : SkyWeb.DatVM.Data.BaseService<tbSession>, ItbSessionService
    {
        public tbSessionService(SkyWeb.DatVM.Data.IUnitOfWork unitOfWork, Repositories.ItbSessionRepository repository) : base(unitOfWork, repository)
        {
        }
    }
}
