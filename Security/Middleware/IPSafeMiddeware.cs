using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Security.Middleware
{
    public class IPSafeMiddeware
    {
        private readonly CheckIpWhiteList _checkIpWhiteList;
        private readonly RequestDelegate _requestDelegate;
        public IPSafeMiddeware(IOptions<CheckIpWhiteList> options, RequestDelegate requestDelegate)
        {
            this._requestDelegate = requestDelegate;
            this._checkIpWhiteList = options.Value;
            
        }
        public async Task Invoke(HttpContext httpContext) {
            var requestIpAdress = httpContext.Connection.RemoteIpAddress;
            var isInWhiteList = _checkIpWhiteList.IPWhiteList.Where(x => IPAddress.Parse(x).Equals(requestIpAdress)).Any();
            if (!isInWhiteList) {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }
            await _requestDelegate(httpContext);

        }
    }
}
