using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitgregator.DAL.Framework;
using Twitgregator.Posts.Framework;
using Twitgregator.Posts.Stats.Framework;

namespace Twitgregator.Projections.Framework
{
    //This would allow us to create a similar sort of data projection
    //as required by this exercise, with any other type of web post
    //(i.e. Tumblr, or the Twitter runner-up/replacement when Twitter fails :) )
    public abstract class AbstractWebPostViewModelFactory
    {
        public virtual WebPostViewModel createViewModel(IRepository<IUserWebPost> repository, object[] args)
        {
            List<string> names = (List<string>)args[0];
            List<IUserWebPost> posts = new List<IUserWebPost>();
            List<UserWebPostStats> stats = new List<UserWebPostStats>();

            //***NB: To improve performance, we should probably fetch these asynchronously,
            //but I'm short on time for this test... :)
            foreach (string userName in names)
            {
                //1)Get post list from the repository
                object[] newArgs = new object[] { userName, args[1] };
                List<IUserWebPost> l = getPosts(repository, newArgs);
                
                //2)Get stats about the posts
                stats.AddRange(getStats(l,userName));
                
                posts.AddRange(l);
            }

            //3)Create the ViewModel
            WebPostViewModel vm = assembleViewModelInstance(posts, stats.ToArray());

            //4)Format the list, if necessary (i.e. sort it somehow)
            formatList(vm);

            return vm;
        }

        protected abstract List<IUserWebPost> getPosts(IRepository<IUserWebPost> repository, object[] args);

        protected abstract UserWebPostStats [] getStats(List<IUserWebPost> posts, string screenName);

        protected abstract void formatList(WebPostViewModel vm);

        protected abstract WebPostViewModel assembleViewModelInstance(List<IUserWebPost> posts, UserWebPostStats[] stats);
    }
}
