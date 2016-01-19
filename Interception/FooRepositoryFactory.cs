using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterceptionTest
{
    class FooRepositoryFactory
    {
        public static IRepository<T> Create<T>()
        {
            var repo = new FooRepository<T>();
            var loggerProxy = new LoggerProxy<IRepository<T>>(repo);

            return loggerProxy.GetTransparentProxy() as IRepository<T>;
        }
    }
}
