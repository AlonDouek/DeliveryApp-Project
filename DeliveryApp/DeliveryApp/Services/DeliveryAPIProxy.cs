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
using DeliveryApp.DTO;
using Newtonsoft.Json;

namespace DeliveryApp.Services
{
    class DeliveryAPIProxy
    {
        private const string CLOUD_URL = "TBD"; //API url when going on the cloud
        //change ips
        private const string DEV_ANDROID_EMULATOR_URL = "http://10.0.2.2:16340"; //API url when using emulator on android
        private const string DEV_ANDROID_PHYSICAL_URL = "http://10.58.55.7:16340"; //API url when using physucal device on android
        private const string DEV_WINDOWS_URL = "http://localhost:16340"; //API url when using windoes on development
        
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
                string str = $"{this.baseUri}/DeliveryAPI/Login?email={email}&pass={pass}";
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/DeliveryAPI/Login?email={email}&pass={pass}");
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    User u = System.Text.Json.JsonSerializer.Deserialize<User>(content, options);
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
        
        public async Task<bool> SignUpAsync(User user)
        {
            try
            {
                string userJson = System.Text.Json.JsonSerializer.Serialize(user);
                StringContent userJsonContent = new StringContent(userJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await this.client.PostAsync($"{this.baseUri}/DeliveryAPI/SignUp", userJsonContent);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve, 
                        PropertyNameCaseInsensitive = true
                    };
                    string content = await response.Content.ReadAsStringAsync();
                    bool Success = System.Text.Json.JsonSerializer.Deserialize<bool>(content, options);
                    return Success;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> ChangeCredentialsAsync(string CuserEmail,string Email,string Password,string Username,string Address,string CreditCard,string PhoneNumber)
        {
            try
            {
                string url = Uri.EscapeUriString($"{this.baseUri}/DeliveryAPI/ChangeCredentials?CUEmail={CuserEmail}&Password={Password}&Email={Email}&Username={Username}&Address={Address}&CreditCard={CreditCard}&PhoneNumber={PhoneNumber}");
                HttpResponseMessage response = await this.client.GetAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        

        public async Task<List<Restaurant>> GetAllRestaurantsAsync()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/DeliveryAPI/getRestaurants");
                if (response.IsSuccessStatusCode)
                {
                    string Content = await response.Content.ReadAsStringAsync();
                    List<Restaurant> so = JsonConvert.DeserializeObject<List<Restaurant>>(Content);
                    
                    return so;
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

        public async Task<List<Models.Menu>> GetAllMenusAsync()
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/DeliveryAPI/getMenus");
                if (response.IsSuccessStatusCode)
                {
                    string Content = await response.Content.ReadAsStringAsync();
                    List<Models.Menu> so = JsonConvert.DeserializeObject<List<Models.Menu>>(Content);

                    return so;
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

        public async Task<Models.Menu> GetMenuAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/DeliveryAPI/getMenuByRes?ID={id}");
                if (response.IsSuccessStatusCode)
                {
                    string Content = await response.Content.ReadAsStringAsync();
                    Models.Menu so = JsonConvert.DeserializeObject<Models.Menu>(Content);

                    return so;
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
        public async Task<List<Models.MenuItem>> GetItemsByMenuIDAsync(int MenuID)
        {
            try//change to right one
            {
                HttpResponseMessage response = await this.client.GetAsync($"{this.baseUri}/DeliveryAPI/GetItemsByMenuID?MenuID={MenuID}");
                if (response.IsSuccessStatusCode)
                {
                    string Content = await response.Content.ReadAsStringAsync();
                    List<Models.MenuItem> so = JsonConvert.DeserializeObject<List<Models.MenuItem>>(Content);

                    return so;
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
