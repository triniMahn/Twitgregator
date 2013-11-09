using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSpec;
using Twitgregator.Posts.Framework;
using Twitgregator.Projections.Framework;
using Twitgregator.Projections.Implementation;

namespace TwitgregatorTests
{
    class describe_WebPostViewModel:nspec
    {
        List<IUserWebPost> posts; 

        void an_unordered_post_list()
        {
            it["should be able to sort the list by date"] = () =>
                {
                    posts = TestData.getSmallUnorderedPostList();
                    WebPostViewModel vm = new TwitterPostListViewModel(posts.ToArray(),null);
                    vm.sortPosts();
                    vm.Posts[0].created_at_datetime.should_be_greater_or_equal_to(vm.Posts[vm.Posts.Length - 1].created_at_datetime);

                };
        }
    }
}
