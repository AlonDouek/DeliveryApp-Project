using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DeliveryApp.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.IO;

namespace DeliveryApp.Services
{
    class DeliveryAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        //change ips
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:21604/DeliveryAPI"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://10.58.55.7:21604/DeliveryAPI"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "https://localhost:44331/DeliveryAPI"; //API url when using windoes on development

        private HttpClient client;
        private string baseUri;
        private string basePhotosUri;
        private static DeliveryAPIProxy proxy = null;

        public static DeliveryAPIProxy CreateProxy()
        {
            string baseUri;

            if (App.IsDevEnv)
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    if (DeviceInfo.DeviceType == DeviceType.Virtual)
                    {
                        baseUri = DEV_ANDROID_EMULATOR_URL;
                    }
                    else
                    {
                        baseUri = DEV_ANDROID_PHYSICAL_URL;
                    }
                }
                else
                {
                    baseUri = DEV_WINDOWS_URL;
                }
            }
            else
            {
                baseUri = CLOUD_URL;
            }

            if (proxy == null)
                proxy = new DeliveryAPIProxy(baseUri);
            return proxy;
        }


        private DeliveryAPIProxy(string baseUri)
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            //Create client with the handler!
            this.client = new HttpClient(handler, true);
            this.baseUri = baseUri;
        }

        public async Task<User> LoginAsync(string email, string pass)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/Login?email={email}&pass={pass}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User u = JsonSerializer.Deserialize<User>(content, options);
                    return u;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //change when know how cause im no sure
        public async Task<User> SignUpAsync(string firstName, string lastName, string email, DateTime birthDate, string username, string pswd)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/SignUp?firstName={firstName}&lastName={lastName}&email={email}&dt={birthDate}&username={username}&password={pswd}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User p = JsonSerializer.Deserialize<User>(content, options);
                    return p;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
//dibbler -=for desigen