using ArdenHotel.Models;
using ArdenHotel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArdenHotel.ViewComponents
{
    public class GetExchangeRate:ViewComponent
    {
        private readonly IGetExchangeRate getExchangeRate;
        private IMemoryCache memoryCache;
        public GetExchangeRate(IGetExchangeRate getExchangeRate, IMemoryCache memoryCache)
        {
            this.getExchangeRate = getExchangeRate;
            this.memoryCache = memoryCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            const string cacheKey = "ExchangeRate";
            List<ExchangeRate> cacheEntry;
            if (!memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = await getExchangeRate.ExchangeRateAsync();

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(15),
                    Priority = CacheItemPriority.Normal
                };
                memoryCache.Set(cacheKey,cacheEntry, cacheExpOptions);

            }

            return View(cacheEntry);
        }
    }
}
