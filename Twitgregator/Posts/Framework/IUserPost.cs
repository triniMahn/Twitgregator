using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitgregator.Posts.Framework
{
    //To save time and testing of a facade or adapter, the property names
    //are the same as Twitter's for now -- can refactor later.
    public interface IUserWebPost
    {
        string UserID { get; set; }
        string text { get; set; }
        string created_at { get; set; }
        DateTime created_at_datetime { get; }
    }
}
