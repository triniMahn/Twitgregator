using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading;
using Twitgregator.DAL.Framework;
using Twitgregator.Posts.Framework;
using Twitgregator.Posts.Implementation;

namespace Twitgregator.DAL.Implementation
{
    public class TwitterAuthData
    {
        public enum AuthDataError
        {
            NO_ISSUE = 0,
            SERVICE_ISSUE = 1,
            UNKNOWN = 2
        }
        
        protected static TwitterAuthData authData = null;
        protected static object lockObj = new object();
        
        //These should probably have protected set accessors, but deserialization (below) won't work, if so.
        public string token_type { get; set; }
        public string access_token { get; set; }
        
        public AuthDataError ErrorInfo { get; protected set; }

        //public TwitterAuthData()
        //{
        //    ErrorInfo = AuthDataError.NO_ISSUE;
        //}
        
        protected static TwitterAuthData getAuthData()
        {
            TwitterAuthData twitAuthResponse = null;

            //Keys in code for simplicity sake -- might want to store
            //these in a user-controlled DB table in prod
            string oAuthConsumerKey = "6sWAxK0FBuiLk6FH46FA";
            string oAuthConsumerSecret = "o8EycvQgrCALEK3ZpjrE0B15pjXr3H0FdIgM9ido";
            
            //TODO: Store in config file in case Twitter changes this up going forward
            string oAuthUrl = "https://api.twitter.com/oauth2/token";
            
            //Construct a string, containing our API keys, that will be inserted
            //into the HTTP Request header. Looks like we're using Basic Authentication here.
            string authHeaderFormat = "Basic {0}";

            string authHeader = string.Format(authHeaderFormat,
                Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(oAuthConsumerKey) + ":" +
                Uri.EscapeDataString((oAuthConsumerSecret)))
            ));

            string postBody = "grant_type=client_credentials";

            HttpWebRequest authRequest = null;
            WebResponse authResponse = null;
            string jsonData = null;

            try
            {
                authRequest = (HttpWebRequest)WebRequest.Create(oAuthUrl);

                authRequest.Headers.Add("Authorization", authHeader);
                authRequest.Method = "POST";
                authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (Stream stream = authRequest.GetRequestStream())
                {
                    byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                    stream.Write(content, 0, content.Length);
                }

                authRequest.Headers.Add("Accept-Encoding", "gzip");

                authResponse = authRequest.GetResponse();

                // Deserialize into a POCO object
                using (authResponse)
                {
                    using (StreamReader reader = new StreamReader(authResponse.GetResponseStream()))
                    {
                        jsonData = reader.ReadToEnd();
                        twitAuthResponse = JsonConvert.DeserializeObject<TwitterAuthData>(jsonData);
                    }
                }
            }
            catch (WebException we)
            {
                twitAuthResponse = new TwitterAuthData { ErrorInfo = AuthDataError.SERVICE_ISSUE };
                Debug.WriteLine("TwitterAuthData::getAuthData - " + we.Message);
                //Write this error to log
            }
            catch (Exception ex)
            {
                twitAuthResponse = new TwitterAuthData { ErrorInfo = AuthDataError.UNKNOWN };
                Debug.WriteLine("TwitterAuthData::getAuthData - " + ex.Message);
                //Write this error to log
            }

