using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;


namespace InterceptionTest
{
    public class LoggerProxy<T> : RealProxy
    {
        private readonly T _decorated;

        public LoggerProxy(T decorated) 
            : base(typeof(T))
        {
            _decorated = decorated;
        }


        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;

            SLog.Red($"In LoggerProxy - Before executing {methodCall.MethodName}");

            try
            {
                var res = methodInfo.Invoke(_decorated, methodCall.InArgs);

                SLog.Red($"In LoggerProxy - After executing {methodCall.MethodName}");

                return new ReturnMessage(res, null, 0, methodCall.LogicalCallContext, methodCall);

            }
            catch (Exception ex)
            {
                SLog.Red($"In LoggerProxy - Exception {ex.Message} executing {methodCall.MethodName}");

                return new ReturnMessage(ex, methodCall);
            }


        }
    }
}
