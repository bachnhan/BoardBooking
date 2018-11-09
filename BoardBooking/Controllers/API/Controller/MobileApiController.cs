using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using Jose;
using HmsService.Models;
using HmsService.Sdk;
using BoardBooking.Controllers.API;
using BoardBooking.Controllers.API.Controller;
using HmsService.ViewModels;

namespace BoardBooking.Controllers
{
    public class MobileApiController : ApiController/*, MobileApi*/
    {
        static string secretKey = ConstantManager.PRIVATE_KEY;
        // GET: MobileApi
        private static string GenerateToken(string CPhone)
        {
            var payload = CPhone + ":" + DateTime.Now.Ticks.ToString();

            return JWT.Encode(payload, secretKey, JwsAlgorithm.HS256);
        }

        private string GetCustomerIdFromToken(string token)
        {
            string key = JWT.Decode(token, secretKey);
            string[] parts = key.Split(new char[] { ':' });
            return parts[0];
        }

        [HttpGet]
        [Route("api/user-role")]
        public HttpResponseMessage GetUserRole(/*string access_Token*/)
        {

           var tbCustomerApi = new tbCustomerApi();
            //var CPhone = GetCustomerIdFromToken(access_Token);
            //var roleId = tbCustomerApi.Get().Where(q => q.CPhone == CPhone).FirstOrDefault();
            var roleId = tbCustomerApi.Get().FirstOrDefault();
            if (roleId == null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(new {
                        success = false,
                        message = ConstantManager.GET_USER_ROLE_FAIL,
                        status = ConstantManager.STATUS_SUCCESS
                    })
                };
            }
            return new HttpResponseMessage
            {
                Content = new JsonContent(new {
                    success = true,
                    message = ConstantManager.GET_USER_ROLE_SUCCESS,
                    status = ConstantManager.STATUS_SUCCESS,
                    data = roleId
                })
            };
        }

        [HttpGet]
        [Route("api/get-user")]
        public tbCustomerViewModel GetInfoUser(string phone, string password)
        {
            var customerApi = new tbCustomerApi();
            var customer = customerApi.Get().Where(q => q.CPhone == phone && q.CPassword == password).FirstOrDefault();
            if (customer == null)
            {
                return customer;
            }
            return null;
        }

        [HttpGet]
        [Route("api/list-store")]
        public List<tbStoreViewModel> GetListStore()
        {
            var storeApi = new tbStoreApi();
            var stores = storeApi.Get().ToList();
            if (stores != null)
            {
                return stores;
            }
            return null;
        }

        [HttpGet]
        [Route("api/info-store")]
        public tbStoreViewModel GetInfoStore(int storeId)
        {
            var storeApi = new tbStoreApi();
            var store = storeApi.Get().Where(q => q.SID == storeId).FirstOrDefault(); 
            if (store == null)
            {
                return null;
            }
            return store;
        }
        
        [HttpGet]
        [Route("api/list-review")]
        public List<tbReviewViewModel> GetListReviewStore(int storeId)
        {
            var reviewApi = new tbReviewApi();
            var reviews = reviewApi.Get().Where(q => q.SID == storeId).ToList(); 
            if (reviews == null)
            {
                return null;
            }
            return reviews;
        }

        [HttpGet]
        [Route("api/list-promotion")]
        public List<tbPromotionViewModel> GetListPromotion()
        {
            var promotionApi = new tbPromotionApi(); 
            var promotions = promotionApi.Get().ToList();
            return promotions;
        }

        [HttpGet]
        [Route("api/list-session-inrange")]
        public List<tbSessionViewModel> GetListSessionStoreInRange(int storeId, DateTime day)
        {
            var sessionApi = new tbSessionApi();
            var sessions = sessionApi.Get().Where(q => q.SID == storeId && q.DayCreate >= day).ToList(); 
            if (sessions == null)
            {
                return null;
            }
            return sessions;
        }

