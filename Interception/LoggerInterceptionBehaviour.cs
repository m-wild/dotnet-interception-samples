using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InterceptionTest
{
    class LoggerInterceptionBehaviour : IInterceptionBehavior
    {

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            SLog.Red($"In LoggerInterceptionBehavor - Before executing {input.MethodBase}");

            var res = getNext()(input, getNext);

            if (res.Exception == null)
            {
                SLog.Red($"In LoggerInterceptionBehavor - After executing {input.MethodBase}");
            }
            else
            {
                SLog.Red($"In LoggerInterceptionBehavor - Exception {res.Exception.Message} executing {input.MethodBase}");
            }

            return res;
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute => true;
    }
}
