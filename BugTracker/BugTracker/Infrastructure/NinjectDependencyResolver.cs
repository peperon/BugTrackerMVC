using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using System.Web.Mvc;
using BugTracker.Domain.RepositoryInterfaces;
using BugTracker.Domain.RepositoryConcrete;

namespace BugTracker.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            _kernel.Bind<IUserRepository>().To<EFUserRepository>();
            _kernel.Bind<IErrorRepository>().To<EFErrorRepository>();
            _kernel.Bind<IProjectRepository>().To<EFProjectRepository>();
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public IKernel Kernel
        {
            get { return _kernel; }
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
    }
}