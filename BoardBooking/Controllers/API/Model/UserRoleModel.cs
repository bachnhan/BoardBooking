using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoardBooking.Controllers.API.Model
{
    public class UserRoleModel
    {
        [JsonProperty(PropertyName = "role-id")]
        public int RoleId;
    }
}