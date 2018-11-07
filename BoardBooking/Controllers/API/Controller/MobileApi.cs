using HmsService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BoardBooking.Controllers.API.Controller
{
    interface MobileApi
    {
        // customer api 
        HttpResponseMessage GetInfoUser(string phone, string password);
        HttpResponseMessage GetUserRole();
        HttpResponseMessage GetListStore();
        HttpResponseMessage GetInfoStore(int storeId);
        HttpResponseMessage GetListReviewStore(int storeId);
        HttpResponseMessage GetListPromotion();
        HttpResponseMessage GetListSessionStoreInRange(int storeId, DateTime day);
        bool CreateNewCustmer(tbCustomerViewModel customer);
        void CreateNewAppointment();

        // manager api 
        HttpResponseMessage GetStoreIdLoginManager(string phone, string password);
        HttpResponseMessage GetListAppointmentForStoreInRange(int storeId, DateTime day); 
    }
}
