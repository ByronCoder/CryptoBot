﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace CryptoBot
{
    public class CryptoPriceProcessor
    {
       public static string GetBitCoinPrice(string date)
        {
           

            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(String.Format("https://rest.coinapi.io/v1/ohlcv/BITSTAMP_SPOT_BTC_USD/latest?period_id=1DAY&limit=1&time_start={0}", date));
            WebReq.Headers.Add("X-CoinAPI-Key", "Enter API Key");
            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();




            string jsonString;

            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

           
            var c = JsonConvert.DeserializeObject<List<Coin>>(jsonString);
            return c[0].price_close;

        }
    }
}
