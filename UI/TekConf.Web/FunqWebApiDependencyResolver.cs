namespace TekConf.Web
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Http.Dependencies;

    using Funq;

    public sealed class FunqWebApiDependencyResolver : IDependencyResolver
    {
        private readonly Container _container;

        public FunqWebApiDependencyResolver(Container container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public IDependencyScope BeginScope()
        {
            return this;
        }

        [DebuggerStepThrough]
        public object GetService(Type serviceType)
        {
            try
            {
                var typeToResolve = serviceType;

                var method = _container.GetType()
                    .GetMethods()
                    .First(m => m.Name == "Resolve" && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 1);
                var instance = method.MakeGenericMethod(typeToResolve).Invoke(_container, null);

                return instance;

            }
            catch (Exception)
            {
                return null;
            }
        }

        [DebuggerStepThrough]
        public IEnumerable<object> GetServices(Type serviceType)
        {
            var service = GetService(serviceType);
            return new object[] { service }.AsEnumerable();
        }

        [DebuggerStepThrough]
        public void Dispose()
        {
        }
    }
}