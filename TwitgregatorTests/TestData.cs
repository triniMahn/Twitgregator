using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitgregator;
using Twitgregator.Posts.Framework;
using Twitgregator.Posts.Implementation;

namespace TwitgregatorTests
{
    public class TestData
    {
        
        public static List<IUserWebPost> getSmallOrderedPostList()
        {
            List<IUserWebPost> posts = new List<IUserWebPost>();
            posts.Add(new TwitterPost
            {
                UserID = "triniMahn",
                created_at = "Thu Oct 31 16:36:56 +0000 2013",
                text = "This post is cool",
                entities = new entities
                {
                    user_mentions = new user_mentions[]{ 
                        new user_mentions{screen_name = "BillGates"}
                    }
                }
            });

            posts.Add(new TwitterPost
            {
                UserID = "triniMahn",
                created_at = "Fri Oct 25 12:55:57 +0000 2013",
                text = "This post is also cool",
                entities = new entities
                {
                    user_mentions = new user_mentions[]{ 
                        new user_mentions{screen_name = "vincenttux"},
                        new user_mentions{screen_name = "fat"}
                    }
                }
            });

            posts.Add(new TwitterPost
            {
                UserID = "triniMahn",
                created_at = "Fri Oct 25 08:47:48 +0000 2013",
                text = "This post is way cooler",
                entities = new entities
                {
                    user_mentions = new user_mentions[]{ 
                        new user_mentions{screen_name = "PayByPhone"},
                        new user_mentions{screen_name = "fat"},
                        new user_mentions{screen_name = "BillGates"}
                    }
                }
            });

            return posts;
        }

        public static List<IUserWebPost> getSmallUnorderedPostList()
        {
            List<IUserWebPost> posts = new List<IUserWebPost>();

            posts = new List<IUserWebPost>();
            posts.Add(new TwitterPost
            {
                UserID = "triniMahn",
                created_at = "Thu Oct 31 16:36:56 +0000 2013",
                text = "3. This post is cool",
                entities = new entities
                {
                    user_mentions = new user_mentions[]{ 
                        new user_mentions{screen_name = "BillGates"}
                    }
                }
            });

            posts.Add(new TwitterPost
            {
                UserID = "triniMahn",
                created_at = "Fri Oct 25 08:47:48 +0000 2013",
                text = "1. This post is way cooler",
                entities = new entities
                {
                    user_mentions = new user_mentions[]{ 
                        new user_mentions{screen_name = "PayByPhone"},
                        new user_mentions{screen_name = "fat"},
                        new user_mentions{screen_name = "BillGates"}
                    }
                }
            });

            posts.Add(new TwitterPost
            {
                UserID = "triniMahn",
                created_at = "Fri Oct 25 12:55:57 +0000 2013",
                text = "2. This post is also cool",
                entities = new entities
                {
                    user_mentions = new user_mentions[]{ 
                        new user_mentions{screen_name = "vincenttux"},
                        new user_mentions{screen_name = "fat"}
                    }
                }
            });

            return posts;
        }
    }
}
