using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using System.Net.Http;

namespace Twitgregator.Web.Helpers
{
    public class HttpErrorResponseWrapper
    {
        protected List<string> errors = null;
        protected ApiController curController = null;

        protected HttpErrorResponseWrapper(ApiController current)
        {
            errors = new List<string>();
            curController = current;
        }

        public static HttpErrorResponseWrapper create(ApiController current)
        {
            return new HttpErrorResponseWrapper(current);
        }

        public static HttpErrorResponseWrapper create(ApiController current, string [] errs)
        {
            HttpErrorResponseWrapper h = new HttpErrorResponseWrapper(current);
            foreach (string s in errs)
                h.AddError(s);
            return h;
        }

        public static HttpErrorResponseWrapper create(ApiController current, string err)
        {
            HttpErrorResponseWrapper h = new HttpErrorResponseWrapper(current);
            h.AddError(err);
            return h;
        }

        public void AddError(string errorMsg)
        {
            errors.Add(errorMsg);
        }

        public HttpResponseMessage getResponse(){
            return curController.Request.CreateResponse<List<string>>(HttpStatusCode.InternalServerError, errors);
        }
    }
}