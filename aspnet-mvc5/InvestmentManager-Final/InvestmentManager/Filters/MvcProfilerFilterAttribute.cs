using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InvestmentManager.Filters
{
    public class MvcProfilerFilterAttribute : ActionFilterAttribute
    {

        public MvcProfilerFilterAttribute()
        {
            this.httpContextName = this.GetType().FullName;
        }

        private readonly String httpContextName;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.IsChildAction)
                return;

            String name = $"Controller: {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} - {filterContext.ActionDescriptor.ActionName}";
            var timing = MiniProfiler.Current.Step(name);
            filterContext.HttpContext.Items.Add(httpContextName, timing);
        }


        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            var timing = filterContext.HttpContext.Items[httpContextName] as IDisposable;
            timing.Dispose();
        }

    }
}