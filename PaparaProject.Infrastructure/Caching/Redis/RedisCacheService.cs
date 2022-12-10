using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PaparaProject.Application.Interfaces.Infrastructure;
using PaparaProject.Application.Utilities.IoC;
using PaparaProject.Application.Utilities.Results;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaProject.Infrastructure.Caching.Redis
{
    public class RedisCacheService : ICacheService
    {
        private RedisServer _redisServer;

        public RedisCacheService()
        {
            _redisServer = ServiceTool.ServiceProvider.GetService<RedisServer>();
            //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //};
        }

      

    public void Add(string key, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data.GetType().GetProperty("Result").GetValue(data, null));
            _redisServer.Database.StringSet(key, jsonData);
        }

        public bool Any(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public async Task<APIResult> Get(string key)
        {
            if (Any(key))
            {
               string jsonData = await _redisServer.Database.StringGetAsync(key);
                APIResult data = JsonConvert.DeserializeObject<APIResult>(jsonData);
                return new APIResult { 
                    Data = jsonData,
                    Message = data.Message,
                    Success = data.Success 
                };
            }

            return default;
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public void Clear()
        {
            _redisServer.FlushDatabase();
        }
    }
}
