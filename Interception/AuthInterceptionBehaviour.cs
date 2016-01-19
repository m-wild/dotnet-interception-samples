using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InterceptionTest
{
    class AuthInterceptionBehaviour : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            if (!Thread.CurrentPrincipal.IsInRole("ADMIN"))
                throw new SecurityException("Access Denied");

            return getNext()(input, getNext);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute => true;
    }
}
