using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSpec;
using Twitgregator.DAL.Implementation;


namespace TwitgregatorTests
{
    class describe_TwitterDataSource: nspec
    {
        //These two tests could be marked as pending at any time
        //to ensure that we're not exhausting Twitter request limits
        void given_a_Twitter_screen_name()
        {
            
            TwitterRESTAPIRepository repo = new TwitterRESTAPIRepository();
            //sgdddsdd
            //System.Diagnostics.Debugger.Launch();
            //Assuming that the pay_by_phone account will post more than 5 times in 5 days. Dangerous assumption? Perhaps.
            xit["should give us some tweets"] = () => repo.FindAll("pay_by_phone", DateTime.Now.AddDays(-5)).Count().should_be_greater_or_equal_to(TwitterRESTAPIRepository.TWEET_REQUEST_COUNT);
        }

        void given_a_since_date()
        {
            //System.Diagnostics.Debugger.Launch();
            TwitterRESTAPIRepository repo = new TwitterRESTAPIRepository();
            DateTime twoWeeksAgo = DateTime.Now.AddDays(-14);
            xit["the last tweet date should be less than 2 weeks old"] = () => repo.FindAll("pay_by_phone", twoWeeksAgo).Last().created_at_datetime.should_be_greater_or_equal_to(twoWeeksAgo);
        }
    }
}
