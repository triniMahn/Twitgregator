using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitgregator.Posts.Framework;
using Twitgregator.Posts.Stats.Framework;

namespace Twitgregator.Projections.Framework
{
    public abstract class WebPostViewModel
    {
        public IUserWebPost[] Posts
        {
            get;
            protected set;
        }
        public UserWebPostStats[] Stats { get;  protected set; }
        
        //protected List<IUserWebPost> posts = null; 

        public virtual void sortPosts()
        {
            Array.Sort<IUserWebPost>(this.Posts, (p1, p2) => DateTime.Compare(p2.created_at_datetime, p1.created_at_datetime));
            //List<IUserWebPost> posts = new List<IUserWebPost>();
            //posts.AddRange(this.Posts);
            //posts.Sort((p1, p2) => DateTime.Compare(p2.created_at_datetime, p1.created_at_datetime));
            //this.Posts = posts.ToArray();
        }

        
        
    }
}
