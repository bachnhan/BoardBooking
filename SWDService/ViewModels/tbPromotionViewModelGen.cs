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
    
    public partial class tbPromotionViewModel : SkyWeb.DatVM.Mvc.BaseEntityViewModel<HmsService.Models.Entities.tbPromotion>
    {
    	
    			public virtual int PID { get; set; }
    			public virtual Nullable<int> SID { get; set; }
    			public virtual string PImage { get; set; }
    			public virtual string PName { get; set; }
    			public virtual Nullable<System.DateTime> StartDay { get; set; }
    			public virtual Nullable<System.DateTime> EndDay { get; set; }
    			public virtual string PDescription { get; set; }
    	
    	public tbPromotionViewModel() : base() { }
    	public tbPromotionViewModel(HmsService.Models.Entities.tbPromotion entity) : base(entity) { }
    
    }
}