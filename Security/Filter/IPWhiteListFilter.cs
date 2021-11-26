using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Security.Models;
using System.Linq;
using System.Net;

namespace Security.Filter
{
    public class IPWhiteListFilter : ActionFilterAttribute
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly CheckIpWhiteList _checkIpWhiteList;
        public IPWhiteListFilter(IOptions<CheckIpWhiteList> options, RequestDelegate requestDelegate)
        {
            this._checkIpWhiteList = options.Value;
            _requestDelegate = requestDelegate;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var requestIpList = context.HttpContext.Connection.RemoteIpAddress;
            var isInWhitList = _checkIpWhiteList.IPWhiteList.Where(x => IPAddress.Parse(x).Equals(requestIpList)).Any();
            if (!isInWhitList)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.Forbidden);
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
