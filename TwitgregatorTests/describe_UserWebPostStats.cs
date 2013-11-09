using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSpec;
using Twitgregator;
using Twitgregator.Posts.Framework;
using Twitgregator.Posts.Stats.Framework;
using Twitgregator.Posts.Stats.Implementation;

namespace TwitgregatorTests
{
    class describe_UserWebPostStats : nspec
    {
        List<IUserWebPost> posts = null;
        
        void before_each()
        {
            posts = TestData.getSmallOrderedPostList();
        }
        
        void when_a_post_count_is_requested()
        {
            
            it["should have a count of 3"] = () =>
            {
                //System.Diagnostics.Debugger.Launch();
                
                UserWebPostStats stats = new TwitterPostCount();
                List<UserWebPostStat> stat = stats.computeStats(posts);
                stat.FirstOrDefault<UserWebPostStat>().Count.should_be(3);
            };
        }

        void when_a_mention_count_is_requested()
        {
            UserWebPostStats stats=null;
            List<UserWebPostStat> stat=null;

            before = () =>
            {
                stats = new TwitterPostUserMentionCount();
                stat = stats.computeStats(posts);
            };
            
            it["should have a stats list length, by user, of 4"] = () =>
                {
                    //System.Diagnostics.Debugger.Launch();
                    
                    stat.Count.should_be(4);
                };

            it["should describe Bill Gates as having 2 mentions"] = () =>
                {
                    stat.FindIndex(x => x.UserID == "BillGates" && x.Count == 2).should_be_greater_or_equal_to(0);
                    
                };
        }
        
        
    }
}
