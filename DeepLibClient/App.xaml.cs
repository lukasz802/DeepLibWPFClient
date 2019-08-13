using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DeepLibClient
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        public const string SERVICE_URL = "https://localhost:44365/";

        public static MyAPI GetDeepLibClient(string uri = SERVICE_URL)
        {
            MyAPI client = new MyAPI(new Uri(uri), new BasicAuthenticationCredentials());
            return client;
        }
    }
}
