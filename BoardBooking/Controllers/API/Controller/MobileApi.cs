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
        HttpResponseMessage GetUserRole();
        HttpResponseMessage GetListStore();
        HttpResponseMessage GetInfoStore(int storeId);
        HttpResponseMessage GetListReviewStore(int storeId);
        HttpResponseMessage GetListPromotion(int storeId);
        HttpResponseMessage GetListSessionStore(int storeId, DateTime day);
        void CreateNewCustmer();
        void CreateNewAppointment();
    }
}
