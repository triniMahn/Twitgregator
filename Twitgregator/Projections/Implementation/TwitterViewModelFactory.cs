using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitgregator.Projections.Framework;
using Twitgregator.Projections.Implementation;
using Twitgregator.DAL.Framework;
using Twitgregator.Posts.Framework;
using Twitgregator.Posts.Stats.Framework;
using Twitgregator.Posts.Stats.Implementation;

namespace Twitgregator.Projections.Implementation
{
    public class TwitterViewModelFactory : AbstractWebPostViewModelFactory
    {
        protected override List<IUserWebPost> getPosts(IRepository<IUserWebPost> repository, object[] args)
        {
            List<IUserWebPost> posts = (List<IUserWebPost>)repository.FindAll(args);
            return posts;
        }

        protected override UserWebPostStats[] getStats(List<IUserWebPost> posts,string screenName)
        {
            List<UserWebPostStats> stats = new List<UserWebPostStats>();
            TwitterPostCount stat1 = new TwitterPostCount();
            stat1.UserName = screenName;
            stat1.computeStats(posts);
            
            TwitterPostUserMentionCount stat2 = new TwitterPostUserMentionCount();
            stat2.UserName = screenName;
            stat2.computeStats(posts);

            stats.Add(stat1);
            stats.Add(stat2);

            return stats.ToArray();
        }

        protected override void formatList(WebPostViewModel vm)
        {
            vm.sortPosts();
        }

        protected override WebPostViewModel assembleViewModelInstance(List<IUserWebPost> posts, UserWebPostStats[] stats)
        {
            TwitterPostListViewModel vm = new TwitterPostListViewModel(posts.ToArray(), stats);
            return vm;
        }
    }
}
