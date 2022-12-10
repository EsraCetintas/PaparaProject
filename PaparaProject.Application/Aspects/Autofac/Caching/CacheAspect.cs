using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using PaparaProject.Application.Interfaces.Infrastructure;
using PaparaProject.Application.Utilities.Interceptors;
using PaparaProject.Application.Utilities.IoC;
using PaparaProject.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace PaparaProject.Application.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private ICacheService _cacheService;
        public CacheAspect()
        {
            _cacheService = ServiceTool.ServiceProvider.GetService<ICacheService>();

        }

        public override void Intercept(IInvocation invocation)
        {
            var key = string.Format($"{invocation.Method.ReflectedType.FullName}");
            if (_cacheService.Any(key))
            {
                invocation.ReturnValue = _cacheService.Get(key);
                return;
            }
            invocation.Proceed();
            _cacheService.Add(key, invocation.ReturnValue);
        }
    }
}
