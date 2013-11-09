using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitgregator.Posts.Framework;
using Twitgregator.Posts.Implementation;
using Twitgregator.Posts.Stats.Framework;

namespace Twitgregator.Posts.Stats.Implementation
{
    public class TwitterPostCountStat : UserWebPostStat
    {

    }

    public class TwitterMentionCountStat : UserWebPostStat { }

    public class TwitterPostCount : UserWebPostStats
    {
        public TwitterPostCount()
        {
            this.statDescription = "The Total number of posts retrieved.";
        }

        protected override void createStatList(List<IUserWebPost> posts)
        {
            this.stats = new List<UserWebPostStat>();

            string userID = posts[0].UserID;
            int count = posts.Count;

            stats.Add(new TwitterPostCountStat { UserID = userID, Count = count });


        }
    }

    public class TwitterPostUserMentionCount : UserWebPostStats
    {
        public TwitterPostUserMentionCount()
        {
            this.statDescription = "The Total number of times another user was mentioned in the list of tweets";
        }

        /// <summary>
        /// Runs with complexity O(nm), where m is always bounded, and probably can be considered a scalar.
        /// </summary>
        /// <param name="posts"></param>
        protected override void createStatList(List<IUserWebPost> posts)
        {
            ConcurrentDictionary<string, int> mentions = null;
            this.stats = new List<UserWebPostStat>();

            mentions = new ConcurrentDictionary<string, int>();
            TwitterPost tp = null;

            //Might be able to use a LINQ GroupBy here, to make the code cleaner,
            //but I'm not sure how to do this right now with collections inside of collections
            //LINQ's probably doing the same under the hood (?).
            foreach (IUserWebPost post in posts)
            {
                tp = (TwitterPost)post;
                foreach (user_mentions user in tp.entities.user_mentions)
                    mentions.AddOrUpdate(user.screen_name, 1, (key, oldValue) => ++oldValue);
            }

            foreach (KeyValuePair<string, int> m in mentions)
                stats.Add(new TwitterMentionCountStat { UserID = m.Key, Count = m.Value });


        }
    }
}
