using ArdenHotel.Models;
using System;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Globalization;
using System.Collections.Generic;

namespace ArdenHotel.Services
{
    public class GetExchangeRate : IGetExchangeRate
    {
        public async Task<List<ExchangeRate>> ExchangeRateAsync()
        {
            await Task.CompletedTask;
            var currency = XDocument
            .Load("http://www.tcmb.gov.tr/kurlar/today.xml")
            .Descendants("Currency")
            .Where(p => !string.IsNullOrEmpty(p.Element("ForexSelling").Value))
            .Select(p => new ExchangeRate
            {
                Code = p.Attribute("Kod").Value,
                Name = p.Element("Isim").Value,
                Buying = decimal.Parse(p.Element("ForexBuying").Value, CultureInfo.InvariantCulture),
                Selling = decimal.Parse(p.Element("ForexSelling").Value, CultureInfo.InvariantCulture)
            }).ToList();
            if (currency != null)
            {
                return currency;

            }
            else
            {
                return new List<ExchangeRate>();
            }
        }
    }
}
