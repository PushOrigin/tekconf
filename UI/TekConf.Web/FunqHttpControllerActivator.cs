namespace TekConf.Web
{
    using System;
    using System.Linq;
    using System.Web.Http.Controllers;
    using System.Web.Http.Dispatcher;

    using Funq;

    public class FunqHttpControllerActivator : IHttpControllerActivator
    {
        private readonly Container _container;

        public FunqHttpControllerActivator(Container container)
        {
            _container = container;
        }

        public IHttpController Create(HttpControllerContext controllerContext, Type controllerType)
        {
            try
            {
                var typeToResolve = controllerType;

                var method = _container.GetType()
                    .GetMethods()
                    .First(m => m.Name == "Resolve" && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 1);
                var instance = method.MakeGenericMethod(typeToResolve).Invoke(_container, null);

                return (IHttpController)instance;

            }
            catch (Exception)
            {
                return null;
            }


        }

        public IHttpController Create(System.Net.Http.HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            try
            {
                var typeToResolve = controllerType;

                var method = _container.GetType()
                    .GetMethods()
                    .First(m => m.Name == "Resolve" && m.IsGenericMethodDefinition && m.GetGenericArguments().Length == 1);
                var instance = method.MakeGenericMethod(typeToResolve).Invoke(_container, null);

                return (IHttpController)instance;

            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}