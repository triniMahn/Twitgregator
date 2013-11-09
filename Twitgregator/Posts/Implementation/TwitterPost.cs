using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Twitgregator.Posts.Framework;

namespace Twitgregator.Posts.Implementation
{
    public class user_mentions
    {
        public string screen_name { get; set; }
    }
    
    public class entities
    {
        public user_mentions [] user_mentions { get; set; }
    }
    public class TwitterPost: IUserWebPost
    {
        
        public string UserID
        {
            get;
            set;
        }

        public Int64 id
        {
            get;
            set;
        }
        
        public string text
        {
            get;
            set;
        }

        public string created_at
        {
            get;
            set;
        }

        public DateTime created_at_datetime
        {
            get
            {
                return getDateFromTwitterDateString(this.created_at);
            }
        }

        public entities entities { get; set; }

        public static DateTime getDateFromTwitterDateString(string twitterDate)
        {
            return DateTime.ParseExact(twitterDate,
                    "ddd MMM dd HH:mm:ss %K yyyy",
                    CultureInfo.InvariantCulture.DateTimeFormat);
        }
    }
}
