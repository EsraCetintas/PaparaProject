using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using PaparaProject.Application.Interfaces.Infrastructure;
using PaparaProject.Application.Utilities.Interceptors;
using PaparaProject.Application.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Application.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        private ICacheService _cacheService;

        public CacheRemoveAspect()
        {
            _cacheService = ServiceTool.ServiceProvider.GetService<ICacheService>();
        }

        protected override void OnSuccess(IInvocation invocation)
        {
            var key = string.Format($"{invocation.Method.ReflectedType.FullName}");
            _cacheService.Remove(key);
        }
    }
}
