using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using Jose;
using HmsService.Models;
using HmsService.Sdk;
using BoardBooking.Controllers.API;

namespace BoardBooking.Controllers
{
    public class MobileApiController : ApiController
    {
        static string secretKey = ConstantManager.PRIVATE_KEY;
        // GET: MobileApi
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
                };
            }
            return new HttpResponseMessage
            {
                Content = new JsonContent(roleId)
            };
        }

        [HttpGet]
        public HttpRequestMessage GetListStore()
        {
            var storeApi = new tbStoreApi();
            var store = storeApi.Get().ToList(); 
            //if (store != null)
            //{
            //    return new HttpResponseMessage
            //    {
            //        Content = new JsonContent(store)
            //    };
            //}
            //return new HttpResponseMessage { };
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
            var reviewApi = new tbReviewApi();
            var review = reviewApi.Get().Where(q => q.SID == storeId).ToList(); 

        }

        public HttpResponseMessage GetListPromotion(int storeId)
        {

        }

        public HttpResponseMessage GetListSessionStore(int storeId, DateTime day)
        {

        }

        public CreateNewCustmer()
        {

        }

        public CreateNewAppointment()
        {

        }

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
    }
}