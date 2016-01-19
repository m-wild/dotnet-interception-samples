using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InterceptionTest
{
    class AuthCallHandlerAttribute : HandlerAttribute
    {
        private readonly string _role;
        public AuthCallHandlerAttribute(string role)
        {
            _role = role;
        }
        
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new AuthCallHandler { Role = _role };
        }
    }
}
