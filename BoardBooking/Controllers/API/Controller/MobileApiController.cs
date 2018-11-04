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