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
                        success = true,
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
                        success = true,
                        message = ConstantManager.GET_LIST_STORE_FAIL,
                        status = ConstantManager.STATUS_SUCCESS
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

        public HttpResponseMessage GetInfoStore(int storeId)
        {
            var storeApi = new tbStoreApi();
            var store = storeApi.Get().Where(q => q.SID == storeId).FirstOrDefault(); 
            if (store != null)
            {
                return new HttpResponseMessage
                {
                    Content = new JsonContent(store)
                };
            }
            return new HttpResponseMessage { };
        }
        
        public HttpResponseMessage GetListReviewStore(int storeId)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage GetListPromotion(int storeId)
        {
            throw new NotImplementedException();
        }

        public HttpResponseMessage GetListSessionStore(int storeId, DateTime day)
        {
            throw new NotImplementedException();
        }

        public void CreateNewCustmer()
        {
            throw new NotImplementedException();
        }

        public void CreateNewAppointment()
        {
            throw new NotImplementedException();
        }
    }
}