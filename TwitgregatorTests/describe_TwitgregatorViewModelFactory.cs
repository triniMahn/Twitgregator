using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSpec;
using Twitgregator.DAL.Implementation;
using Twitgregator.DAL.Framework;
using Twitgregator;
using Twitgregator.Posts.Framework;
using Twitgregator.Posts.Implementation;
using Twitgregator.Projections.Framework;
using Twitgregator.Projections.Implementation;

namespace TwitgregatorTests
{
    class MockTweetRepository : IRepository<IUserWebPost>
    {
        //List<IUserWebPost> posts;

        
        
        public IEnumerable<IUserWebPost> FindAll(object[]args)
        {
            return TestData.getSmallUnorderedPostList();
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
    
    class describe_TwitgregatorViewModelFactory: nspec
    {
        void before_each()
        {
            
        }
        
        void when_a_sample_data_source_is_provided()
        {
            //System.Diagnostics.Debugger.Launch();
            MockTweetRepository repo = new MockTweetRepository();
            TwitterViewModelFactory factory = new TwitterViewModelFactory();
            List<string> screenNames = new List<string>();
            screenNames.Add("doesnot_matter");
            TwitterPostListViewModel vm = (TwitterPostListViewModel)factory.createViewModel(repo, new object[]{screenNames,DateTime.Now});
            
            it["should have a set of 2 sets of UserWebPostStats"] = () => vm.Stats.Count().should_be(2);
        }

        List<TwitterPost> posts;

        
    }
}
