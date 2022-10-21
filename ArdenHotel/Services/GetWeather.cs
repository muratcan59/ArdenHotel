using ArdenHotel.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace ArdenHotel.Services
{
    public class GetWeather : IGetWeather
    {
        public async Task<OpenWeatherResponse> GetWeatherForArdenAsync()
        {
           

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri("https://api.openweathermap.org");
                    var response = await client.GetAsync("/data/2.5/weather?q=didim&appid=ed26eb708b115d3580f0f670c6a4c1d2&lang=tr&units=metric");
                    response.EnsureSuccessStatusCode();
                    var stringResult = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var rawWeather = JsonConvert.DeserializeObject<OpenWeatherResponse>(stringResult);
                        return rawWeather;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

    }
}
