using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InterceptionTest
{
    class LoggingCallHandlerAttribute : HandlerAttribute
    {
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new LoggingCallHandler();
        }
    }
}
