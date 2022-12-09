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
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            _cacheService.Remove(key);
        }
    }
}
