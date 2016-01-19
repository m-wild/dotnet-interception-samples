using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InterceptionTest
{
    class LoggingCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            for (var i = 0; i < input.Arguments.Count; i++)
            {
                SLog.Green($"{input.Arguments.ParameterName(i)} = {input.Arguments[i]}");
            }

            SLog.Red($"In LoggingCallHandler - Before executing {input.MethodBase}");

            var res = getNext()(input, getNext);

            if (res.Exception == null)
            {
                SLog.Red($"In LoggingCallHandler - After executing {input.MethodBase}");
            }
            else
            {
                SLog.Red($"In LoggingCallHandler - Exception {res.Exception.Message} executing {input.MethodBase}");
            }

            return res;
        }


        public int Order
        {
            get { return 2; }
            set { throw new InvalidOperationException(); }
        }
    }
}
