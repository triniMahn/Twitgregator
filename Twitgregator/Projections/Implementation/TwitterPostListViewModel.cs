using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitgregator.Projections.Framework;
using Twitgregator.Posts.Framework;
using Twitgregator.Posts.Stats.Framework;

namespace Twitgregator.Projections.Implementation
{
    public class TwitterPostListViewModel : WebPostViewModel
    {
        public TwitterPostListViewModel(IUserWebPost[] posts, UserWebPostStats[] stats)
        {
            this.Posts = posts;
            this.Stats = stats;
        }
    }
}
