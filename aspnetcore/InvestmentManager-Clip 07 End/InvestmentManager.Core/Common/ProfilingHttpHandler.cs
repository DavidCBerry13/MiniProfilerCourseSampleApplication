using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvestmentManager.Core.Common
{
    public class ProfilingHttpHandler : DelegatingHandler
    {
        /// <summary>
        /// Default constructor for the ProfilingHTTP Handler.  Using this constructor
        /// requires you to assign the inner handler for this handler using the InnerHandler
        /// property
        /// </summary>
        public ProfilingHttpHandler()
        {
        }


        /// <summary>
        /// Create a new ProfilingHttpHandler with the given inner handler
        /// </summary>
        /// <param name="innerHandler">An HttpMessageHandler of the inner handler, that is
        /// the handler that will be called after this one.
        /// </param>
        public ProfilingHttpHandler(HttpMessageHandler innerHandler)
        {
            this.InnerHandler = innerHandler;
        }


        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpVerb = request.Method.ToString();
            String url = request.RequestUri.AbsoluteUri;

            var requestContent = await this.GetRequestContent(request);

            using (CustomTiming timing = MiniProfiler.Current.CustomTiming("http", String.Empty, httpVerb))
            {
                var response = await base.SendAsync(request, cancellationToken);

                timing.CommandString = $"URL          : {url}\nCONTENT      : {requestContent}\nRESPONSE CODE: {response.StatusCode}";
                return response;
            }
        }


        internal async Task<String> GetRequestContent(HttpRequestMessage request)
        {
            if ( request.Content != null)
            {
                return await request.Content.ReadAsStringAsync();
            }
            else
            {
                return await Task.FromResult(String.Empty);
            }
        }

    }
}
