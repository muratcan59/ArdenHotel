﻿using ArdenHotel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArdenHotel.Services
{
    public interface IGetWeather
    {
        Task<OpenWeatherResponse> GetWeatherForArdenAsync();
    }
}
