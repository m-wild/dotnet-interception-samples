using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace InterceptionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("***\nBegin program\n");

            // 1. --- basic
            // IRepository<Customer> customerRepo = new FooRepository<Customer>();
            

            // 2. --- realproxy
            //IRepository<Customer> customerRepo = FooRepositoryFactory.Create<Customer>();


            // 3. --- unity interception
            IUnityContainer container = new UnityContainer();
            container.AddNewExtension<Interception>();

            // 3.1 -- no policy
            //container.RegisterType(typeof(IRepository<>), typeof(FooRepository<>),
            //    new Interceptor<InterfaceInterceptor>(),
            //    // adds the floowing behaviours to every method call in the resolved type
            //    new InterceptionBehavior<AuthInterceptionBehaviour>(),
            //    new InterceptionBehavior<LoggerInterceptionBehaviour>());

            // 3.2 -- generic policy
            // you'll need to remove the attributes from the types for this to work
            //container.RegisterType(typeof(IRepository<>), typeof(FooRepository<>),
            //    new InterceptionBehavior<PolicyInjectionBehavior>(),
            //    new Interceptor<InterfaceInterceptor>());

            //container.Configure<Interception>()
            //    .AddPolicy("logging")
            //    .AddMatchingRule<AssemblyMatchingRule>(
            //        new InjectionConstructor(
            //                new InjectionParameter("InterceptionTest")))
            //    .AddCallHandler<LoggingCallHandler>(
            //        new ContainerControlledLifetimeManager(),
            //        new InjectionConstructor());


            // 3.3 attribute based policy
            container.RegisterType(typeof(IRepository<>), typeof(FooRepository<>),
                new InterceptionBehavior<PolicyInjectionBehavior>(),
                new Interceptor<InterfaceInterceptor>());


            // 3.* -- always resolve
            IRepository<Customer> customerRepo = container.Resolve<IRepository<Customer>>();



            Console.Write("Admin? (Y/N) ");
            if (!(Console.ReadLine().Trim().ToLower() == "y"))
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("Foo"), new[] {"USER"});
            }
            else
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity("Super Foo"), new[] { "USER", "ADMIN" });
            }






            // ----------- below (ie. our main app logic) never changes
            var cust = new Customer
            {
                Id = 1,
                Name = "Big Boss",
                Address = "1 Mother Base"
            };

            customerRepo.Add(cust);
            Console.WriteLine();

            customerRepo.Update(cust);
            Console.WriteLine();

            customerRepo.Delete(cust);
            Console.WriteLine();


            Console.WriteLine("\nEnd program\n***");
            Console.ReadLine();
        }
    }
}
