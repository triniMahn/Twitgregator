using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Text.RegularExpressions;
using Twitgregator.Web.Helpers;

using Twitgregator.DAL.Implementation;
using Twitgregator.Projections.Implementation;

namespace Twitgregator.Web.Controllers.Api
{
    public class HomeController : ApiController
    {
        //public static Regex handleEx = new Regex(@"(?<=^|(?<=[^a-zA-Z0-9-_\.]))@([A-Za-z]+[A-Za-z0-9]+)");
        public static Regex handleEx = new Regex(@"(?<=^|(?<=[^a-zA-Z0-9-_\.]))@([A-Za-z_]+[A-Za-z0-9_]+)");
        
        protected List<string> parseNames(string screenNames)
        {
            List<string> names = new List<string>();
            try
            {
                MatchCollection matches = handleEx.Matches(screenNames);
                foreach (Match m in matches)
                    names.Add(m.Value);
            }
            catch
            {
                
            }
            return names;
        }
        
        public HttpResponseMessage Get(string screenNames)
        {
            TwitterPostListViewModel vm = null;
            //Names are URL encoded on the client side
            string decoded = HttpUtility.UrlDecode(screenNames);
            
            //Get a list of names out of the submitted form input
            List<string> names = parseNames(decoded);
            
            if(null == names)
                return HttpErrorResponseWrapper.create(this, "Error retrieving tweets.").getResponse();

            try
            {
                //Create a Twitter data source
                TwitterRESTAPIRepository repo = new TwitterRESTAPIRepository();
                
                TwitterViewModelFactory factory = new TwitterViewModelFactory();
                
                //Get tweets for each account going back two weeks from today
                object[] args = new object[] { names, DateTime.Now.AddDays(-14) };
                
                //Get a ViewModel to return to the client side for Backbone
                vm = (TwitterPostListViewModel)factory.createViewModel(repo, args);
            }
            catch (Exception ex)
            {
                return HttpErrorResponseWrapper.create(this, "Error retrieving tweets.").getResponse();
            }

            return Request.CreateResponse <TwitterPostListViewModel>(HttpStatusCode.OK,vm);
        }
        
        public HttpResponseMessage Post(string fc)
        {
            try { }
            catch (Exception ex)
            {
                return HttpErrorResponseWrapper.create(this, "Error retrieving tweets.").getResponse();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