        [HttpPost]
        [Route("api/customer")]
        public bool CreateNewCustmer(tbCustomerViewModel customer)
        {
            var customerApi = new tbCustomerApi();
            var check = customerApi.Get().Where(q => q.CPhone == customer.CPhone).FirstOrDefault();
            if (check != null)
            {
                var result = customerApi.CreateCustomer(customer);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }

        [HttpPost]
        [Route("api/create-appointment")]
        public bool CreateNewAppointment(tbAppointmentViewModel model)
        {
            var appointmentApi = new tbAppointmentApi();
            var sessionApi = new tbSessionApi();
            var check = sessionApi.Get().Where(q => q.SsID == model.SsID && q.Capital >= model.ACapital).FirstOrDefault();
            if (check != null)
            {
                var result = appointmentApi.CreateAppointment(model);
                check.Capital = check.Capital - model.ACapital; ;
                sessionApi.UpdateSesion(check);
                return true;
            }
            return false;
        }

        [HttpPost]
        [Route("api/create-review")]
        public bool CreateNewReview(tbReviewViewModel model)
        {
            var reviewApi = new tbReviewApi();
            var check = reviewApi.CreateReview(model);
            if (check) return true;
            return false; 
        }

        // manager api 
        [HttpGet]
        [Route("api/get-store")]
        public tbStoreViewModel GetStoreIdLoginManager(string phone, string password)
        {
            var customerApi = new tbCustomerApi();
            var storeApi = new tbStoreApi();
            var customer = customerApi.Get().Where(q => q.CPhone == phone && q.CPassword == password).FirstOrDefault();
            var checkStore = storeApi.Get().Where(q => q.CPhone == phone).FirstOrDefault();
            if (customer == null)
            {
                return null;
            } else if (checkStore == null)
            {
                return null;
            }
            return checkStore;
        }

        [HttpGet]
        [Route("api/get-appoinment-store")]
        public List<tbAppointmentViewModel> GetListAppointmentFromToday(int storeId)
        {
            var appointmentApi = new tbAppointmentApi();
            var sessionApi = new tbSessionApi();
            DateTime now = DateTime.Now;
            var sessions = sessionApi.Get().Where(q => q.SID == storeId && q.DayCreate >= now).ToList();
            List<tbAppointmentViewModel> result = new List<tbAppointmentViewModel>();
            foreach (var item in sessions)
            {
                var appointments = appointmentApi.Get().Where(q => q.SsID == item.SsID).ToList();
                result.AddRange(appointments);
            }
            return result;
        }

        [HttpGet]
        [Route("api/get_session")]
        public List<tbSessionViewModel> GetListSessionFromToday(int storeId)
        {
            var sessionApi = new tbSessionApi();
            DateTime now = DateTime.Now;
            var listSession = sessionApi.Get().Where(q => q.SID == storeId && q.DayCreate >= now).ToList();
            return listSession;
        }

        [HttpPost]
        [Route("api/create-session")]
        public bool CreateSessionByManager(tbSessionViewModel model)
        {
            var sessionApi = new tbSessionApi();
            var check = sessionApi.Get().Where(q => q.DayCreate == model.DayCreate).FirstOrDefault();
            if (check == null)
            {
                sessionApi.CreateSession(model);
                return true;
            }
            return false; 
        }

        [HttpGet]
        [Route("api/get-promotion-store")]
        public List<tbPromotionViewModel> GetListPromotionOfStore(int storeId)
        {
            var promotionApi = new tbPromotionApi();
            DateTime now = DateTime.Now;
            var promotions = promotionApi.Get().Where(q => q.SID == storeId).ToList();
            return promotions;
        }

        [HttpPost]
        [Route("api/create-promotion")]
        public bool CreatePromotion(tbPromotionViewModel model)
        {
            var promotionApi = new tbPromotionApi();
            var check = promotionApi.CreatePromotion(model);
            if (check) return true;
            return false;
        }

        [HttpPost]
        [Route("apit/update-appointment-status")]
        public bool UpdateAppointmentStatus(tbAppointmentViewModel model)
        {
            var appointmentApi = new tbAppointmentApi();
            var check = appointmentApi.UpdateAppointment(model);
            if (check) return true;
            return false; 
        }
    }
}