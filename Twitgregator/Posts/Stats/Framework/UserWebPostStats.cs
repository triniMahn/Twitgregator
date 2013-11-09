using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitgregator.Posts.Framework;

namespace Twitgregator.Posts.Stats.Framework
{
    //Could use this later to encapsulate post list-based operations,
    //as it will probably make the code cleaner, but don't have time right now
    //"Shipping beats perfection." - Salman Khan
    public abstract class UserWebPostList
    {
        protected List<IUserWebPost> posts = null;

        public abstract int getUserStats();
        
    }

    public abstract class UserWebPostStat
    {
        public string UserID { get; set; }
        public int Count { get; set; }
    }
    
    public abstract class UserWebPostStats
    {
        protected List<UserWebPostStat> stats = null;
        protected string statDescription = null;

        public string UserName { get; set; }
        
        public string Description
        {
            get { return statDescription; }
        }

        public UserWebPostStat[] Stats
        {
            get { return stats.ToArray(); }
        }

        public List<UserWebPostStat> computeStats(List<IUserWebPost> posts)
        {
            if (null == posts || 0 == posts.Count)
                return new List<UserWebPostStat>();

            if (null == stats)
                createStatList(posts);

            return stats;
        }
        
        protected abstract void createStatList(List<IUserWebPost> posts);
    }
}
