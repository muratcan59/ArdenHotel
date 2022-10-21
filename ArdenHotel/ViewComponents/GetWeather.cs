using ArdenHotel.Models;
using ArdenHotel.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArdenHotel.ViewComponents
{
    public class GetWeather : ViewComponent
    {
        private readonly IGetWeather getWeather;
        private IMemoryCache memoryCache;
        public GetWeather(IGetWeather getWeather, IMemoryCache memoryCache)
        {
            this.getWeather = getWeather;
            this.memoryCache = memoryCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            const string cacheKey = "Weather";
            OpenWeatherResponse cacheEntry;
            if (!memoryCache.TryGetValue(cacheKey, out cacheEntry))
            {
                cacheEntry = await getWeather.GetWeatherForArdenAsync();

                var cacheExpOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(15),
                    Priority = CacheItemPriority.Normal
                };
                memoryCache.Set(cacheKey, cacheEntry, cacheExpOptions);

            }
            return View(cacheEntry);
        }
    }
}