            return twitAuthResponse;
        }

        //Double check locking is controversial (http://www.yoda.arachsys.com/csharp/singleton.html), but I'm doing it anyway, since
        //given the response delay we get better lazy loading here
        public static TwitterAuthData getInstance()
        {
            if (null == authData)
            {
                lock (lockObj)
                {
                    if (null == authData)
                    {
                        authData = getAuthData();
                    }
                }
            }
            return authData;
        }
    }
    
    public class TwitterRESTAPIRepository: IRepository<IUserWebPost>
    {
        //With help from: http://stackoverflow.com/questions/17067996/authenticate-and-request-a-users-timeline-with-twitter-api-1-1-oauth
        //AND, https://dev.twitter.com/docs/working-with-timelines

        public static readonly int TWEET_REQUEST_COUNT = 5;
        
        protected HttpWebRequest createTimelineGETRequest(string requestURL)
        {
            TwitterAuthData twitAuthResponse = TwitterAuthData.getInstance();
           
            HttpWebRequest timeLineRequest = (HttpWebRequest)WebRequest.Create(requestURL);
            string timelineHeaderFormat = "{0} {1}";
            timeLineRequest.Headers.Add("Authorization", string.Format(timelineHeaderFormat, twitAuthResponse.token_type, twitAuthResponse.access_token));
            timeLineRequest.Method = "Get";

            return timeLineRequest;
        }

        protected TwitterPost[] getTweets(string screenName, Int64 max_id)
        {
            //string screenname = "paybyphone";//"triniMahn";
            WebResponse timeLineResponse = null;
            string timelineRequestFormat = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&include_rts=1&exclude_replies=0&count={1}&trim_user=1" + (max_id>0?"&max_id={2}":"");
            string timelineUrl = string.Format(timelineRequestFormat, screenName, TWEET_REQUEST_COUNT, max_id);

            HttpWebRequest request = createTimelineGETRequest(timelineUrl);

            try
            {
                timeLineResponse = request.GetResponse();
            }
            catch (WebException we)
            {
                Debug.WriteLine("TwitterRESTAPIRepository::getTweets - " + we.Message);
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("TwitterRESTAPIRepository::getTweets - " + ex.Message);
                return null;
            }

            string timeLineJson = null;

            using (timeLineResponse)
            {
                using (var reader = new StreamReader(timeLineResponse.GetResponseStream()))
                {
                    timeLineJson = reader.ReadToEnd();
                }
            }
            
            //Console.WriteLine(timeLineJson);
            TwitterPost [] tweets = JsonConvert.DeserializeObject<TwitterPost[]>(timeLineJson);
            return tweets;
        }

        protected void writePostArrayToList(string screenName, TwitterPost[] posts, ref List<TwitterPost> postList)
        {
            for (int i = 0; i < posts.Length; i++)
            {
                //This is hacky, but it saves us from requesting the user data
                posts[i].UserID = screenName;
                Debug.WriteLine(posts[i].id + " : " + posts[i].created_at);
                postList.Add(posts[i]);
            }
        }
        
        protected void getTweetsSince(string screenName, DateTime since, Int64 max_id, ref List<TwitterPost> postList)
        {
            TwitterPost[] posts = null;
            
            posts = getTweets(screenName, max_id);
            
            TwitterPost oldest = posts[posts.Length - 1];
            if (!(posts.Length < TWEET_REQUEST_COUNT) && oldest.created_at_datetime >= since)
            {
                if (0 == max_id)
                {
                    //Doing this so that I can visualize the construction of the
                    //post list every step of the way. Can add a DEBUG conditional later on to improve
                    //performance, and use AddRange instead.
                    writePostArrayToList(screenName,posts, ref postList);
                }
                else
                {
                    //In case this app is running in a 32-bit environment,
                    //you must remove the first tweet in requests after the intial,
                    //because it will be duplicated in the previous set.
                    //See: https://dev.twitter.com/docs/working-with-timelines
                    for (int i = 1; i < posts.Length; i++)
                    {
                        posts[i].UserID = screenName;
                        Debug.WriteLine(posts[i].id + " : " + posts[i].created_at);
                        postList.Add(posts[i]);
                    }

                }
                
                //In case we were running into request rate limiting issues
                //Thread.Sleep(1000);
                
                Console.WriteLine("Oldest: " + oldest.created_at + " \r\nID: " + oldest.id);
                getTweetsSince(screenName, since, oldest.id, ref postList);
            }
            else
            {
                writePostArrayToList(screenName, posts, ref postList);
            }

            return; //postList;
        }

        public IEnumerable<IUserWebPost> FindAll(object [] args)
        {
            return FindAll((string)args[0], (DateTime)args[1]);
        }


        public IEnumerable<IUserWebPost> FindAll(string screenName, DateTime since)
        {
            List<TwitterPost> posts = new List<TwitterPost>();
            getTweetsSince(screenName, since, 0, ref posts);
            //List<IUserWebPost> toReturn = new List<IUserWebPost>();
            //toReturn.AddRange(posts.ToArray<IUserWebPost>());
            return (List<IUserWebPost>)posts.ConvertAll(x => (IUserWebPost)x);
        }

        public IUserWebPost Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public IUserWebPost Add(IUserWebPost t)
        {
            throw new NotImplementedException();
        }

        public void Delete(IUserWebPost t)
        {
            throw new NotImplementedException();
        }
    }


}
