//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HmsService.ViewModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbAppointmentStatuViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<HmsService.Models.Entities.tbAppointmentStatu>
    {
    	
    			public virtual int ASID { get; set; }
    			public virtual string ASName { get; set; }
    	
    	public tbAppointmentStatuViewModel() : base() { }
    	public tbAppointmentStatuViewModel(HmsService.Models.Entities.tbAppointmentStatu entity) : base(entity) { }
    
    }
}