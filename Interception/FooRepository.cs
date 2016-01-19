using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterceptionTest
{
    [LoggingCallHandler]
    public class FooRepository<T> : IRepository<T>
    {
        [AuthCallHandler("USER")]
        public void Add(T entity)
        {
            Console.WriteLine($"Adding {entity}");
        }

        [AuthCallHandler("ADMIN")]
        public void Delete(T entity)
        {
            Console.WriteLine($"Deleting {entity}");
        }

        [AuthCallHandler("USER")]
        public void Update(T entity)
        {
            Console.WriteLine($"Updating {entity}");
        }

        public IEnumerable<T> GetAll()
        {
            Console.WriteLine("Getting entities");
            return null;
        }

        public T GetById(int id)
        {
            Console.WriteLine($"Getting entity {id}");
            return default(T);
        }
    }
}
