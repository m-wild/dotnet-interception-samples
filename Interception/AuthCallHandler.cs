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
    [LoggingCallHandler]
    class AuthCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            SLog.Cyan($"User {Thread.CurrentPrincipal.Identity.Name} attepting to execute {input.MethodBase} - Required Role = {Role}");

            if (!Thread.CurrentPrincipal.IsInRole(Role))
            {
                throw new SecurityException("Access Denied");
            }
            else
            {
                SLog.Cyan("Access granted");
                return getNext()(input, getNext);
            }
        }

        public int Order
        {
            get { return 1; }
            set { throw new InvalidOperationException(); }
        }
        public string Role { get; set; }
    }
}
