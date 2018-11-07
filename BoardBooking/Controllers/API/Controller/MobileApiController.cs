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
    public class MobileApiController : ApiController, MobileApi
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
        public HttpResponseMessage GetInfoUser(string phone, string password)
        {
            var customerApi = new tbCustomerApi();
            var customer = customerApi.Get().Where(q => q.CPhone == phone && q.CPassword == password).FirstOrDefault();
            if(customer == null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(new
                    {
                        success = false,
                        message = ConstantManager.GET_FAIL, 
                        status = ConstantManager.STATUS_FAIL
                    })
                };
            }
            return new HttpResponseMessage
            {
                Content = new JsonContent(new
                {
                    success = true, 
                    message = ConstantManager.GET_SUCCESS, 
                    status = ConstantManager.STATUS_SUCCESS,
                    data = customer
                })
            };
        }

        [HttpGet]
        [Route("api/list-store")]
        public HttpResponseMessage GetListStore()
        {
            var storeApi = new tbStoreApi();
            var stores = storeApi.Get().ToList();
            if (stores == null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(new
                    {
                        success = false,
                        message = ConstantManager.GET_LIST_STORE_FAIL,
                        status = ConstantManager.STATUS_FAIL,
                    })
                };
            }
            return new HttpResponseMessage
            {
                Content = new JsonContent(new
                {
                    success = true,
                    message = ConstantManager.GET_LIST_STORE_SUCCESS,
                    status = ConstantManager.STATUS_SUCCESS,
                    data = stores
                })
            };
        }

        [HttpGet]
        [Route("api/info-store")]
        public HttpResponseMessage GetInfoStore(int storeId)
        {
            var storeApi = new tbStoreApi();
            var store = storeApi.Get().Where(q => q.SID == storeId).FirstOrDefault(); 
            if (store == null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(new
                    {
                        success = false,
                        message = ConstantManager.GET_FAIL,
                        status = ConstantManager.STATUS_FAIL, 
                    })
                };
            }
            return new HttpResponseMessage
            {
                Content = new JsonContent(new
                {
                    success = true, 
                    message = ConstantManager.GET_SUCCESS, 
                    status = ConstantManager.STATUS_SUCCESS, 
                    data = store
                })
            };
        }
        
        [HttpGet]
        [Route("api/list-review")]
        public HttpResponseMessage GetListReviewStore(int storeId)
        {
            var reviewApi = new tbReviewApi();
            var review = reviewApi.Get().Where(q => q.SID == storeId).ToList(); 
            if (review == null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(new
                    {
                        success = false,
                        message = ConstantManager.GET_FAIL, 
                        status = ConstantManager.STATUS_FAIL,
                    })
                };
            }
            return new HttpResponseMessage
            {
                Content = new JsonContent(new
                {
                    success = true, 
                    message = ConstantManager.GET_SUCCESS, 
                    status = ConstantManager.STATUS_SUCCESS,
                    data = review
                })
            };
        }

        [HttpGet]
        [Route("api/list-promotion")]
        public HttpResponseMessage GetListPromotion()
        {
            var promotionApi = new tbPromotionApi();
            var promotion = promotionApi.Get().ToList();
            if (promotion == null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(new
                    {
                        success = false,
                        message = ConstantManager.GET_FAIL, 
                        status = ConstantManager.STATUS_FAIL,
                    })
                };
            }
            return new HttpResponseMessage
            {
                Content = new JsonContent(new
                {
                    success = true, 
                    message = ConstantManager.GET_SUCCESS, 
                    status = ConstantManager.STATUS_SUCCESS, 
                    data = promotion
                })
            };
        }

        [HttpGet]
        [Route("api/list-session-inrange")]
        public HttpResponseMessage GetListSessionStoreInRange(int storeId, DateTime day)
        {
            var sessionApi = new tbSessionApi();
            var session = sessionApi.Get().Where(q => q.SID == storeId && q.DayCreate >= day).ToList(); 
            if (session == null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(new
                    {
                        success = false, 
                        message = ConstantManager.GET_FAIL,
                        status = ConstantManager.STATUS_FAIL
                    })
                };
            }
            return new HttpResponseMessage
            {
                Content = new JsonContent(new
                {
                    success = true, 
                    message = ConstantManager.GET_SUCCESS, 
                    status = ConstantManager.STATUS_SUCCESS, 
                    data = session
                })
            };
        }

        [HttpPost]
        [Route("api/customer")]
        public void CreateNewCustmer(tbCustomerViewModel customer)
        {
            var customerApi = new tbCustomerApi();
            var result = customerApi.CreateCustomer(customer);
            
            throw new NotImplementedException();
        }

        public void CreateNewAppointment()
        {
            throw new NotImplementedException();
        }


        // manager api 
        [HttpGet]
        [Route("api/get-store")]
        public HttpResponseMessage GetStoreIdLoginManager(string phone, string password)
        {
            var customerApi = new tbCustomerApi();
            var storeApi = new tbStoreApi();
            var customer = customerApi.Get().Where(q => q.CPhone == phone && q.CPassword == password).FirstOrDefault();
            var checkStore = storeApi.Get().Where(q => q.CPhone == phone).FirstOrDefault();
            if (customer == null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(new
                    {
                        success = false, 
                        message = ConstantManager.GET_FAIL, 
                        status = ConstantManager.STATUS_FAIL
                    })
                };
            } else if (checkStore == null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(new
                    {
                        success = false,
                        message = ConstantManager.GET_FAIL,
                        status = ConstantManager.STATUS_FAIL
                    })
                };
            }
            return new HttpResponseMessage
            {
                Content = new JsonContent(new
                {
                    success = true, 
                    message = ConstantManager.GET_SUCCESS, 
                    status = ConstantManager.STATUS_SUCCESS, 
                    data = checkStore
                })
            };  
        }

        [HttpGet]
        [Route("api/get-appoinment-store")]
        public HttpResponseMessage GetListAppointmentForStoreInRange(int storeId, DateTime day)
        {
            var appointmentApi = new tbAppointmentApi();
            var sessionApi = new tbSessionApi();
            var sessions = sessionApi.Get().Where(q => q.SID == storeId && q.DayCreate >= day).ToList();
            List<tbAppointmentViewModel> result = new List<tbAppointmentViewModel>();
            foreach (var item in sessions)
            {
                var appointments = appointmentApi.Get().Where(q => q.SsID == item.SsID).ToList();
                result.AddRange(appointments);
            } 
            return new HttpResponseMessage
            {
                Content = new JsonContent(new
                {
                    success = false, 
                    message = ConstantManager.GET_FAIL, 
                    status = ConstantManager.STATUS_FAIL,
                    data = result
                })
            };
        }

    }
}