using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitgregator.DAL.Framework
{
    public interface IRepository<T>
    {
        //Probably not the most descriptive way to pass arguments,
        //but it makes for easy flexibility
        IEnumerable<T> FindAll(object[] args);

        T Get(int id);

        void Save();

        T Add(T t);

        void Delete(T t);

    }
}
