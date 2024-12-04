using AutoMapper;
using Ninject;
using Ninject.Web.Common;
using Project.Core.Security;
using Project.Features.Implements;
using Project.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Api.Infrastructure
{
    public class DependencyInjectionResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public DependencyInjectionResolver(IKernel kernel)
        {
            this.kernel = kernel;

            BindingServices();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void BindingServices()
        {
            kernel.Bind<IPasswordHasher>().To<PasswordHasher>();


            kernel.Bind<IClinicBranch>().To<ClinicBranchService>().InRequestScope();
        }
    }
}